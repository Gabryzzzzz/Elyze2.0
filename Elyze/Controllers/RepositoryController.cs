// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.IO;
using Elyze.Data;
using Elyze.Helpers;
using Elyze.Helpers.DataTables;
using Elyze.Helpers.Headers;
using Elyze.Models;
using Elyze.Models.Repository;
using Elyze.Models.Societa;
using Elyze.Models.Valuta;
using Microsoft.AspNetCore.Localization;
//using Elyze.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Localization;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NPOI.HPSF;
using NPOI.SS.Formula.Functions;
using NPOI.SS.Formula.PTG;
using Org.BouncyCastle.Bcpg.Sig;
using static Elyze.Helpers.Headers.HeaderClassBuilders;

namespace Elyze.Controllers
{
    public class RepositoryController : Controller
    {
        private readonly ElyzeContext _context;
        private readonly IHtmlLocalizer<SharedResource> _sharedLocalizer;
        private readonly string _basePath;
        private readonly IClaimGetterService _claimGetterService;
        private readonly IConfiguration _configuration;

        public RepositoryController(ElyzeContext context, IHtmlLocalizer<SharedResource> sharedLocalizer, IConfiguration configuration, IClaimGetterService claimGetterService)
        {
            _context = context;
            this._sharedLocalizer = sharedLocalizer;
            _basePath = configuration.GetSection("Host:BaseHostPath").Value;
            _claimGetterService = claimGetterService;
            _configuration = configuration;
        }

        public IActionResult Index(int? id)
        {
            ViewData["BasePath"] = _basePath;
            ViewData["Header"] = HeaderClassBuilders.Repository.Index(_sharedLocalizer, _basePath);
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> GetRepositoryDataTableAsync()
        {
            try
            {
                var form = HelpersDataTables.ContextToDatatable(Request);

                DataTableResult result = new()
                {
                    Draw = form.Draw,
                    RecordsTotal = await _context.Repository.CountAsync()
                };

                var requestFeatures = Request.HttpContext.Features.Get<IRequestCultureFeature>();
                var culture = requestFeatures.RequestCulture.Culture.Name;

                var query = from repository in _context.Repository
                            join area in _context.Area on new { IdArea = repository.AreaId } equals new { IdArea = area.IdArea }
                            join lingua in _context.Lingue on new { IdLingua = area.IdLingua } equals new { IdLingua = lingua.Id }
                            where lingua != null && lingua.SiglaEstesa == culture
                            && (form.SearchValue.Equals("") ||
                            (repository.Titolo != null && repository.Titolo.ToLower().Contains(form.SearchValue)) ||
                            (repository.Descrizione != null && repository.Descrizione.ToLower().Contains(form.SearchValue)) ||
                            (area.Descrizione != null && area.Descrizione.ToLower().Contains(form.SearchValue)))
                            select new RepositoryDataTableViewModel
                            {
                                Id = repository.Id,
                                IdArea = area.IdArea,
                                Titolo = repository.Titolo,
                                Descrizione = repository.Descrizione,
                                Areadescrizione = area.Descrizione
                            };

                result.RecordsFiltered = query.Count();

                result.Data = await query.Skip(form.Start)
                    .Take(form.Length)
                    .Cast<object>()
                    .ToListAsync();

                return Json(result);
            }
            catch (Exception ex)
            {
                List<RepositoryDataTableViewModel> result = new List<RepositoryDataTableViewModel>();
                return Json(result);
            }
        }

        // GET: Gruppo/Create
        public IActionResult Create()
        {
            ViewData["Header"] = HeaderClassBuilders.Repository.Create(_sharedLocalizer, _basePath);

            string userId = _claimGetterService.UserId();
            bool isAdmin = _claimGetterService.IsAdmin().Result;

            IRequestCultureFeature? requestFeatures = Request.HttpContext.Features.Get<IRequestCultureFeature>();
            string? culture = requestFeatures?.RequestCulture.Culture.Name;

            var userArea = (from _area in _context.Area
                            join _lingua in _context.Lingue on new { LinguaId = _area.IdLingua } equals new { LinguaId = _lingua.Id }
                            join _userArea in _context.UserArea on new { AreaId = _area.IdArea } equals new { AreaId = _userArea.IdArea } into areaJoin
                            from subArea in areaJoin.DefaultIfEmpty()
                            where _lingua.SiglaEstesa == culture &&
                            (subArea.IdUser == userId || isAdmin)
                            select new
                            {
                                Id = _area.IdArea,
                                Descrizione = _area.Descrizione
                            }).ToList();

            var societa = (from _societa in _context.Societa
                               join _userSocieta in _context.UserSocieta on new { SocietaId = _societa.Id } equals new { SocietaId = _userSocieta.IdSocieta } into societaJoin
                               from subSocieta in societaJoin.DefaultIfEmpty()
                               where subSocieta.IdUser == userId || isAdmin
                               select new
                               {
                                   Id = _societa.Id,
                                   Descrizione = _societa.Descrizione
                               }).ToList();

            RepositoryFormViewModel repositoryFormViewModel = new RepositoryFormViewModel();
            repositoryFormViewModel.Area = new SelectList(userArea, "Id", "Descrizione");
            repositoryFormViewModel.Societa = new SelectList(societa, "Id", "Descrizione");

            return View(repositoryFormViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RepositoryFormViewModel repositoryFormViewModel)
        {
            if (ModelState.IsValid)
            {
                int iDAttachment = 0;
                IFormFile file = repositoryFormViewModel.UploadedFiles;

                byte[] bytes;
                using (var memoryStream = new MemoryStream())
                {
                    file.CopyTo(memoryStream);
                    bytes = memoryStream.ToArray();
                }

                string filePath = "";
                Models.Features features = _configuration.GetSection("Features").Get<Models.Features>();
                if (features.RepositorySaving == "DATABASE")
                {
                    filePath = file.Name;
                    Attachment attachment = new Attachment();
                    attachment.Description = filePath;
                    attachment.FileName = file.FileName;
                    attachment.FileExtension = new FileInfo(file.FileName).Extension;
                    attachment.FileBase64String = Convert.ToBase64String(bytes);

                    _context.Attachment.Add(attachment);
                    _context.SaveChanges();

                    iDAttachment = attachment.Id;
                }
                else if (features.RepositorySaving == "WWWROOT")
                {
                    string? company = Request.Cookies["Company"] == null ? "" : Request.Cookies["Company"];
                    string basePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot/Repository", company);

                    bool folderExists = Directory.Exists(basePath);
                    if (!folderExists)
                    {
                        Directory.CreateDirectory(basePath);
                    }

                    filePath = Path.Combine(basePath, file.FileName);
                    System.IO.File.WriteAllBytes(filePath, bytes);
                }
                else
                {
                    string? company = Request.Cookies["Company"] == null ? "" : Request.Cookies["Company"];
                    string basePath = Path.Combine(features.RepositorySaving, company);

                    bool folderExists = Directory.Exists(basePath);
                    if (!folderExists)
                    {
                        Directory.CreateDirectory(basePath);
                    }

                    filePath = Path.Combine(basePath, file.FileName);
                    System.IO.File.WriteAllBytes(filePath, bytes);
                }

                Data.Repository repository = new Data.Repository();
                repository.Descrizione = repositoryFormViewModel.Descrizione;
                repository.FilePath = filePath;
                repository.Titolo = repositoryFormViewModel.Titolo;
                repository.AreaId = repositoryFormViewModel.AreaId;
                repository.SocietaId = repositoryFormViewModel.SocietaId;
                repository.AttachmentId = iDAttachment != 0 ? iDAttachment : null;

                _context.Repository.Add(repository);
                _context.SaveChanges();
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Download(int id)
        {
            var repository = await (from repo in _context.Repository
                              join attach in _context.Attachment on repo.AttachmentId.Value equals attach.Id into lj
                              from subAttach in lj.DefaultIfEmpty()
                              where repo.Id == id
                              select new
                              {
                                  Repository = repo,
                                  Attachment = subAttach

                              }).FirstOrDefaultAsync();

            if(repository == null || repository.Repository == null)
            {
                return NotFound();
            }

            byte[] fileArray = null;
            string fileName = "";

            Models.Features features = _configuration.GetSection("Features").Get<Models.Features>();
            if (features.RepositorySaving == "DATABASE")
            {
                fileArray = Convert.FromBase64String(repository.Attachment.FileBase64String);
                fileName = repository.Attachment.FileName;
            }
            else 
            {
                try
                {
                    fileArray = System.IO.File.ReadAllBytes(repository.Repository.FilePath);
                    fileName = System.IO.Path.GetFileName(repository.Repository.FilePath);
                }
                catch(Exception ex)
                {
                    return NotFound("File Not Found");
                }
            }

            return File(fileArray, "application/octet-stream", fileName);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            ViewData["Header"] = HeaderClassBuilders.Repository.Delete(_sharedLocalizer, _basePath);

            if (id == null)
            {
                return NotFound();
            }

            var repository = await _context.Repository
                .Select(m => new RepositoryFormViewModel
                {
                    Id = m.Id,
                    Titolo = m.Titolo ?? "",
                    Descrizione = m.Descrizione ?? ""
                }).FirstOrDefaultAsync(x => x.Id == id);

            if (repository == null)
            {
                return NotFound();
            }

            return View(repository);
        }

        // POST: Valuta/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {

            var repository = await _context.Repository.FindAsync(id);
            if (repository == null)
            {
                return NotFound();
            }

            var attachment = await _context.Attachment.FindAsync(repository.AttachmentId);
            if(attachment != null)
            {
                _context.Attachment.Remove(attachment);
            }
            else
            {
                try
                {
                    System.IO.File.Delete(repository.FilePath);
                }
                catch(FileNotFoundException ex)
                {
                    _context.Repository.Remove(repository);
                    await _context.SaveChangesAsync();

                    return NotFound();
                }
            }

            _context.Repository.Remove(repository);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
