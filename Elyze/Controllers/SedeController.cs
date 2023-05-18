// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Elyze.Data;
using Elyze.Helpers.DataTables;
using Elyze.Helpers.Headers;
using Elyze.Models.BaseViewModels;
using Elyze.Models.Sede;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Localization;
using Microsoft.EntityFrameworkCore;

namespace Elyze.Controllers
{
    public class SedeController : Controller
    {
        private readonly ElyzeContext _context;
        private readonly IHtmlLocalizer<SharedResource> _sharedLocalizer;
        //appsettings configuration
        private readonly string _basePath;

        public SedeController(ElyzeContext context, IHtmlLocalizer<SharedResource> sharedLocalizer, IConfiguration configuration)
        {
            _context = context;
            _sharedLocalizer = sharedLocalizer;
            _basePath = configuration.GetSection("Host:BaseHostPath").Value;
        }

        public IActionResult RedirectIfNoSocieta()
        {
            return RedirectToAction("Index", "Societa", new { Redirect = "NoSocieta" });
        }

        //endpoint for the server side datatable
        [HttpPost]
        public async Task<IActionResult> GetSedeDataTable()
        {
            var form = HelpersDataTables.ContextToDatatable(Request);

            DataTableResult result = new()
            {
                Draw = form.Draw,
                RecordsTotal = await _context.Sede.CountAsync()
            };
            var query = from sede in _context.Sede
                        where (form.SearchValue.Equals("") ||
                        (sede.Descrizione != null && sede.Descrizione.ToLower().Contains(form.SearchValue)) ||
                        (sede.SedeN != null && sede.SedeN.ToLower().Contains(form.SearchValue)) ||
                        (sede.IsoNazione != null && sede.IsoNazione.ToLower().Contains(form.SearchValue)) ||
                        (sede.Nazione != null && sede.Nazione.ToLower().Contains(form.SearchValue)))
                        select new SedeViewDataTableModel
                        {
                            Id = sede.Id,
                            Descrizione = sede.Descrizione ?? "",
                            SedeN = sede.SedeN ?? "",
                            IsoNazione = sede.IsoNazione ?? "",
                            Nazione = sede.Nazione ?? ""
                        };

            if (_sharedLocalizer["Description"].Value == form.SortColumn)
            {
                query = form.SortColumnDirection == "asc" ? query.OrderBy(x => x.Descrizione) : query.OrderByDescending(x => x.Descrizione);
            }
            else
            if (_sharedLocalizer["Sede_N"].Value == form.SortColumn)
            {
                query = form.SortColumnDirection == "asc" ? query.OrderBy(x => x.SedeN) : query.OrderByDescending(x => x.SedeN);
            }
            else
            if (_sharedLocalizer["Country_Iso_Code"].Value == form.SortColumn)
            {
                query = form.SortColumnDirection == "asc" ? query.OrderBy(x => x.IsoNazione) : query.OrderByDescending(x => x.IsoNazione);
            }
            else
            if (_sharedLocalizer["Nazione"].Value == form.SortColumn)
            {
                query = form.SortColumnDirection == "asc" ? query.OrderBy(x => x.Nazione) : query.OrderByDescending(x => x.Nazione);
            }
            else
            {
                query = query.OrderBy(x => x.Id);
            }

            result.RecordsFiltered = query.Count();
            result.Data = await query.Skip(form.Start).Take(form.Length).Cast<object>().ToListAsync();

            return Json(result);
        }

        // GET: Sede
        public IActionResult Index()
        {
            if (!_context.Societa.Any())
            {
                return RedirectIfNoSocieta();
            }

            if (Request.Query["Redirect"].FirstOrDefault() != null)
            {
                ViewData["Redirect"] = Request.Query["Redirect"].First();
            }

            ViewData["Header"] = HeaderClassBuilders.Sede.Index(_sharedLocalizer, _basePath);

            return View();
        }

        // GET: Sede/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            ViewData["Header"] = HeaderClassBuilders.Sede.Details(_sharedLocalizer, _basePath);

            if (id == null)
            {
                return NotFound();
            }

            var sede = await _context.Sede
                .Select(x => new SedeViewFormModel()
                {
                    Id = x.Id,
                    Descrizione = x.Descrizione ?? "",
                    IsoNazione = x.IsoNazione ?? "",
                    Nazione = x.Nazione ?? "",
                    SedeN = x.SedeN ?? "",
                    SocietaId = x.SocietaId
                })
                .FirstOrDefaultAsync(m => m.Id == id);

            if (sede == null)
            {
                return NotFound();
            }

            ViewBag.societa = _context.Societa.Where(x => x.Id == sede.SocietaId).ToList();

            if (sede == null)
            {
                return NotFound();
            }

            return View(sede);
        }

        // GET: Sede/Create
        public IActionResult Create()
        {

            if (!_context.Societa.Any())
            {
                return RedirectIfNoSocieta();
            }

            try
            {
                ViewData["Header"] = HeaderClassBuilders.Sede.Create(_sharedLocalizer, _basePath);
                ViewBag.societa = _context.Societa.ToList();
                ViewBag.Valuta = _context.Valuta.ToList();
                return View();
            }
            catch (Exception ex)
            {
                return View("Error", new ErrorViewModel());
            }
        }

        // POST: Sede/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Descrizione,SedeN,IsoNazione,Nazione, SocietaId,ValutaId")] SedeViewFormModel sede)
        {
            if (ModelState.IsValid)
            {
                Sede newSede = new()
                {
                    Descrizione = sede.Descrizione,
                    SedeN = sede.SedeN,
                    IsoNazione = sede.IsoNazione,
                    Nazione = sede.Nazione,
                    SocietaId = sede.SocietaId
                };

                _context.Add(newSede);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(sede);
        }

        // GET: Sede/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            ViewData["Header"] = HeaderClassBuilders.Sede.Edit(_sharedLocalizer, _basePath);

            if (id == null)
            {
                return NotFound();
            }
            ViewBag.societa = _context.Societa.ToList();
            ViewBag.Valuta = _context.Valuta.ToList();
            var sede = await _context.Sede
                .Select(x => new SedeViewFormModel()
                {
                    Id = x.Id,
                    Descrizione = x.Descrizione ?? "",
                    IsoNazione = x.IsoNazione ?? "",
                    Nazione = x.Nazione ?? "",
                    SedeN = x.SedeN ?? "",
                    SocietaId = x.SocietaId
                })
                .FirstOrDefaultAsync(m => m.Id == id);

            if (sede == null)
            {
                return NotFound();
            }

            ViewBag.societa = _context.Societa.Where(x => x.Id == sede.SocietaId).ToList();

            if (sede == null)
            {
                return NotFound();
            }
            return View(sede);
        }

        // POST: Sede/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Descrizione,SedeN,IsoNazione,Nazione,SocietaId,ValutaId")] SedeViewFormModel sede)
        {
            if (id != sede.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {

                    var newSede = await _context.Sede.FindAsync(id);

                    if (newSede == null)
                    {
                        return NotFound();
                    }

                    newSede.Descrizione = sede.Descrizione;
                    newSede.SedeN = sede.SedeN;
                    newSede.IsoNazione = sede.IsoNazione;
                    newSede.Nazione = sede.Nazione;
                    newSede.SocietaId = sede.SocietaId;

                    _context.Update(newSede);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SedeExists(sede.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(sede);
        }

        // GET: Sede/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            ViewData["Header"] = HeaderClassBuilders.Sede.Delete(_sharedLocalizer, _basePath);


            if (id == null)
            {
                return NotFound();
            }

            var sede = await _context.Sede
                 .Select(x => new SedeViewFormModel()
                 {
                     Id = x.Id,
                     Descrizione = x.Descrizione ?? "",
                     IsoNazione = x.IsoNazione ?? "",
                     Nazione = x.Nazione ?? "",
                     SedeN = x.SedeN ?? "",
                     SocietaId = x.SocietaId
                 })
                 .FirstOrDefaultAsync(m => m.Id == id);

            if (sede == null)
            {
                return NotFound();
            }

            ViewBag.societa = _context.Societa.Where(x => x.Id == sede.SocietaId).ToList();

            if (sede == null)
            {
                return NotFound();
            }

            return View(sede);
        }

        // POST: Sede/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var sede = await _context.Sede.FindAsync(id);

            if (sede == null)
            {
                return NotFound();
            }

            _context.Sede.Remove(sede);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SedeExists(int id)
        {
            return _context.Sede.Any(e => e.Id == id);
        }
    }
}
