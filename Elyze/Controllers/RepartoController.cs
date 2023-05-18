// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Elyze.Data;
using Elyze.Helpers.DataTables;
using Elyze.Helpers.Headers;
using Elyze.Models.Reparto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Localization;
using Microsoft.EntityFrameworkCore;

namespace Elyze.Controllers
{
    public class RepartoController : Controller
    {
        private readonly ElyzeContext _context;
        private readonly IHtmlLocalizer<SharedResource> _sharedLocalizer;
        //appsettings configuration
        private readonly string _basePath;

        public RepartoController(ElyzeContext context, IHtmlLocalizer<SharedResource> sharedLocalizer, IConfiguration configuration)
        {
            _context = context;
            _sharedLocalizer = sharedLocalizer;
            _basePath = configuration.GetSection("Host:BaseHostPath").Value;
        }

        public IActionResult RedirectIfNoSede()
        {
            return RedirectToAction("Index", "Sede", new { Redirect = "NoSede" });
        }

        //endpoint for the server side datatable
        [HttpPost]
        public async Task<IActionResult> GetRepartoDataTable()
        {
            var form = HelpersDataTables.ContextToDatatable(Request);

            DataTableResult result = new()
            {
                Draw = form.Draw,
                RecordsTotal = await _context.Reparto.CountAsync()
            };

            var query = from reparti in _context.Reparto
                        join stabilimento in _context.Sede on reparti.StabilimentoId equals stabilimento.Id
                        where (form.SearchValue.Equals("") ||
                            (reparti.Nome != null && reparti.Nome.ToLower().Contains(form.SearchValue)) ||
                            (reparti.Descrizione != null && reparti.Descrizione.ToLower().Contains(form.SearchValue)) ||
                            (stabilimento.Descrizione != null && stabilimento.Descrizione.ToLower().Contains(form.SearchValue)))
                        select new RepartoViewDataTableModel
                        {
                            Id = reparti.Id,
                            Nome = reparti.Nome ?? "",
                            Descrizione = reparti.Descrizione ?? "",
                            DescrizioneSede = stabilimento.Descrizione ?? ""
                        };

            if (_sharedLocalizer["Rep_Name"].Value == form.SortColumn)
            {
                query = form.SortColumnDirection == "asc" ? query.OrderBy(x => x.Nome) : query.OrderByDescending(x => x.Nome);
            }
            else if (_sharedLocalizer["Sede"].Value == form.SortColumn)
            {
                query = form.SortColumnDirection == "asc" ? query.OrderBy(x => x.DescrizioneSede) : query.OrderByDescending(x => x.DescrizioneSede);
            }
            else if (_sharedLocalizer["Rep_Description"].Value == form.SortColumn)
            {
                query = form.SortColumnDirection == "asc" ? query.OrderBy(x => x.Descrizione) : query.OrderByDescending(x => x.Descrizione);

            }
            else
            {
                query = query.OrderBy(x => x.Id);
            }

            result.RecordsFiltered = query.Count();
            result.Data = await query.Skip(form.Start).Take(form.Length).Cast<object>().ToListAsync();

            return Json(result);
        }

        // GET: Reparto
        public IActionResult Index()
        {
            if (!_context.Sede.Any())
            {
                return RedirectIfNoSede();
            }

            if (Request.Query["Redirect"].FirstOrDefault() != null)
            {
                ViewData["Redirect"] = Request.Query["Redirect"].First();
            }

            ViewData["Header"] = HeaderClassBuilders.Reparto.Index(_sharedLocalizer, _basePath);
            return View();
        }

        // GET: Reparto/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            ViewData["Header"] = HeaderClassBuilders.Reparto.Details(_sharedLocalizer, _basePath);
            ViewBag.Sedi = _context.Sede.ToList();

            if (id == null)
            {
                return NotFound();
            }

            var reparto = await _context.Reparto
                .Select(x => new RepartoViewFormModel()
                {
                    Id = x.Id,
                    Nome = x.Nome ?? "",
                    Descrizione = x.Descrizione ?? "",
                    StabilimentoId = x.StabilimentoId
                })
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reparto == null)
            {
                return NotFound();
            }

            return View(reparto);
        }

        // GET: Reparto/Create
        public IActionResult Create()
        {
            if (!_context.Sede.Any())
            {
                return RedirectIfNoSede();
            }

            ViewData["Header"] = HeaderClassBuilders.Reparto.Create(_sharedLocalizer, _basePath);
            ViewBag.Sedi = _context.Sede.ToList();
            return View();
        }

        // POST: Reparto/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,StabilimentoId,Descrizione")] RepartoViewFormModel reparto)
        {
            if (ModelState.IsValid)
            {
                var newReparto = new Reparto()
                {
                    Nome = reparto.Nome,
                    Descrizione = reparto.Descrizione,
                    StabilimentoId = reparto.StabilimentoId
                };

                _context.Add(newReparto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(reparto);
        }

        // GET: Reparto/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            ViewData["Header"] = HeaderClassBuilders.Reparto.Edit(_sharedLocalizer, _basePath);
            ViewBag.Sedi = _context.Sede.ToList();

            if (id == null)
            {
                return NotFound();
            }

            var reparto = await _context.Reparto
                .Select(x => new RepartoViewFormModel()
                {
                    Id = x.Id,
                    Nome = x.Nome ?? "",
                    Descrizione = x.Descrizione ?? "",
                    StabilimentoId = x.StabilimentoId
                })
                .FirstOrDefaultAsync(x => x.Id == id);

            if (reparto == null)
            {
                return NotFound();
            }
            return View(reparto);
        }

        // POST: Reparto/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,StabilimentoId,Descrizione")] RepartoViewFormModel reparto)
        {
            if (id != reparto.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {

                    var repartoToUpdate = await _context.Reparto.FindAsync(id);
                    if (repartoToUpdate == null)
                    {
                        return NotFound();
                    }

                    repartoToUpdate.Nome = reparto.Nome;
                    repartoToUpdate.Descrizione = reparto.Descrizione;
                    repartoToUpdate.StabilimentoId = reparto.StabilimentoId;

                    _context.Update(repartoToUpdate);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RepartoExists(reparto.Id))
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
            return View(reparto);
        }

        // GET: Reparto/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            ViewData["Header"] = HeaderClassBuilders.Reparto.Delete(_sharedLocalizer, _basePath);

            if (id == null)
            {
                return NotFound();
            }

            var reparto = await _context.Reparto
                .Select(x => new RepartoViewFormModel()
                {
                    Id = x.Id,
                    Nome = x.Nome ?? "",
                    Descrizione = x.Descrizione ?? "",
                    StabilimentoId = x.StabilimentoId
                })
                .FirstOrDefaultAsync(x => x.Id == id);

            if (reparto == null)
            {
                return NotFound();
            }

            ViewBag.Sedi = _context.Sede.Where(x => x.Id == reparto.StabilimentoId).ToList();

            return View(reparto);
        }

        // POST: Reparto/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var reparto = await _context.Reparto.FindAsync(id);

            if (reparto == null)
            {
                return NotFound();
            }

            _context.Reparto.Remove(reparto);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RepartoExists(int id)
        {
            return _context.Reparto.Any(e => e.Id == id);
        }
    }
}
