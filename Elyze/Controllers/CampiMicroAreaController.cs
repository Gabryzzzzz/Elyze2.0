// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
using Elyze.Data;
using Elyze.Helpers.DataTables;
using Elyze.Helpers.Headers;
using Elyze.Models.CampiMA;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Localization;
using Microsoft.EntityFrameworkCore;

namespace Elyze.Controllers
{
    public class CampiMicroAreaController : Controller
    {
        private readonly ElyzeContext _context;
        private readonly IHtmlLocalizer<SharedResource> _sharedLocalizer;
        //appsettings configuration
        private readonly string _basePath;

        public CampiMicroAreaController(ElyzeContext context, IHtmlLocalizer<SharedResource> sharedLocalizer, IConfiguration configuration)
        {
            _context = context;
            _sharedLocalizer = sharedLocalizer;
            _basePath = configuration.GetSection("Host:BaseHostPath").Value;
        }

        public IActionResult RedirectIfNoMicroArea()
        {
            return RedirectToAction("Index", "MicroArea", new { Redirect = "NoMicroArea" });
        }


        //post
        [HttpPost]
        public async Task<IActionResult> GetCampiMicroAreaDataTable()
        {
            var form = HelpersDataTables.ContextToDatatable(Request);

            DataTableResult result = new()
            {
                Draw = form.Draw,
                RecordsTotal = await _context.CampiMa.CountAsync()
            };

            var campiMAFilter = int.Parse(Request.Form["CampiMA"].FirstOrDefault() ?? "0");


            var query = from CMA in _context.CampiMa
                        join MA in _context.MicroArea on CMA.IdMicroArea equals MA.Id
                        join A in _context.Area on MA.IdArea equals A.Id
                        join L in _context.Lingue on A.IdLingua equals L.Id
                        join T in _context.TipologieCampiMicroArea on CMA.IdTipologia equals T.Id
                        join UDM in _context.UnitaMisura on CMA.IdUDM equals UDM.Id
                        where ((form.SearchValue.Equals("") ||
                            (CMA.Descrizione != null && CMA.Descrizione.ToLower().Contains(form.SearchValue)) ||
                            (MA.Descrizione != null && MA.Descrizione.ToLower().Contains(form.SearchValue)) ||
                            (T.Nome != null && T.Nome.ToLower().Contains(form.SearchValue)) ||
                            (CMA.InizioValidita.ToString().ToLower().Contains(form.SearchValue)) ||
                            (UDM.Nome != null && UDM.Nome.ToLower().Contains(form.SearchValue)) ||
                            (CMA.FineValidita.ToString().ToLower().Contains(form.SearchValue))) &&
                            (campiMAFilter == 0 || CMA.IdMicroArea == campiMAFilter))
                        select new CampiMADataTableViewModel
                        {
                            Id = CMA.Id,
                            Descrizione = CMA.Descrizione ?? "",
                            DataCreazione = CMA.InizioValidita.ToShortDateString(),
                            DataSpegnimento = CMA.FineValidita.ToShortDateString(),
                            MicroArea = MA.Descrizione ?? "",
                            Tipologia = T.Nome,
                            UDM = UDM.Nome ?? "",
                            LinguaId = L.Id,
                            LinguaName = L.Sigla ?? "",
                            IdCampo = CMA.Id
                        };


            if (_sharedLocalizer["MA_MicroArea"].Value == form.SortColumn)
            {
                query = form.SortColumnDirection == "asc" ? query.OrderBy(x => x.MicroArea) : query.OrderByDescending(x => x.MicroArea);
            }
            else if (_sharedLocalizer["MA_Type"].Value == form.SortColumn)
            {
                query = form.SortColumnDirection == "asc" ? query.OrderBy(x => x.Tipologia) : query.OrderByDescending(x => x.Tipologia);
            }
            else if (_sharedLocalizer["Description"].Value == form.SortColumn)
            {
                query = form.SortColumnDirection == "asc" ? query.OrderBy(x => x.Descrizione) : query.OrderByDescending(x => x.Descrizione);
            }
            else if (_sharedLocalizer["Start_Date"].Value == form.SortColumn)
            {
                query = form.SortColumnDirection == "asc" ? query.OrderBy(x => x.DataCreazione) : query.OrderByDescending(x => x.DataCreazione);
            }
            else if (_sharedLocalizer["End_Date"].Value == form.SortColumn)
            {
                query = form.SortColumnDirection == "asc" ? query.OrderBy(x => x.DataSpegnimento) : query.OrderByDescending(x => x.DataSpegnimento);
            }
            else if (_sharedLocalizer["UDM"].Value == form.SortColumn)
            {
                query = form.SortColumnDirection == "asc" ? query.OrderBy(x => x.UDM) : query.OrderByDescending(x => x.UDM);
            }
            else if (_sharedLocalizer["Language"].Value == form.SortColumn)
            {
                query = form.SortColumnDirection == "asc" ? query.OrderBy(x => x.LinguaName) : query.OrderByDescending(x => x.LinguaName);
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
                    if (x.IdCampo == y.IdCampo)
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

        public async Task<IActionResult> Index()
        {
            if (!_context.MicroArea.Any())
            {
                return RedirectIfNoMicroArea();
            }

            ViewData["Header"] = HeaderClassBuilders.CampoMicroArea.Index(_sharedLocalizer, _basePath);
            ViewData["BasePath"] = _basePath;
            ViewBag.MicroAree = _context.MicroArea
                .Include(p => p.IdAreaNavigation.IdLinguaNavigation)
                .Select(x => new { Id = x.Id, Descrizione = x.Descrizione + " - " + x.IdAreaNavigation.IdLinguaNavigation.Sigla })
                .ToList();

            return View("Index");
        }

        //httpget
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            if (!_context.MicroArea.Any())
            {
                return RedirectIfNoMicroArea();
            }

            ViewData["Header"] = HeaderClassBuilders.CampoMicroArea.Create(_sharedLocalizer, _basePath);

            var lastIdCampoMA = 1;
            if (_context.CampiMa.Any())
            {
                lastIdCampoMA = _context.CampiMa.Select(a => a.Id).Max() + 1;
            }

            var campoToAdd = new CampiMAFormViewModel()
            {
                IdCampoMA = lastIdCampoMA
            };

            var tipologie = await _context.TipologieCampiMicroArea.Select(x => new { x.Id, x.Nome }).ToListAsync();
            var udms = await _context.UnitaMisura.Select(x => new { x.Id, x.Nome }).ToListAsync();
            ViewBag.Tipologie = tipologie;
            ViewBag.UDMs = udms;

            ViewBag.MicroArea = _context.MicroArea
                .Include(p => p.IdAreaNavigation.IdLinguaNavigation)
                .Select(x => new { Id = x.Id, Descrizione = x.Descrizione + " - " + x.IdAreaNavigation.IdLinguaNavigation.Sigla })
                .ToList();


            return View(campoToAdd);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CampiMAFormViewModel campo)
        {

            var campoToAdd = new CampiMa()
            {
                Descrizione = campo.Descrizione,
                IdMicroArea = campo.IdMicroArea,
                IdTipologia = campo.IdTipologia,
                InizioValidita = campo.DataCreazione,
                FineValidita = campo.DataSpegnimento,
                IdUDM = campo.IdUDM,
                IdCampoMa = campo.IdCampoMA
            };



            _context.Add(campoToAdd);



            await _context.SaveChangesAsync();

            var inserimenti = _context.InserimentiFissi.Where(x => x.IdMicroArea == campo.IdMicroArea).ToList();
            if (inserimenti.Count > 0)
            {
                foreach (var inserimento in inserimenti)
                {
                    var inserimentoToAdd = new Inserimenti()
                    {
                        IdInserimento = inserimento.IdInserimento,
                        IdCampo = campoToAdd.Id,
                        ValoreCampo = null,
                        ValoreValidato = null
                    };

                    _context.Add(inserimentoToAdd);
                }

                await _context.SaveChangesAsync();
            }


            return RedirectToAction("Index", "CampiMicroArea");
        }

        //httpget
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            if (!_context.MicroArea.Any())
            {
                return RedirectIfNoMicroArea();
            }

            ViewData["Header"] = HeaderClassBuilders.CampoMicroArea.Edit(_sharedLocalizer, _basePath);

            var campoToEdit = await _context.CampiMa.Where(x => x.Id == id).Select(x => new CampiMAFormViewModel()
            {
                Id = x.Id,
                DataCreazione = x.InizioValidita,
                DataSpegnimento = x.FineValidita,
                Descrizione = x.Descrizione ?? "",
                IdMicroArea = x.IdMicroArea,
                IdTipologia = x.IdTipologia,
                IdUDM = x.IdUDM,
                IdCampoMA = x.IdCampoMa
            }).FirstOrDefaultAsync();

            if (campoToEdit == null)
            {
                return NotFound();
            }

            var tipologie = await _context.TipologieCampiMicroArea.Select(x => new { x.Id, x.Nome }).ToListAsync();
            var udms = await _context.UnitaMisura.Select(x => new { x.Id, x.Nome }).ToListAsync();
            ViewBag.Tipologie = tipologie;
            ViewBag.UDMs = udms;

            ViewBag.MicroArea = _context.MicroArea
               .Include(p => p.IdAreaNavigation.IdLinguaNavigation)
               .Select(x => new { Id = x.Id, Descrizione = x.Descrizione + " - " + x.IdAreaNavigation.IdLinguaNavigation.Sigla })
               .ToList();


            return View(campoToEdit);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(CampiMAFormViewModel campo)
        {

            var campoToEdit = await _context.CampiMa.Where(x => x.Id == campo.Id).FirstAsync();

            //campoToEdit.Id = campo.Id;
            campoToEdit.InizioValidita = campo.DataCreazione;
            campoToEdit.FineValidita = campo.DataSpegnimento;
            campoToEdit.Descrizione = campo.Descrizione;
            campoToEdit.IdMicroArea = campo.IdMicroArea;
            campoToEdit.IdTipologia = campo.IdTipologia;
            campoToEdit.IdCampoMa = campo.IdCampoMA;

            _context.Update(campoToEdit);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index", "CampiMicroArea");
        }

        //httpget
        [HttpGet]
        public async Task<IActionResult> AggiungiLingua(int id)
        {
            if (!_context.MicroArea.Any())
            {
                return RedirectIfNoMicroArea();
            }

            ViewData["Header"] = HeaderClassBuilders.CampoMicroArea.AggiungiLingua(_sharedLocalizer, _basePath);

            var campoToEdit = await _context.CampiMa.Where(x => x.Id == id).Select(x => new CampiMAFormViewModel()
            {
                Id = x.Id,
                DataCreazione = x.InizioValidita,
                DataSpegnimento = x.FineValidita,
                Descrizione = x.Descrizione ?? "",
                IdMicroArea = x.IdMicroArea,
                IdTipologia = x.IdTipologia,
                IdUDM = x.IdUDM,
                IdCampoMA = x.IdCampoMa
            }).FirstOrDefaultAsync();

            if (campoToEdit == null)
            {
                return NotFound();
            }

            var idMicroArea = await _context.MicroArea
                .Where(y => y.Id == campoToEdit.IdMicroArea)
                .Select(x => x.IdMicroArea).FirstAsync();

            var campoToAdd = new CampiMAFormViewModel();
            ViewBag.MicroArea = await _context.MicroArea
               .Include(p => p.IdAreaNavigation.IdLinguaNavigation)
               .Where(y => y.IdMicroArea == idMicroArea)
               .Select(x => new { Id = x.Id, Descrizione = x.Descrizione + " - " + x.IdAreaNavigation.IdLinguaNavigation.Sigla }).ToListAsync();

            var tipologie = await _context.TipologieCampiMicroArea.Select(x => new { x.Id, x.Nome }).ToListAsync();
            var udms = await _context.UnitaMisura.Select(x => new { x.Id, x.Nome }).ToListAsync();
            ViewBag.Tipologie = tipologie;
            ViewBag.UDMs = udms;

            return View(campoToEdit);
        }

        [HttpPost]
        public async Task<IActionResult> AggiungiLingua(CampiMAFormViewModel campo)
        {

            var campoToAdd = new CampiMa()
            {
                Descrizione = campo.Descrizione,
                IdMicroArea = campo.IdMicroArea,
                IdTipologia = campo.IdTipologia,
                InizioValidita = campo.DataCreazione,
                FineValidita = campo.DataSpegnimento,
                IdUDM = campo.IdUDM,
                IdCampoMa = campo.IdCampoMA
            };

            _context.Add(campoToAdd);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index", "CampiMicroArea");
        }

        //httpget
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            if (!_context.MicroArea.Any())
            {
                return RedirectIfNoMicroArea();
            }

            ViewData["Header"] = HeaderClassBuilders.CampoMicroArea.Details(_sharedLocalizer, _basePath);

            var campo = await _context.CampiMa.Where(x => x.Id == id).Select(x => new CampiMAFormViewModel()
            {
                Id = x.Id,
                DataCreazione = x.InizioValidita,
                DataSpegnimento = x.FineValidita,
                Descrizione = x.Descrizione ?? "",
                IdMicroArea = x.IdMicroArea,
                IdTipologia = x.IdTipologia,
                IdUDM = x.IdUDM
            }).FirstOrDefaultAsync();

            if (campo == null)
            {
                return NotFound();
            }

            var campoToAdd = new CampiMAFormViewModel();
            var microAree = await _context.MicroArea.Select(x => new { x.Id, x.Descrizione }).ToListAsync();
            var tipologie = await _context.TipologieCampiMicroArea.Select(x => new { x.Id, x.Nome }).ToListAsync();
            var udms = await _context.UnitaMisura.Select(x => new { x.Id, x.Nome }).ToListAsync();
            ViewBag.MicroArea = microAree;
            ViewBag.Tipologie = tipologie;
            ViewBag.UDMs = udms;


            return View(campo);
        }

        //httpget
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            if (!_context.MicroArea.Any())
            {
                return RedirectIfNoMicroArea();
            }

            ViewData["Header"] = HeaderClassBuilders.CampoMicroArea.Delete(_sharedLocalizer, _basePath);

            var campoToDelete = await _context.CampiMa.Where(x => x.Id == id).Select(x => new CampiMAFormViewModel()
            {
                Id = x.Id,
                DataCreazione = x.InizioValidita,
                DataSpegnimento = x.FineValidita,
                Descrizione = x.Descrizione ?? "",
                IdMicroArea = x.IdMicroArea,
                IdTipologia = x.IdTipologia,
                IdUDM = x.IdUDM,
                IdCampoMA = x.IdCampoMa
            }).FirstOrDefaultAsync();

            if (campoToDelete == null)
            {
                return NotFound();
            }

            var tipologie = await _context.TipologieCampiMicroArea.Select(x => new { x.Id, x.Nome }).ToListAsync();
            var udms = await _context.UnitaMisura.Select(x => new { x.Id, x.Nome }).ToListAsync();
            ViewBag.Tipologie = tipologie;
            ViewBag.UDMs = udms;

            ViewBag.MicroArea = _context.MicroArea
               .Include(p => p.IdAreaNavigation.IdLinguaNavigation)
               .Select(x => new { Id = x.Id, Descrizione = x.Descrizione + " - " + x.IdAreaNavigation.IdLinguaNavigation.Sigla })
               .ToList();


            return View(campoToDelete);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(CampiMAFormViewModel campo)
        {

            var campoToDelete = await _context.CampiMa.Where(x => x.Id == campo.Id).FirstAsync();

            _context.CampiMa.Remove(campoToDelete);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index", "CampiMicroArea");
        }

    }
}
