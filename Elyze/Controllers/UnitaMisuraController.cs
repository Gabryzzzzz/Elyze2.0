// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Elyze.Data;
using Elyze.Helpers.DataTables;
using Elyze.Helpers.Headers;
using Elyze.Models.UnitaDiMisura;
using Elyze.Models.Valuta;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Localization;
using Microsoft.EntityFrameworkCore;

namespace Elyze.Controllers
{
    public class UnitaMisuraController : Controller
    {
        private readonly ElyzeContext _context;
        private readonly IHtmlLocalizer<SharedResource> _sharedLocalizer;
        //appsettings configuration
        private readonly string _basePath;

        public UnitaMisuraController(ElyzeContext context, IHtmlLocalizer<SharedResource> sharedLocalizer, IConfiguration configuration)
        {
            _context = context;
            this._sharedLocalizer = sharedLocalizer;
            _basePath = configuration.GetSection("Host:BaseHostPath").Value;
        }


        //endpoint for the server side datatable
        [HttpPost]
        public async Task<IActionResult> GetUnitaMisuraDataTableAsync()
        {
            var form = HelpersDataTables.ContextToDatatable(Request);

            DataTableResult result = new()
            {
                Draw = form.Draw,
                RecordsTotal = await _context.UnitaMisura.CountAsync()
            };

            var query = from unita_misura in _context.UnitaMisura
                        where (form.SearchValue.Equals("") ||
                            (unita_misura.Nome != null && unita_misura.Nome.ToLower().Contains(form.SearchValue)) ||
                            (unita_misura.Simbolo != null && unita_misura.Simbolo.ToLower().Contains(form.SearchValue)))
                        select new ValutaViewFormModel
                        {
                            Id = unita_misura.Id,
                            Nome = unita_misura.Nome ?? "",
                            Simbolo = unita_misura.Simbolo ?? ""
                        };

            if (_sharedLocalizer["UDM_name"].Value == form.SortColumn)
            {
                query = form.SortColumnDirection == "asc" ? query.OrderBy(x => x.Nome) : query.OrderByDescending(x => x.Nome);
            }
            else
            {
                query = query.OrderBy(x => x.Id);
            }

            result.RecordsFiltered = query.Count();
            result.Data = await query.Skip(form.Start).Take(form.Length).Cast<object>().ToListAsync();

            return Json(result);
        }

        // GET: UnitaMisura
        public async Task<IActionResult> Index()
        {
            ViewData["Header"] = HeaderClassBuilders.UnitaDiMisura.Index(_sharedLocalizer, _basePath);
            ViewData["BasePath"] = _basePath;
            return View();
        }

        // GET: UnitaMisura/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            ViewData["Header"] = HeaderClassBuilders.UnitaDiMisura.Details(_sharedLocalizer, _basePath);

            if (id == null)
            {
                return NotFound();
            }

            var unitaDiMisura = await _context.UnitaMisura
                .Select(m => new UnitaDiMisuraViewFormModel
                {
                    Id = m.Id,
                    Nome = m.Nome ?? "",
                    Simbolo = m.Simbolo ?? ""
                })
                .FirstOrDefaultAsync(m => m.Id == id);

            if (unitaDiMisura == null)
            {
                return NotFound();
            }

            return View(unitaDiMisura);
        }

        // GET: UnitaMisura/Create
        public IActionResult Create()
        {
            ViewData["Header"] = HeaderClassBuilders.UnitaDiMisura.Create(_sharedLocalizer, _basePath);

            return View(new UnitaDiMisuraViewFormModel());
        }

        // POST: UnitaMisura/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,Simbolo")] UnitaDiMisuraViewFormModel oUnitaMisura)
        {
            if (ModelState.IsValid)
            {
                var unitaDiMisura = new UnitaMisura()
                {
                    Nome = oUnitaMisura.Nome,
                    Simbolo = oUnitaMisura.Simbolo
                };

                _context.Add(unitaDiMisura);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(oUnitaMisura);
        }

        // GET: UnitaMisura/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            ViewData["Header"] = HeaderClassBuilders.UnitaDiMisura.Edit(_sharedLocalizer, _basePath);

            if (id == null)
            {
                return NotFound();
            }

            var oUnitaMisura = await _context.UnitaMisura.FindAsync(id);
            if (oUnitaMisura == null)
            {
                return NotFound();
            }

            var unitaDiMisuraToEdit = new UnitaDiMisuraViewFormModel()
            {
                Id = oUnitaMisura.Id,
                Nome = oUnitaMisura.Nome,
                Simbolo = oUnitaMisura.Simbolo
            };

            return View(unitaDiMisuraToEdit);
        }

        // POST: UnitaMisura/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Simbolo")] UnitaMisura oUnitaMisura)
        {
            if (id != oUnitaMisura.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {

                    var unitaDiMisuraToUpdate = await _context.UnitaMisura.FindAsync(id);

                    unitaDiMisuraToUpdate.Nome = oUnitaMisura.Nome;
                    unitaDiMisuraToUpdate.Simbolo = oUnitaMisura.Simbolo;

                    _context.Update(unitaDiMisuraToUpdate);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UnitaMisuraExists(oUnitaMisura.Id))
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
            return View(oUnitaMisura);
        }

        // GET: UnitaMisura/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            ViewData["Header"] = HeaderClassBuilders.UnitaDiMisura.Delete(_sharedLocalizer, _basePath);

            if (id == null)
            {
                return NotFound();
            }

            var oUnitaMisura = await _context.UnitaMisura
                .FirstOrDefaultAsync(m => m.Id == id);
            if (oUnitaMisura == null)
            {
                return NotFound();
            }

            var unitaDiMisuraToEdit = new UnitaDiMisuraViewFormModel()
            {
                Id = oUnitaMisura.Id,
                Nome = oUnitaMisura.Nome,
                Simbolo = oUnitaMisura.Simbolo
            };

            return View(unitaDiMisuraToEdit);

        }

        // POST: UnitaMisura/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var oUnitaMisura = await _context.UnitaMisura.FindAsync(id);
            _context.UnitaMisura.Remove(oUnitaMisura);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UnitaMisuraExists(int id)
        {
            return _context.UnitaMisura.Any(e => e.Id == id);
        }
    }
}
