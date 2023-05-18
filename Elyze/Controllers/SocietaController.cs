// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Elyze.Data;
using Elyze.Helpers.DataTables;
using Elyze.Helpers.Headers;
using Elyze.Models.Societa;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Localization;
using Microsoft.EntityFrameworkCore;

namespace Elyze.Controllers
{
    public class SocietaController : Controller
    {
        private readonly ElyzeContext _context;
        private readonly IHtmlLocalizer<SharedResource> _sharedLocalizer;
        //appsettings configuration
        private readonly string _basePath;

        public SocietaController(ElyzeContext context, IHtmlLocalizer<SharedResource> sharedLocalizer, IConfiguration configuration)
        {
            _context = context;
            this._sharedLocalizer = sharedLocalizer;
            _basePath = configuration.GetSection("Host:BaseHostPath").Value;
        }
        public IActionResult RedirectIfNoGruppo()
        {
            return RedirectToAction("Index", "Gruppo", new { Redirect = "NoGruppo" });
        }

        //endpoint for the server side datatable
        [HttpPost]
        public async Task<IActionResult> GetSocietaDataTable()
        {
            var form = HelpersDataTables.ContextToDatatable(Request);

            DataTableResult result = new()
            {
                Draw = form.Draw,
                RecordsTotal = await _context.Societa.CountAsync()
            };
            var query = from societa_ in _context.Societa
                        join gruppo in _context.Gruppo on societa_.GruppoId equals gruppo.Id
                        where (form.SearchValue.Equals("") ||
                            (societa_.Descrizione != null && societa_.Descrizione.ToLower().Contains(form.SearchValue)) ||
                            (societa_.CodiceIsoNazione != null && societa_.CodiceIsoNazione.ToLower().Contains(form.SearchValue)) ||
                            (societa_.Nazione != null && societa_.Nazione.ToLower().Contains(form.SearchValue)))
                        select new SocietaViewDataTableModel
                        {
                            Id = societa_.Id,
                            GruppoId = societa_.GruppoId,
                            Descrizione = societa_.Descrizione ?? "",
                            CodiceIsoNazione = societa_.CodiceIsoNazione ?? "",
                            Nazione = societa_.Nazione ?? "",
                            GruppoName = gruppo.Nome ?? ""
                        };

            if (_sharedLocalizer["Description"].Value == form.SortColumn)
            {
                query = form.SortColumnDirection == "asc" ? query.OrderBy(x => x.Descrizione) : query.OrderByDescending(x => x.Descrizione);
            }
            else
            if (_sharedLocalizer["Country_Iso_Code"].Value == form.SortColumn)
            {
                query = form.SortColumnDirection == "asc" ? query.OrderBy(x => x.CodiceIsoNazione) : query.OrderByDescending(x => x.CodiceIsoNazione);
            }
            else
            if (_sharedLocalizer["Nazione"].Value == form.SortColumn)
            {
                query = form.SortColumnDirection == "asc" ? query.OrderBy(x => x.Nazione) : query.OrderByDescending(x => x.Nazione);
            }
            else
            if (_sharedLocalizer["Group"].Value == form.SortColumn)
            {
                query = form.SortColumnDirection == "asc" ? query.OrderBy(x => x.GruppoName) : query.OrderByDescending(x => x.GruppoName);
            }
            else
            {
                query = query.OrderBy(x => x.Id);
            }

            result.RecordsFiltered = query.Count();
            result.Data = await query.Skip(form.Start).Take(form.Length).Cast<object>().ToListAsync();

            return Json(result);
        }

        // GET: Societa
        public IActionResult Index()
        {
            if (!_context.Gruppo.Any())
            {
                return RedirectIfNoGruppo();
            }

            if (Request.Query["Redirect"].FirstOrDefault() != null)
            {
                ViewData["Redirect"] = Request.Query["Redirect"].First();
            }

            ViewData["Header"] = HeaderClassBuilders.Societa.Index(_sharedLocalizer, _basePath);
            return View();
        }

        // GET: Societa/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            ViewData["Header"] = HeaderClassBuilders.Societa.Details(_sharedLocalizer, _basePath);

            if (id == null)
            {
                return NotFound();
            }

            ViewBag.Gruppi = _context.Gruppo.ToList();
            var societa = await _context.Societa
                .Select(x => new SocietaViewFormModel()
                {
                    Id = x.Id,
                    GruppoId = x.GruppoId,
                    Descrizione = x.Descrizione ?? "",
                    CodiceIsoNazione = x.CodiceIsoNazione ?? "",
                    Nazione = x.Nazione ?? ""
                })
                .FirstOrDefaultAsync(m => m.Id == id);
            if (societa == null)
            {
                return NotFound();
            }

            return View(societa);
        }

        // GET: Societa/Create
        public async Task<IActionResult> Create()
        {
            if (!_context.Gruppo.Any())
            {
                return RedirectIfNoGruppo();
            }

            ViewData["Header"] = HeaderClassBuilders.Societa.Create(_sharedLocalizer, _basePath);

            ViewBag.Gruppi = await _context.Gruppo.ToListAsync();
            ViewBag.Sedi = await _context.Sede.ToListAsync();

            return View();
        }

        // POST: Societa/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,GruppoId,Descrizione,Sede,CodiceIsoNazione,Nazione")] SocietaViewFormModel societa)
        {
            if (ModelState.IsValid)
            {

                var newSocieta = new Societa()
                {
                    GruppoId = societa.GruppoId,
                    Descrizione = societa.Descrizione,
                    CodiceIsoNazione = societa.CodiceIsoNazione,
                    Nazione = societa.Nazione
                };

                _context.Add(newSocieta);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Gruppi = await _context.Gruppo.ToListAsync();
            return View(societa);
        }

        // GET: Societa/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            ViewData["Header"] = HeaderClassBuilders.Societa.Edit(_sharedLocalizer, _basePath);
            ViewBag.Gruppi = await _context.Gruppo.ToListAsync();

            if (id == null)
            {
                return NotFound();
            }

            var societa = await _context.Societa.Select(x => new SocietaViewFormModel()
            {
                Id = x.Id,
                GruppoId = x.GruppoId,
                Descrizione = x.Descrizione ?? "",
                CodiceIsoNazione = x.CodiceIsoNazione ?? "",
                Nazione = x.Nazione ?? ""
            }).FirstOrDefaultAsync(m => m.Id == id);

            if (societa == null)
            {
                return NotFound();
            }
            return View(societa);
        }

        // POST: Societa/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,GruppoId,Descrizione,Sede,CodiceIsoNazione,Nazione")] SocietaViewFormModel societa)
        {
            if (id != societa.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var newSocieta = await _context.Societa.FindAsync(id);

                    if (newSocieta == null)
                    {
                        return NotFound();
                    }

                    newSocieta.Nazione = societa.Nazione;
                    newSocieta.CodiceIsoNazione = societa.CodiceIsoNazione;
                    newSocieta.Descrizione = societa.Descrizione;
                    newSocieta.GruppoId = societa.GruppoId;

                    if (newSocieta == null)
                    {
                        return NotFound();
                    }

                    _context.Update(newSocieta);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SocietaExists(societa.Id))
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
            return View(societa);
        }

        // GET: Societa/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            ViewData["Header"] = HeaderClassBuilders.Societa.Delete(_sharedLocalizer, _basePath);

            if (id == null)
            {
                return NotFound();
            }

            var societa = await _context.Societa.Select(x => new SocietaViewFormModel()
            {
                Id = x.Id,
                GruppoId = x.GruppoId,
                Descrizione = x.Descrizione ?? "",
                CodiceIsoNazione = x.CodiceIsoNazione ?? "",
                Nazione = x.Nazione ?? ""
            }).FirstOrDefaultAsync(m => m.Id == id);

            if (societa == null)
            {
                return NotFound();
            }

            ViewBag.Gruppi = await _context.Gruppo.Where(x => x.Id == societa.GruppoId).ToListAsync();

            if (societa == null)
            {
                return NotFound();
            }

            return View(societa);
        }

        // POST: Societa/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var societa = await _context.Societa.FindAsync(id);

            if (societa == null)
            {
                return NotFound();
            }

            _context.Societa.Remove(societa);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SocietaExists(int id)
        {
            return _context.Societa.Any(e => e.Id == id);
        }
    }
}
