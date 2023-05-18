// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Elyze.Data;
using Elyze.Helpers.DataTables;
using Elyze.Helpers.Headers;
using Elyze.Models.OperatoriDiConversione;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Localization;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Elyze.Controllers
{
    public class OperatoreDiConversioneController : Controller
    {
        private readonly ElyzeContext _context;
        private readonly IHtmlLocalizer<SharedResource> _sharedLocalizer;
        //appsettings configuration
        private readonly string _basePath;

        public OperatoreDiConversioneController(ElyzeContext context, IHtmlLocalizer<SharedResource> sharedLocalizer, IConfiguration configuration)
        {
            _context = context;
            _sharedLocalizer = sharedLocalizer;
            _basePath = configuration.GetSection("Host:BaseHostPath").Value;
        }

        [HttpPost]
        public async Task<IActionResult> GetOperatoreDiConversioneDataTable()
        {
            var form = HelpersDataTables.ContextToDatatable(Request);

            DataTableResult result = new()
            {
                Draw = form.Draw,
                RecordsTotal = await _context.Gruppo.CountAsync()
            };

            var query = from _operatoriDiconversione in _context.OperatoriDiConversione
                        join _unitaDiMisuraArrivo in _context.UnitaMisura on new { UnitaDiMisuraArrivo = _operatoriDiconversione.IdUnitaDiMisuraArrivo.Value } equals new { UnitaDiMisuraArrivo = _unitaDiMisuraArrivo.Id }
                        join _unitaDiMisuraPartenza in _context.UnitaMisura on new { UnitaDiMisuraPartenza = _operatoriDiconversione.IdUnitaDiMisuraPartenza.Value } equals new { UnitaDiMisuraPartenza = _unitaDiMisuraPartenza.Id }
                        join _operazione in _context.Operazione on _operatoriDiconversione.IdOperazione equals _operazione.Id
                        where (form.SearchValue.Equals("") ||
                            (string.IsNullOrEmpty(_operatoriDiconversione.FattoreDiConversione.ToString()) && _operatoriDiconversione.FattoreDiConversione.ToString().ToLower().Contains(form.SearchValue)) ||
                            ((_unitaDiMisuraArrivo.Nome != null && _unitaDiMisuraArrivo.Simbolo != null) && (_unitaDiMisuraArrivo.Nome.ToLower().Contains(form.SearchValue) || _unitaDiMisuraArrivo.Simbolo.ToLower().Contains(form.SearchValue))) ||
                            ((_unitaDiMisuraPartenza.Nome != null && _unitaDiMisuraPartenza.Simbolo != null) && (_unitaDiMisuraPartenza.Nome.ToLower().Contains(form.SearchValue) || _unitaDiMisuraPartenza.Simbolo.ToLower().Contains(form.SearchValue))) ||
                            (_operazione.OperatoreDiConversione != null && _operazione.OperatoreDiConversione.ToString().ToLower().Contains(form.SearchValue)))
                        select new OperatoreDiConversioneDataTableView
                        {
                            Id = _operatoriDiconversione.Id,
                            FattoreDiConversione = _operatoriDiconversione.FattoreDiConversione.ToString() ?? "",
                            UnitaDiMisuraArrivoDecrizione = _unitaDiMisuraArrivo.Nome + " (" + _unitaDiMisuraArrivo.Simbolo + ")",
                            UnitaDiMisuraPartenzaDecrizione = _unitaDiMisuraPartenza.Nome + " (" + _unitaDiMisuraPartenza.Simbolo + ")",
                            OperazioneDescrizione = _operazione.OperatoreDiConversione ?? ""
                        };

            if (_sharedLocalizer["Conversion_Factor"].Value == form.SortColumn)
            {
                query = form.SortColumnDirection == "asc" ? query.OrderBy(x => x.FattoreDiConversione) : query.OrderByDescending(x => x.FattoreDiConversione);
            }
            else
            if (_sharedLocalizer["UDM_Target"].Value == form.SortColumn)
            {
                query = form.SortColumnDirection == "asc" ? query.OrderBy(x => x.UnitaDiMisuraArrivoDecrizione) : query.OrderByDescending(x => x.UnitaDiMisuraArrivoDecrizione);
            }
            else
            if (_sharedLocalizer["UDM_Start"].Value == form.SortColumn)
            {
                query = form.SortColumnDirection == "asc" ? query.OrderBy(x => x.UnitaDiMisuraPartenzaDecrizione) : query.OrderByDescending(x => x.UnitaDiMisuraPartenzaDecrizione);
            }
            else
            if (_sharedLocalizer["Operation"].Value == form.SortColumn)
            {
                query = form.SortColumnDirection == "asc" ? query.OrderBy(x => x.OperazioneDescrizione) : query.OrderByDescending(x => x.OperazioneDescrizione);
            }
            else
            {
                query = query.OrderBy(x => x.Id);
            }

            result.RecordsFiltered = query.Count();
            result.Data = await query.Skip(form.Start).Take(form.Length).Cast<object>().ToListAsync();

            return Json(result);
        }

        private OperatoreDiConversioneViewDto CreateOperatoreDiConversioneViewDto()
        {
            var unitaDiMisura = (from _unitaDiMisura in _context.UnitaMisura
                                 select new
                                 {
                                     _unitaDiMisura.Id,
                                     Nome = $"{_unitaDiMisura.Nome} ({_unitaDiMisura.Simbolo})"
                                 }).ToList();

            OperatoreDiConversioneViewDto operatoreDiConversioneViewDto = new()
            {
                UnitaDiMisuraPartenza = new SelectList(unitaDiMisura, "Id", "Nome"),
                UnitaDiMisuraArrivo = new SelectList(unitaDiMisura, "Id", "Nome"),
                Operazione = new SelectList(_context.Operazione.ToList(), "Id", "OperatoreDiConversione")
            };

            return operatoreDiConversioneViewDto;
        }

        private async Task<OperatoreDiConversioneViewDto?> GetOperatoreDiConversioneById(int? id)
        {
            var operatoreDiconversioneViewDTO = await (from _operatoriDiconversione in _context.OperatoriDiConversione
                                                       join _unitaDiMisuraArrivo in _context.UnitaMisura on new { UnitaDiMisuraArrivo = _operatoriDiconversione.IdUnitaDiMisuraArrivo.Value } equals new { UnitaDiMisuraArrivo = _unitaDiMisuraArrivo.Id }
                                                       join _unitaDiMisuraPartenza in _context.UnitaMisura on new { UnitaDiMisuraPartenza = _operatoriDiconversione.IdUnitaDiMisuraPartenza.Value } equals new { UnitaDiMisuraPartenza = _unitaDiMisuraPartenza.Id }
                                                       join _operazione in _context.Operazione on _operatoriDiconversione.IdOperazione equals _operazione.Id
                                                       where (_operatoriDiconversione.Id == (id ?? 0))
                                                       select new OperatoreDiConversioneViewDto
                                                       {
                                                           Id = _operatoriDiconversione.Id,
                                                           OperazioneId = _operazione.Id,
                                                           UnitaDiMisuraPartenzaId = _unitaDiMisuraPartenza.Id,
                                                           UnitaDiMisuraArrivoId = _unitaDiMisuraArrivo.Id,
                                                           FattoreDiConversione = _operatoriDiconversione.FattoreDiConversione.HasValue ? _operatoriDiconversione.FattoreDiConversione.ToString() : "0",
                                                           UnitaDiMisuraArrivoDecrizione = $"{_unitaDiMisuraArrivo.Nome} ({_unitaDiMisuraArrivo.Simbolo})",
                                                           UnitaDiMisuraPartenzaDecrizione = $"{_unitaDiMisuraPartenza.Nome} ({_unitaDiMisuraPartenza.Simbolo})",
                                                           OperazioneDecrizione = _operazione.OperatoreDiConversione
                                                       }).FirstOrDefaultAsync();

            if (operatoreDiconversioneViewDTO == null)
            {
                return null;
            }

            var unitaDiMisura = (from _unitaDiMisura in _context.UnitaMisura
                                 select new
                                 {
                                     _unitaDiMisura.Id,
                                     Nome = $"{_unitaDiMisura.Nome} ({_unitaDiMisura.Simbolo})"
                                 }).ToList();

            operatoreDiconversioneViewDTO.UnitaDiMisuraArrivo = new SelectList(unitaDiMisura, "Id", "Nome", operatoreDiconversioneViewDTO.UnitaDiMisuraArrivoId);
            operatoreDiconversioneViewDTO.UnitaDiMisuraPartenza = new SelectList(unitaDiMisura, "Id", "Nome", operatoreDiconversioneViewDTO.UnitaDiMisuraPartenzaId);
            operatoreDiconversioneViewDTO.Operazione = new SelectList(_context.Operazione.ToList(), "Id", "OperatoreDiConversione");

            return operatoreDiconversioneViewDTO;
        }

        // GET
        public IActionResult Index()
        {
            //ViewBag.Gruppi = _context.Gruppo.ToList();
            ViewData["Header"] = HeaderClassBuilders.OperatoreDiConversione.Index(_sharedLocalizer, _basePath);

            return View();
        }

        //GET
        public IActionResult Create()
        {
            ViewData["Header"] = HeaderClassBuilders.OperatoreDiConversione.Create(_sharedLocalizer, _basePath);
            var operatoreDiConversioneViewDto = CreateOperatoreDiConversioneViewDto();

            return View(operatoreDiConversioneViewDto);
        }

        // POST: Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UnitaDiMisuraPartenzaId,UnitaDiMisuraArrivoId,OperazioneId, FattoreDiConversione")] OperatoreDiConversioneViewDto operatoreDiConversioneViewDto)
        {
            if (ModelState.IsValid)
            {
                var operatoreDiConversione = new Data.OperatoreDiConversione
                {
                    IdUnitaDiMisuraPartenza = operatoreDiConversioneViewDto.UnitaDiMisuraPartenzaId,
                    IdUnitaDiMisuraArrivo = operatoreDiConversioneViewDto.UnitaDiMisuraArrivoId,
                    IdOperazione = operatoreDiConversioneViewDto.OperazioneId
                };

                var fattoreParsed = decimal.TryParse(operatoreDiConversioneViewDto.FattoreDiConversione.Replace(".", ","), out var result);
                if (!fattoreParsed)
                {
                    result = 0;
                }

                operatoreDiConversione.FattoreDiConversione = result;

                _context.Add(operatoreDiConversione);

                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            var newOperatoreDiConversioneViewDto = CreateOperatoreDiConversioneViewDto();

            return View(newOperatoreDiConversioneViewDto);
        }

        // GET: CampiMA/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            ViewData["Header"] = HeaderClassBuilders.OperatoreDiConversione.Edit(_sharedLocalizer, _basePath);

            if (id == null)
            {
                return NotFound();
            }

            var operatoreDiConversioneViewDto = await GetOperatoreDiConversioneById(id);
            if (operatoreDiConversioneViewDto == null)
            {
                return NotFound();
            }

            return View(operatoreDiConversioneViewDto);
        }

        // POST: CampiMA/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UnitaDiMisuraPartenzaId,UnitaDiMisuraArrivoId,OperazioneId, FattoreDiConversione")] OperatoreDiConversioneViewDto operatoreDiConversioneViewDto)
        {
            if (id != operatoreDiConversioneViewDto.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View(operatoreDiConversioneViewDto);
            }

            var operatoreDiConversione = await _context.OperatoriDiConversione.FindAsync(id);

            if (operatoreDiConversione == null)
            {
                return NotFound();
            }


            operatoreDiConversione.IdUnitaDiMisuraPartenza = operatoreDiConversioneViewDto.UnitaDiMisuraPartenzaId;
            operatoreDiConversione.IdUnitaDiMisuraArrivo = operatoreDiConversioneViewDto.UnitaDiMisuraArrivoId;
            operatoreDiConversione.IdOperazione = operatoreDiConversioneViewDto.OperazioneId;

            var fattoreParsed = decimal.TryParse(operatoreDiConversioneViewDto.FattoreDiConversione.Replace(".", ","), out var result);
            if (!fattoreParsed)
            {
                result = 0;
            }

            operatoreDiConversione.FattoreDiConversione = result;

            _context.Update(operatoreDiConversione);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // GET
        public async Task<IActionResult> Delete(int? id)
        {
            ViewData["Header"] = HeaderClassBuilders.OperatoreDiConversione.Delete(_sharedLocalizer, _basePath);

            if (id == null)
            {
                return NotFound();
            }

            var operatoreDiConversioneViewDto = await GetOperatoreDiConversioneById(id);

            if (operatoreDiConversioneViewDto == null)
            {
                return NotFound();
            }

            return View(operatoreDiConversioneViewDto);
        }

        // POST
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var operatoreDiConversione = await _context.OperatoriDiConversione.FindAsync(id);

            if (operatoreDiConversione == null)
            {
                return NotFound();
            }

            _context.OperatoriDiConversione.Remove(operatoreDiConversione);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // GET
        public async Task<IActionResult> Details(int? id)
        {
            ViewData["Header"] = HeaderClassBuilders.OperatoreDiConversione.Details(_sharedLocalizer, _basePath);

            if (id == null)
            {
                return NotFound();
            }

            var operatoreDiConversione = await GetOperatoreDiConversioneById(id);

            if (operatoreDiConversione == null)
            {
                return NotFound();
            }

            return View(operatoreDiConversione);
        }

    }
}
