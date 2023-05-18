// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Elyze.Data;
using Elyze.Helpers.DataTables;
using Elyze.Helpers.Headers;
using Elyze.Models;
using Elyze.Models.MicroArea;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Localization;
using Microsoft.EntityFrameworkCore;

namespace Elyze.Controllers
{
    public class MicroAreaController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHtmlLocalizer<SharedResource> _sharedLocalizer;
        private readonly IConfiguration _configuration;
        private readonly ElyzeContext _context;
        private readonly string _basePath;
        private string _isForConfindustria;
        private string _estensione;

        public MicroAreaController(IHtmlLocalizer<SharedResource> sharedLocalizer, IConfiguration configuration, ElyzeContext context)
        {
            _sharedLocalizer = sharedLocalizer;
            _configuration = configuration;
            _context = context;
            _basePath = configuration.GetSection("Host:BaseHostPath").Value;

            _isForConfindustria = configuration.GetValue<bool>("CustomStyle:Confindustria") ? "Confindustria" : "Normale";
            _estensione = _isForConfindustria == "Normale" ? "png" : "svg";
        }

        //check if there are any area, if not redirect to area controller
        public IActionResult RedirectIfNoArea()
        {
            return RedirectToAction("Index", "Area", new { Redirect = "NoArea" });
        }

        //post
        [HttpPost]
        public async Task<IActionResult> GetMicroAreaDataTable()
        {
            var form = HelpersDataTables.ContextToDatatable(Request);

            DataTableResult result = new()
            {
                Draw = form.Draw,
                RecordsTotal = await _context.MicroArea.CountAsync()
            };

            var areaFilter = int.Parse(Request.Form["AreaFilter"].First());
            var query = from MA in _context.MicroArea
                        join A in _context.Area on MA.IdArea equals A.Id
                        join L in _context.Lingue on A.IdLingua equals L.Id
                        join GRI in _context.Gri on MA.IdGri equals GRI.Id
                        //
                        where ((form.SearchValue.Equals("") ||
                            (MA.Id.ToString().Contains(form.SearchValue)) ||
                            ((MA.Stato ? "true" : "false").Contains(form.SearchValue)) ||
                            (MA.Descrizione != null && MA.Descrizione.ToLower().Contains(form.SearchValue)) ||
                            (MA.DataCreazione.ToString().Contains(form.SearchValue)) ||
                            (MA.DataSpegnimento.ToString().Contains(form.SearchValue)) ||
                            (A.Id.ToString().ToLower().Contains(form.SearchValue)) ||
                            (A.Descrizione != null && A.Descrizione.ToLower().Contains(form.SearchValue)) ||
                            (L.Id.ToString().ToLower().Contains(form.SearchValue)) ||
                            (L.Sigla != null && L.Sigla.ToLower().Contains(form.SearchValue)) ||
                            (MA.Id.ToString().ToLower().Contains(form.SearchValue))) &&
                            (areaFilter == 0 || A.IdArea == areaFilter))
                        select new MicroAreaDataTableViewModel
                        {
                            Id = MA.Id,
                            Stato = MA.Stato,
                            Descrizione = MA.Descrizione ?? "",
                            DataCreazione = MA.DataCreazione.ToShortDateString(),
                            DataSpegnimento = MA.DataSpegnimento.ToShortDateString(),
                            AreaId = A.Id,
                            AreaName = A.Descrizione ?? "",
                            GriId = GRI.Id,
                            GriName = GRI.CodiceGri ?? "",
                            LinguaId = L.Id,
                            LinguaName = L.Sigla ?? "",
                            IdMicroArea = MA.IdMicroArea
                        };

            if (_sharedLocalizer["Description"].Value == form.SortColumn)
            {
                query = form.SortColumnDirection == "asc" ? query.OrderBy(x => x.Descrizione) : query.OrderByDescending(x => x.Descrizione);
            }
            else if (_sharedLocalizer["Area_CreationDate"].Value == form.SortColumn)
            {
                query = form.SortColumnDirection == "asc" ? query.OrderBy(x => x.DataCreazione) : query.OrderByDescending(x => x.DataCreazione);
            }
            else if (_sharedLocalizer["Area_ValidityDate"].Value == form.SortColumn)
            {
                query = form.SortColumnDirection == "asc" ? query.OrderBy(x => x.DataSpegnimento) : query.OrderByDescending(x => x.DataCreazione);
            }
            else if (_sharedLocalizer["Area"].Value == form.SortColumn)
            {
                query = form.SortColumnDirection == "asc" ? query.OrderBy(x => x.AreaName) : query.OrderByDescending(x => x.AreaName);
            }
            else if (_sharedLocalizer["Gri_Id"].Value == form.SortColumn)
            {
                query = form.SortColumnDirection == "asc" ? query.OrderBy(x => x.GriName) : query.OrderByDescending(x => x.GriName);
            }
            else if (_sharedLocalizer["Language"].Value == form.SortColumn)
            {
                query = form.SortColumnDirection == "asc" ? query.OrderBy(x => x.LinguaName) : query.OrderByDescending(x => x.LinguaName);
            }
            else if (_sharedLocalizer["Status"].Value == form.SortColumn)
            {
                query = form.SortColumnDirection == "asc" ? query.OrderBy(x => x.Stato) : query.OrderByDescending(x => x.Stato);
            }
            else
            {
                query = query.OrderBy(x => x.Id);
            }

            var dataset1 = await query.ToListAsync();
            var dataset2 = await query.ToListAsync();

            dataset1.ForEach(x =>
            {
                List<string> languages = new()
                {
                    x.LinguaName
                };
                dataset2.ForEach(y =>
                {
                    if (x.IdMicroArea == y.IdMicroArea)
                    {
                        languages.Add(y.LinguaName);
                    }
                });
                x.LinguaName = string.Join(" - ", languages.Distinct().ToList());
            });

            result.RecordsFiltered = query.Count();
            result.Data = dataset1.Skip(form.Start).Take(form.Length).Cast<object>().ToList();

            return Json(result);
        }

        public async Task<IActionResult> Main(int id)
        {
            if (!_context.Area.Any())
            {
                return RedirectIfNoArea();
            }

            ViewData["Header"] = HeaderClassBuilders.MicroArea.Main(_sharedLocalizer, _basePath);
            ViewBag.Features = _configuration.GetSection("Features").Get<Features>(); ;


            ViewData["IdArea"] = id;

            var idImage = await _context.Area.Where(x => x.Id == id).Select(y => y.IdIconaArea).FirstOrDefaultAsync();

            List<AreaIcons> areaIcons = _context.AreaIcons
                .Where(x => x.Id == idImage)
                .Select(x => new AreaIcons()
                {
                    Id = x.Id,
                    Descrizione = x.Descrizione,
                    Codice = x.Codice,
                    LocalPath = string.Format(x.LocalPath, _isForConfindustria, _estensione)
                })
                .ToList();

            ViewData["AreaImage"] = areaIcons.Where(x => x.Id == idImage).Select(y => string.Format(y.LocalPath, _isForConfindustria, _estensione)).FirstOrDefault();

            var microAree = await _context.MicroArea
                .Where(x => x.IdArea == id)
                .Select(y => new MicroAreaMainViewModel
                {
                    IdMicroArea = y.Id,
                    Nome = y.Descrizione ?? "",
                    IdArea = id
                }).ToListAsync();

            return View(microAree);
        }

        public async Task<IActionResult> Index()
        {

            if (!_context.Area.Any())
            {
                return RedirectIfNoArea();
            }

            if (Request.Query["Redirect"].FirstOrDefault() != null)
            {
                Console.WriteLine("Redirect " + Request.Query["Redirect"].First());
                ViewData["Redirect"] = Request.Query["Redirect"].First();
            }

            ViewData["Header"] = HeaderClassBuilders.MicroArea.Index(_sharedLocalizer, _basePath);
            ViewData["BasePath"] = _basePath;

            ViewBag.Aree = await _context.Area
                .Include(p => p.IdLinguaNavigation)
                .Where(x => x.Stato).Select(y => new
                {
                    Descrizione = y.Descrizione + " - " + y.IdLinguaNavigation.Sigla,
                    Id = y.IdArea
                })
                .OrderBy(y => y.Id)
                .ToListAsync();

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Create(int? id)
        {
            ViewData["Header"] = HeaderClassBuilders.MicroArea.Create(_sharedLocalizer, _basePath);

            if (!_context.Area.Any())
            {
                return RedirectIfNoArea();
            }

            if (Request.Query["Redirect"].FirstOrDefault() != null)
            {
                Console.WriteLine("Redirect " + Request.Query["Redirect"].First());
                ViewData["Redirect"] = Request.Query["Redirect"].First();
            }

            var lastIdMicroArea = 1;
            if (_context.MicroArea.Any())
            {
                lastIdMicroArea = _context.MicroArea.Select(a => a.IdMicroArea).Max() + 1;
            }


            var microArea = new MicroAreaFormViewModel
            {
                IdArea = id ?? 0,
                IdMicroArea = lastIdMicroArea,
            };

            ViewBag.Aree = await _context.Area
                .Include(p => p.IdLinguaNavigation)
                .Where(x => x.Stato).Select(y => new
                {
                    Descrizione = y.Descrizione + " - " + y.IdLinguaNavigation.Sigla,
                    Id = y.Id
                })
                .OrderBy(y => y.Id)
                .ToListAsync();

            ViewBag.Gri = await _context.Gri.Select(y => new
            {
                Descrizione = y.CodiceGri,
                Id = y.Id
            }).ToListAsync();

            var idImage = await _context.Area.Where(x => x.IdArea == id).Select(y => y.IdIconaArea).FirstOrDefaultAsync();

            return View(microArea);
        }

        [HttpPost]
        public async Task<IActionResult> Create(MicroAreaFormViewModel microArea)
        {

            var microAreaToAdd = new Data.MicroArea
            {
                DataCreazione = microArea.DataCreazione,
                DataSpegnimento = microArea.DataSpegnimento,
                Descrizione = microArea.Descrizione,
                IdArea = microArea.IdArea,
                IdGri = microArea.IdGri,
                Stato = microArea.Stato,
                IdMicroArea = microArea.IdMicroArea,
            };

            await _context.MicroArea.AddAsync(microAreaToAdd);
            await _context.SaveChangesAsync();

            return RedirectToAction("Main", "MicroArea", new { id = microArea.IdArea });


            //return View(microArea);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (!_context.Area.Any())
            {
                return RedirectIfNoArea();
            }

            ViewData["Header"] = HeaderClassBuilders.MicroArea.Delete(_sharedLocalizer, _basePath);

            var micro = await _context.MicroArea
                .Where(y => y.Id == id)
                .Select(x => new MicroAreaFormViewModel()
                {
                    Id = x.Id,
                    IdArea = x.IdArea,
                    IdGri = x.IdGri,
                    DataCreazione = x.DataCreazione,
                    DataSpegnimento = x.DataSpegnimento,
                    Descrizione = x.Descrizione,
                    Stato = x.Stato,
                    IdMicroArea = x.IdMicroArea,
                }).FirstAsync();


            ViewBag.Aree = await _context.Area
                 .Include(p => p.IdLinguaNavigation)
                 .Where(x => x.Stato).Select(y => new
                 {
                     Descrizione = y.Descrizione + " - " + y.IdLinguaNavigation.Sigla,
                     Id = y.Id
                 })
                 .OrderBy(y => y.Id)
                 .ToListAsync();

            ViewBag.Gri = await _context.Gri.Select(y => new
            {
                Descrizione = y.CodiceGri,
                Id = y.Id
            }).ToListAsync();

            var idImage = await _context.Area.Where(x => x.IdArea == id).Select(y => y.IdIconaArea).FirstOrDefaultAsync();

            return View(micro);
        }


        // POST: Valuta/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {

            var microAreaToDelete = await _context.MicroArea.FindAsync(id);

            if (microAreaToDelete == null)
            {
                return NotFound();
            }

            _context.MicroArea.Remove(microAreaToDelete);
            await _context.SaveChangesAsync();

            return RedirectToAction("Main", "MicroArea", new { id = microAreaToDelete.IdArea });
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (!_context.Area.Any())
            {
                return RedirectIfNoArea();
            }

            ViewData["Header"] = HeaderClassBuilders.MicroArea.Details(_sharedLocalizer, _basePath);

            var micro = await _context.MicroArea
                .Where(y => y.Id == id)
                .Select(x => new MicroAreaFormViewModel()
                {
                    Id = x.Id,
                    IdArea = x.IdArea,
                    IdGri = x.IdGri,
                    DataCreazione = x.DataCreazione,
                    DataSpegnimento = x.DataSpegnimento,
                    Descrizione = x.Descrizione,
                    Stato = x.Stato,
                    IdMicroArea = x.IdMicroArea,
                }).FirstAsync();


            ViewBag.Aree = await _context.Area
                .Include(p => p.IdLinguaNavigation)
                .Where(x => x.Stato).Select(y => new
                {
                    Descrizione = y.Descrizione + " - " + y.IdLinguaNavigation.Sigla,
                    Id = y.Id
                })
                .OrderBy(y => y.Id)
                .ToListAsync();

            ViewBag.Gri = await _context.Gri.Select(y => new
            {
                Descrizione = y.CodiceGri,
                Id = y.Id
            }).ToListAsync();

            var idImage = await _context.Area.Where(x => x.IdArea == id).Select(y => y.IdIconaArea).FirstOrDefaultAsync();

            return View(micro);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (!_context.Area.Any())
            {
                return RedirectIfNoArea();
            }

            ViewData["Header"] = HeaderClassBuilders.MicroArea.Edit(_sharedLocalizer, _basePath);

            var micro = await _context.MicroArea
                .Where(y => y.Id == id)
                .Select(x => new MicroAreaFormViewModel()
                {
                    Id = x.Id,
                    IdArea = x.IdArea,
                    IdGri = x.IdGri,
                    DataCreazione = x.DataCreazione,
                    DataSpegnimento = x.DataSpegnimento,
                    Descrizione = x.Descrizione,
                    Stato = x.Stato,
                    IdMicroArea = x.IdMicroArea,
                }).FirstAsync();


            ViewBag.Aree = await _context.Area
                .Include(p => p.IdLinguaNavigation)
                .Where(x => x.Stato).Select(y => new
                {
                    Descrizione = y.Descrizione + " - " + y.IdLinguaNavigation.Sigla,
                    Id = y.Id
                })
                .OrderBy(y => y.Id)
                .ToListAsync();

            ViewBag.Gri = await _context.Gri.Select(y => new
            {
                Descrizione = y.CodiceGri,
                Id = y.Id
            }).ToListAsync();

            var idImage = await _context.Area.Where(x => x.IdArea == id).Select(y => y.IdIconaArea).FirstOrDefaultAsync();

            return View(micro);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(MicroAreaFormViewModel microArea)
        {
            ViewData["Header"] = HeaderClassBuilders.MicroArea.Edit(_sharedLocalizer, _basePath);

            var microAreaToUpdate = await _context.MicroArea
                .Where(y => y.Id == microArea.Id).FirstAsync();

            microAreaToUpdate.Stato = microArea.Stato;
            microAreaToUpdate.DataCreazione = microArea.DataCreazione;
            microAreaToUpdate.DataSpegnimento = microArea.DataSpegnimento;
            microAreaToUpdate.IdGri = microArea.IdGri;
            microAreaToUpdate.Descrizione = microArea.Descrizione;
            microAreaToUpdate.IdArea = microArea.IdArea;
            microAreaToUpdate.IdMicroArea = microArea.IdMicroArea;

            _context.Update(microAreaToUpdate);
            await _context.SaveChangesAsync();

            return RedirectToAction("Main", "MicroArea", new { id = microAreaToUpdate.IdArea });
        }

        [HttpGet]
        public async Task<IActionResult> AggiungiLingua(int? id)
        {
            if (!_context.Area.Any())
            {
                return RedirectIfNoArea();
            }

            ViewData["Header"] = HeaderClassBuilders.MicroArea.AggiungiLingua(_sharedLocalizer, _basePath);

            var micro = await _context.MicroArea
                .Include(u => u.IdAreaNavigation)
                .Where(y => y.Id == id)
                .Select(x => new MicroAreaFormViewModel()
                {
                    IdArea = x.IdArea,
                    IdGri = x.IdGri,
                    DataCreazione = x.DataCreazione,
                    DataSpegnimento = x.DataSpegnimento,
                    Descrizione = x.Descrizione,
                    Stato = x.Stato,
                    IdMicroArea = x.IdMicroArea
                }).FirstAsync();

            var idArea = await _context.MicroArea
                .Include(u => u.IdAreaNavigation)
                .Where(y => y.Id == id)
                .Select(x => x.IdAreaNavigation.IdArea).FirstAsync();

            ViewBag.Aree = await _context.Area
                .Include(p => p.IdLinguaNavigation)
                .Where(x => x.Stato && x.IdArea == idArea).Select(y => new
                {
                    Descrizione = y.Descrizione + " - " + y.IdLinguaNavigation.Sigla,
                    Id = y.Id
                })
                .OrderBy(y => y.Id)
                .ToListAsync();

            ViewBag.Gri = await _context.Gri.Select(y => new
            {
                Descrizione = y.CodiceGri,
                Id = y.Id
            }).ToListAsync();

            var idImage = await _context.Area.Where(x => x.IdArea == id).Select(y => y.IdIconaArea).FirstOrDefaultAsync();

            return View(micro);
        }

        [HttpPost]
        public async Task<IActionResult> AggiungiLingua(MicroAreaFormViewModel microArea)
        {
            ViewData["Header"] = HeaderClassBuilders.MicroArea.Edit(_sharedLocalizer, _basePath);


            var microAreaToAddLingua = new MicroArea
            {
                DataCreazione = microArea.DataCreazione,
                DataSpegnimento = microArea.DataSpegnimento,
                Descrizione = microArea.Descrizione,
                IdArea = microArea.IdArea,
                IdGri = microArea.IdGri,
                Stato = microArea.Stato,
                IdMicroArea = microArea.IdMicroArea,
            };

            await _context.MicroArea.AddAsync(microAreaToAddLingua);
            await _context.SaveChangesAsync();

            return RedirectToAction("Main", "MicroArea", new { id = microAreaToAddLingua.IdArea });
        }

    }
}
