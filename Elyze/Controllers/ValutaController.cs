// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Elyze.Data;
using Elyze.Helpers.DataTables;
using Elyze.Helpers.Headers;
using Elyze.Models.Valuta;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Localization;
using Microsoft.EntityFrameworkCore;

namespace Elyze.Controllers
{
    public class ValutaController : Controller
    {
        private readonly ElyzeContext _context;
        private readonly IHtmlLocalizer<SharedResource> _sharedLocalizer;
        //appsettings configuration
        private readonly string _basePath;

        public ValutaController(ElyzeContext context, IHtmlLocalizer<SharedResource> sharedLocalizer, IConfiguration configuration)
        {
            _context = context;
            this._sharedLocalizer = sharedLocalizer;
            _basePath = configuration.GetSection("Host:BaseHostPath").Value;
        }

        //endpoint for the server side datatable
        [HttpPost]
        public async Task<IActionResult> GetValutaDataTableAsync()
        {
            var form = HelpersDataTables.ContextToDatatable(Request);

            DataTableResult result = new()
            {
                Draw = form.Draw,
                RecordsTotal = await _context.Valuta.CountAsync()
            };

            var query = from valuta in _context.Valuta
                        where (form.SearchValue.Equals("") ||
                            (valuta.Nome != null && valuta.Nome.ToLower().Contains(form.SearchValue)) ||
                            (valuta.Simbolo != null && valuta.Simbolo.ToLower().Contains(form.SearchValue)))
                        select new ValutaViewFormModel
                        {
                            Id = valuta.Id,
                            Nome = valuta.Nome ?? "",
                            Simbolo = valuta.Simbolo ?? ""
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

        // GET: Valuta
        public IActionResult Index()
        {
            ViewData["BasePath"] = _basePath;
            ViewData["Header"] = HeaderClassBuilders.Valuta.Index(_sharedLocalizer, _basePath);
            return View();
        }

        // GET: Valuta/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            ViewData["Header"] = HeaderClassBuilders.Valuta.Details(_sharedLocalizer, _basePath);

            if (id == null)
            {
                return NotFound();
            }

            var oValuta = await _context.Valuta
                .Select(m => new ValutaViewFormModel
                {
                    Id = m.Id,
                    Nome = m.Nome ?? "",
                    Simbolo = m.Simbolo ?? ""
                })
                .FirstOrDefaultAsync(m => m.Id == id);

            if (oValuta == null)
            {
                return NotFound();
            }

            return View(oValuta);
        }

        // GET: Valuta/Create
        public IActionResult Create()
        {
            ViewData["Header"] = HeaderClassBuilders.Valuta.Create(_sharedLocalizer, _basePath);

            return View();
        }

        // POST: Valuta/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,Simbolo")] ValutaViewFormModel oValuta)
        {

            if (ModelState.IsValid)
            {
                Valuta valutaNew = new()
                {
                    Nome = oValuta.Nome ?? "",
                    Simbolo = oValuta.Simbolo ?? "",
                };

                _context.Add(valutaNew);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(oValuta);
        }

        // GET: Valuta/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            ViewData["Header"] = HeaderClassBuilders.Valuta.Edit(_sharedLocalizer, _basePath);

            if (id == null)
            {
                return NotFound();
            }

            var oValuta = await _context.Valuta
                .Select(m => new ValutaViewFormModel
                {
                    Id = m.Id,
                    Nome = m.Nome ?? "",
                    Simbolo = m.Simbolo ?? ""
                }).FirstOrDefaultAsync(x => x.Id == id);

            if (oValuta == null)
            {
                return NotFound();
            }


            return View(oValuta);
        }

        // POST: Valuta/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Simbolo")] Valuta oValuta)
        {
            ViewData["Header"] = HeaderClassBuilders.Valuta.Edit(_sharedLocalizer, _basePath);


            if (id != oValuta.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    Valuta valutaNew = new()
                    {
                        Id = oValuta.Id,
                        Nome = oValuta.Nome ?? "",
                        Simbolo = oValuta.Simbolo ?? "",
                    };
                    _context.Update(valutaNew);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ValutaExists(oValuta.Id))
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

            ValutaViewFormModel viewForm = new()
            {
                Id = oValuta.Id,
                Nome = oValuta.Nome ?? "",
                Simbolo = oValuta.Simbolo ?? ""
            };

            return View(viewForm);
        }

        // GET: Valuta/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            ViewData["Header"] = HeaderClassBuilders.Valuta.Delete(_sharedLocalizer, _basePath);

            if (id == null)
            {
                return NotFound();
            }

            var oValuta = await _context.Valuta
                .Select(m => new ValutaViewFormModel
                {
                    Id = m.Id,
                    Nome = m.Nome ?? "",
                    Simbolo = m.Simbolo ?? ""
                }).FirstOrDefaultAsync(x => x.Id == id);

            if (oValuta == null)
            {
                return NotFound();
            }

            return View(oValuta);
        }

        // POST: Valuta/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {

            var oValuta = await _context.Valuta.FindAsync(id);

            if (oValuta == null)
            {
                return NotFound();
            }

            _context.Valuta.Remove(oValuta);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ValutaExists(int id)
        {
            return _context.Valuta.Any(e => e.Id == id);
        }
    }
}
