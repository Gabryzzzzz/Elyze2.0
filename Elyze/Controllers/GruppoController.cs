// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Elyze.Data;
using Elyze.Helpers.DataTables;
using Elyze.Helpers.Headers;
using Elyze.Models.Gruppo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Localization;
using Microsoft.EntityFrameworkCore;

namespace Elyze.Controllers
{
    public class GruppoController : Controller
    {
        private readonly ElyzeContext _context;
        private readonly IHtmlLocalizer<SharedResource> _sharedLocalizer;
        //appsettings configuration
        private readonly string _basePath;

        public GruppoController(ElyzeContext context, IHtmlLocalizer<SharedResource> sharedLocalizer, IConfiguration configuration)
        {
            _context = context;
            _sharedLocalizer = sharedLocalizer;
            _basePath = configuration.GetSection("Host:BaseHostPath").Value;
        }

        [HttpPost]
        public async Task<IActionResult> GetGruppoDataTable()
        {
            var form = HelpersDataTables.ContextToDatatable(Request);

            DataTableResult result = new()
            {
                Draw = form.Draw,
                RecordsTotal = await _context.Gruppo.CountAsync()
            };

            var query = from gruppo in _context.Gruppo
                        where (form.SearchValue.Equals("") ||
                            (gruppo.Nome != null && gruppo.Nome.ToLower().Contains(form.SearchValue)) ||
                            (gruppo.Descrizione != null && gruppo.Descrizione.ToLower().Contains(form.SearchValue)))
                        select new GruppoViewDataTableModel
                        {
                            Id = gruppo.Id,
                            Nome = gruppo.Nome ?? "",
                            Descrizione = gruppo.Descrizione ?? ""
                        };

            if (_sharedLocalizer["Group_Name"].Value == form.SortColumn)
            {
                query = form.SortColumnDirection == "asc" ? query.OrderBy(x => x.Nome) : query.OrderByDescending(x => x.Nome);
            }
            else
            if (_sharedLocalizer["Group_Description"].Value == form.SortColumn)
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

        // GET: Gruppo
        public IActionResult Index()
        {
            ViewData["Header"] = HeaderClassBuilders.Gruppo.Index(_sharedLocalizer, _basePath);

            if (Request.Query["Redirect"].FirstOrDefault() != null)
            {
                ViewData["Redirect"] = Request.Query["Redirect"].First();
            }

            return View();
        }

        // GET: Gruppo/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            ViewData["Header"] = HeaderClassBuilders.Gruppo.Details(_sharedLocalizer, _basePath);

            if (id == null)
            {
                return NotFound();
            }

            var gruppo = await _context.Gruppo
                .Select(x => new GruppoViewFormModel()
                {
                    Id = x.Id,
                    Nome = x.Nome ?? "",
                    Descrizione = x.Descrizione ?? ""
                })
                .FirstOrDefaultAsync(m => m.Id == id);

            if (gruppo == null)
            {
                return NotFound();
            }

            return View(gruppo);
        }

        // GET: Gruppo/Create
        public IActionResult Create()
        {
            ViewData["Header"] = HeaderClassBuilders.Gruppo.Create(_sharedLocalizer, _basePath);

            return View();
        }

        // POST: Gruppo/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,Descrizione")] GruppoViewFormModel gruppo)
        {
            if (ModelState.IsValid)
            {
                Gruppo newGruppo = new()
                {
                    Nome = gruppo.Nome,
                    Descrizione = gruppo.Descrizione
                };

                _context.Add(newGruppo);
                gruppo.Id = newGruppo.Id;
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(gruppo);
        }

        // GET: Gruppo/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            ViewData["Header"] = HeaderClassBuilders.Gruppo.Edit(_sharedLocalizer, _basePath);

            if (id == null)
            {
                return NotFound();
            }

            var gruppo = await _context.Gruppo.Select(x => new GruppoViewFormModel()
            {
                Id = x.Id,
                Nome = x.Nome ?? "",
                Descrizione = x.Descrizione ?? ""
            }).FirstOrDefaultAsync(x => x.Id == id);


            if (gruppo == null)
            {
                return NotFound();
            }
            return View(gruppo);
        }

        // POST: Gruppo/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Descrizione")] GruppoViewFormModel gruppo)
        {
            if (id != gruppo.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var gruppoToUpdate = await _context.Gruppo.FirstOrDefaultAsync(x => x.Id == id);

                    if (gruppoToUpdate == null)
                    {
                        return NotFound();
                    }

                    gruppoToUpdate.Nome = gruppo.Nome;
                    gruppoToUpdate.Descrizione = gruppo.Descrizione;

                    _context.Update(gruppoToUpdate);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GruppoExists(gruppo.Id))
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
            return View(gruppo);
        }

        // GET: Gruppo/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            ViewData["Header"] = HeaderClassBuilders.Gruppo.Delete(_sharedLocalizer, _basePath);


            if (id == null)
            {
                return NotFound();
            }

            var gruppo = await _context.Gruppo
                .Select(x => new GruppoViewFormModel()
                {
                    Id = x.Id,
                    Descrizione = x.Descrizione ?? "",
                    Nome = x.Nome ?? ""
                })
                .FirstOrDefaultAsync(m => m.Id == id);
            if (gruppo == null)
            {
                return NotFound();
            }

            return View(gruppo);
        }

        // POST: Gruppo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var gruppo = await _context.Gruppo.FindAsync(id);

            if (gruppo == null)
            {
                return NotFound();
            }

            _context.Gruppo.Remove(gruppo);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GruppoExists(int id)
        {
            return _context.Gruppo.Any(e => e.Id == id);
        }
    }
}
