// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Dynamic;
using System.Globalization;
using System.Security.Claims;
using System.Text.RegularExpressions;
using Elyze.Data;
using Elyze.Helpers;
using Elyze.Helpers.DataTables;
using Elyze.Helpers.Headers;
using Elyze.Models.Gen;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Localization;
using Microsoft.EntityFrameworkCore;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace Elyze.Controllers
{
    public class DynamicProperties : DynamicObject
    {
        private DynamicProperties() { }

        private Dictionary<string, object> dictionary = new Dictionary<string, object>();

        public static DynamicProperties LoadFromDictionary(IDictionary<string, object> Dictionary)
        {
            dynamic dprop = new DynamicProperties();
            foreach (var item in Dictionary)
            {
                dprop.dictionary.Add(item.Key.ToLower(), item.Value);
            }
            return dprop;
        }
        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            string name = binder.Name.ToLower();
            Console.WriteLine("Trying to get " + name);
            return this.dictionary.TryGetValue(name, out result);
        }
        public override bool TrySetMember(SetMemberBinder binder, object value)
        {
            Console.WriteLine("TrySetMember: " + value.ToString());
            this.dictionary[binder.Name.ToLower()] = value;
            return true;
        }
    }

    public class GenController : Controller
    {

        private readonly ElyzeContext _context;
        private readonly IHtmlLocalizer<SharedResource> _sharedLocalizer;
        private readonly IConfiguration _configuration;
        private readonly UserManager<AspNetUsers> _userManager;
        private readonly string _basePath;

        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment _hostingEnvironment;
        private readonly IClaimGetterService _claimGetterService;

        public GenController(ElyzeContext context,
            IHtmlLocalizer<SharedResource> sharedLocalizer,
            IConfiguration configuration,
            UserManager<AspNetUsers> userManager,
            IHttpContextAccessor httpContextAccessor,
            Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnvironment,
            IClaimGetterService claimGetterService)
        {
            _context = context;
            _sharedLocalizer = sharedLocalizer;
            _configuration = configuration;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
            _hostingEnvironment = hostingEnvironment;
            _claimGetterService = claimGetterService;
            _basePath = configuration.GetSection("Host:BaseHostPath").Value;

        }

        [HttpPost]
        public async Task<IActionResult> GetInserimentiDataTable()
        {
            var form = HelpersDataTables.ContextToDatatable(Request);

            var idSocieta = Request.Form["IdSocieta"].ToString();
            var startDate = Request.Form["StartDate"].ToString();
            var endDate = Request.Form["EndDate"].ToString();


            DataTableResult result = new()
            {
                Draw = form.Draw,
                RecordsTotal = await _context.InserimentiFissi.Where(x => x.IdMicroArea == Convert.ToInt32(Request.Form["IdMicroArea"])).CountAsync()
            };

            var query = from inserimento in _context.InserimentiFissi
                        join reparto in _context.Reparto on inserimento.RepartoId equals reparto.Id
                        join societa in _context.Societa on inserimento.SocietaId equals societa.Id
                        join microarea in _context.MicroArea on inserimento.IdMicroArea equals microarea.Id
                        where inserimento.IdMicroArea == Convert.ToInt32(Request.Form["IdMicroArea"]) &&
                            (idSocieta == "0" || inserimento.SocietaId == Convert.ToInt32(idSocieta)) &&
                            (startDate == "" || inserimento.DataInizio >= Convert.ToDateTime(startDate)) &&
                            (endDate == "" || inserimento.DataFine <= Convert.ToDateTime(endDate))
                        select new
                        {
                            Stato = inserimento.Stato,
                            IdSocieta = societa.Id,
                            Societa = societa.Descrizione,
                            IdReparto = reparto.Id,
                            Reparto = reparto.Nome,
                            inserimento.Id,
                            inserimento.IdInserimento,
                            DataInizio = inserimento.DataInizio.Value.ToShortDateString(),
                            DataFine = inserimento.DataFine.Value.ToShortDateString(),
                            UtenteInserimento = inserimento.IdUtente,
                            NomeUtenteInserimento = "",
                            UtenteValidatore = inserimento.IdValidatore,
                            NomeUtenteValidatore = "",
                            DataInserimento = inserimento.DataInserimento.Value.ToShortDateString(),
                            DataValidazione = inserimento.DataValidazione.Value.ToShortDateString(),
                        };

            if (_sharedLocalizer["Start_Date"].Value == form.SortColumn)
            {
                query = form.SortColumnDirection == "asc" ? query.OrderBy(x => x.DataInizio) : query.OrderByDescending(x => x.DataInizio);
            }
            else
            if (_sharedLocalizer["End_Date"].Value == form.SortColumn)
            {
                query = form.SortColumnDirection == "asc" ? query.OrderBy(x => x.DataFine) : query.OrderByDescending(x => x.DataFine);
            }
            else
            {
                query = query.OrderBy(x => x.Id);
            }

            var dataset1 = await query.ToListAsync();
            var dataset2 = new List<object>();

            List<IDictionary<string, object>> dictionariedDataSet = new List<IDictionary<string, object>>();

            foreach (var item in dataset1)
            {
                var userCompilatore = await _userManager.FindByIdAsync(item.UtenteInserimento);
                var nameCompilatore = userCompilatore.Name + " " + userCompilatore.Surname;
                if (userCompilatore != null)
                {
                    nameCompilatore = userCompilatore.Name + " " + userCompilatore.Surname;
                }

                var userValidatore = await _userManager.FindByIdAsync(item.UtenteValidatore);
                var nameValidatore = "";
                if (userValidatore != null)
                {
                    nameValidatore = userValidatore.Name + " " + userValidatore.Surname;
                }




                var oRqf = Request.HttpContext.Features.Get<IRequestCultureFeature>();
                var oCulture = oRqf.RequestCulture.Culture;
                var sLanguage = "IT";
                if (oCulture.Name.ToLower().Contains("en"))
                {
                    sLanguage = "EN";
                }
                IDictionary<string, object> items = new Dictionary<string, object>();
                if (sLanguage == "IT")
                {
                    items = new Dictionary<string, object>
                    {
                    { "Stato", item.Stato },
                    { "Id", item.Id.ToString() },
                    { "Data Inizio", item.DataInizio },
                    { "Data Fine", item.DataFine },
                    { "Id Societa", item.IdSocieta },
                    { "Societa", item.Societa },
                    { "Id Reparto", item.IdReparto },
                    { "Reparto", item.Reparto },
                    { "Data Validazione", item.DataValidazione },
                    { "IdInserimento", item.IdInserimento.ToString() },
                    { "Utente Inserimento", item.UtenteInserimento },
                    { "Nome Utente Inserimento", nameCompilatore},
                    { "Utente Validatore", item.UtenteValidatore },
                    { "Nome Utente Validatore", nameValidatore},
                    { "Data Inserimento", item.DataInserimento },
                };
                }
                else
                {
                    items = new Dictionary<string, object>
                    {
                        { "State", item.Stato },
                        { "Id", item.Id.ToString() },
                        { "Date Start", item.DataInizio },
                        { "Date End", item.DataFine },
                        { "Id Company", item.IdSocieta },
                        { "Company", item.Societa },
                        { "Id Department", item.IdReparto },
                        { "Department", item.Reparto },
                        { "Date Validation", item.DataValidazione },
                        { "IdInserimento", item.IdInserimento.ToString() },
                        { "User Insert", item.UtenteInserimento },
                        { "Name UserInsert", nameCompilatore},
                        { "User Validator", item.UtenteValidatore },
                        { "Name UserValidator", nameValidatore},
                        { "Date Insert", item.DataInserimento },
                    };
                }

                var queryCampiDinamici = from InserimentiDinamici in _context.Inserimenti
                                         join InserimentiFissi in _context.InserimentiFissi on InserimentiDinamici.IdInserimento equals InserimentiFissi.IdInserimento
                                         join CampiMa in _context.CampiMa on InserimentiDinamici.IdCampo equals CampiMa.Id
                                         where InserimentiDinamici.IdInserimento == item.IdInserimento
                                         select new
                                         {
                                             InserimentiDinamici.ValoreCampo,
                                             InserimentiDinamici.ValoreValidato,
                                             Descrizione = CampiMa.Descrizione ?? ""
                                         };

                var dataSetDinamici = await queryCampiDinamici.ToListAsync();

                if (sLanguage == "IT")
                {
                    foreach (var campoDinamico in queryCampiDinamici)
                    {
                        items.Add(campoDinamico.Descrizione + " Non Validata", campoDinamico.ValoreCampo);
                        items.Add(campoDinamico.Descrizione + " Validata", campoDinamico.ValoreValidato);
                    }

                    dictionariedDataSet.Add(items);
                }
                else
                {
                    foreach (var campoDinamico in queryCampiDinamici)
                    {
                        items.Add(campoDinamico.Descrizione + " Not Validated", campoDinamico.ValoreCampo);
                        items.Add(campoDinamico.Descrizione + " Validated", campoDinamico.ValoreValidato);
                    }

                    dictionariedDataSet.Add(items);
                }

            }

            var found = false;
            foreach (var dictionary in dictionariedDataSet)
            {

                foreach (var elementOfDictationary in dictionary)
                {
                    if (elementOfDictationary.Value != null && elementOfDictationary.Value.ToString().ToLower().Contains(form.SearchValue.ToLower()))
                    {
                        found = true;
                        break;
                    }
                }

                if (found)
                {
                    dataset2.Add(DictionaryToObject(dictionary));
                    found = false;
                }
            }

            result.RecordsFiltered = dataset2.Count;
            result.Data = dataset2.Skip(form.Start).Take(form.Length).ToList();

            return Json(result);
        }


        private async Task<List<object>> GetDataSetInserimentoById(int idMicroArea, bool forForm = false, DataTableForm form = null)
        {
            var query = from inserimento in _context.InserimentiFissi
                        join reparto in _context.Reparto on inserimento.RepartoId equals reparto.Id
                        join societa in _context.Societa on inserimento.SocietaId equals societa.Id
                        join microarea in _context.MicroArea on inserimento.IdMicroArea equals microarea.Id
                        where inserimento.IdMicroArea == (forForm ? Convert.ToInt32(Request.Form["IdMicroArea"]) : idMicroArea)
                        select new
                        {
                            Stato = inserimento.Stato,
                            IdSocieta = societa.Id,
                            Societa = societa.Descrizione,
                            IdReparto = reparto.Id,
                            Reparto = reparto.Nome,
                            inserimento.Id,
                            inserimento.IdInserimento,
                            DataInizio = inserimento.DataInizio.Value.ToShortDateString(),
                            DataFine = inserimento.DataFine.Value.ToShortDateString(),
                            UtenteInserimento = inserimento.IdUtente,
                            NomeUtenteInserimento = "",
                            UtenteValidatore = inserimento.IdValidatore,
                            NomeUtenteValidatore = "",
                            DataInserimento = inserimento.DataInserimento.Value.ToShortDateString(),
                            DataValidazione = inserimento.DataValidazione.Value.ToShortDateString(),
                        };

            if (forForm)
            {
                if (_sharedLocalizer["Start_Date"].Value == form.SortColumn)
                {
                    query = form.SortColumnDirection == "asc" ? query.OrderBy(x => x.DataInizio) : query.OrderByDescending(x => x.DataInizio);
                }
                else
                if (_sharedLocalizer["End_Date"].Value == form.SortColumn)
                {
                    query = form.SortColumnDirection == "asc" ? query.OrderBy(x => x.DataFine) : query.OrderByDescending(x => x.DataFine);
                }
                else
                {
                    query = query.OrderBy(x => x.Id);
                }
            }



            var dataset1 = await query.ToListAsync();
            var dataset2 = new List<object>();

            List<IDictionary<string, object>> dictionariedDataSet = new List<IDictionary<string, object>>();

            foreach (var item in dataset1)
            {
                var userCompilatore = await _userManager.FindByIdAsync(item.UtenteInserimento);
                var nameCompilatore = userCompilatore.Name + " " + userCompilatore.Surname;
                if (userCompilatore != null)
                {
                    nameCompilatore = userCompilatore.Name + " " + userCompilatore.Surname;
                }

                var userValidatore = await _userManager.FindByIdAsync(item.UtenteValidatore);
                var nameValidatore = "";
                if (userValidatore != null)
                {
                    nameValidatore = userValidatore.Name + " " + userValidatore.Surname;
                }

                var oRqf = Request.HttpContext.Features.Get<IRequestCultureFeature>();
                var oCulture = oRqf.RequestCulture.Culture;
                var sLanguage = "IT";
                if (oCulture.Name.ToLower().Contains("en"))
                {
                    sLanguage = "EN";
                }
                IDictionary<string, object> items = new Dictionary<string, object>();
                if (sLanguage == "IT")
                {
                    items = new Dictionary<string, object>
                    {
                    { "Stato", item.Stato },
                    { "Id", item.Id.ToString() },
                    { "Data Inizio", item.DataInizio },
                    { "Data Fine", item.DataFine },
                    { "Id Societa", item.IdSocieta },
                    { "Societa", item.Societa },
                    { "Id Reparto", item.IdReparto },
                    { "Reparto", item.Reparto },
                    { "Data Validazione", item.DataValidazione },
                    { "IdInserimento", item.IdInserimento.ToString() },
                    { "Utente Inserimento", item.UtenteInserimento },
                    { "Nome Utente Inserimento", nameCompilatore},
                    { "Utente Validatore", item.UtenteValidatore },
                    { "Nome Utente Validatore", nameValidatore},
                    { "Data Inserimento", item.DataInserimento },
                };
                }
                else
                {
                    items = new Dictionary<string, object>
                    {
                        { "State", item.Stato },
                        { "Id", item.Id.ToString() },
                        { "Date Start", item.DataInizio },
                        { "Date End", item.DataFine },
                        { "Id Company", item.IdSocieta },
                        { "Company", item.Societa },
                        { "Id Department", item.IdReparto },
                        { "Department", item.Reparto },
                        { "Date Validation", item.DataValidazione },
                        { "IdInserimento", item.IdInserimento.ToString() },
                        { "User Insert", item.UtenteInserimento },
                        { "Name UserInsert", nameCompilatore},
                        { "User Validator", item.UtenteValidatore },
                        { "Name UserValidator", nameValidatore},
                        { "Date Insert", item.DataInserimento },
                    };
                }

                var queryCampiDinamici = from InserimentiDinamici in _context.Inserimenti
                                         join InserimentiFissi in _context.InserimentiFissi on InserimentiDinamici.IdInserimento equals InserimentiFissi.IdInserimento
                                         join CampiMa in _context.CampiMa on InserimentiDinamici.IdCampo equals CampiMa.Id
                                         where InserimentiDinamici.IdInserimento == item.IdInserimento
                                         select new
                                         {
                                             InserimentiDinamici.ValoreCampo,
                                             InserimentiDinamici.ValoreValidato,
                                             Descrizione = CampiMa.Descrizione ?? "" + " - " + InserimentiDinamici.IdCampo + " | "// ValidateForDataTable(CampiMa.Descrizione ?? "") + " - " + InserimentiDinamici.IdCampo + "|"
                                         };

                var dataSetDinamici = await queryCampiDinamici.ToListAsync();

                if (sLanguage == "IT")
                {
                    foreach (var campoDinamico in queryCampiDinamici)
                    {
                        items.Add(campoDinamico.Descrizione + " Non Validata", campoDinamico.ValoreCampo);
                        items.Add(campoDinamico.Descrizione + " Validata", campoDinamico.ValoreValidato);
                    }

                    dictionariedDataSet.Add(items);
                }
                else
                {
                    foreach (var campoDinamico in queryCampiDinamici)
                    {
                        items.Add(campoDinamico.Descrizione + " Not Validated", campoDinamico.ValoreCampo);
                        items.Add(campoDinamico.Descrizione + " Validated", campoDinamico.ValoreValidato);
                    }

                    dictionariedDataSet.Add(items);
                }

            }
            if (forForm)
            {
                var found = false;
                foreach (var dictionary in dictionariedDataSet)
                {

                    foreach (var elementOfDictationary in dictionary)
                    {
                        if (elementOfDictationary.Value != null && elementOfDictationary.Value.ToString().ToLower().Contains(form.SearchValue.ToLower()))
                        {
                            found = true;
                            break;
                        }
                    }

                    if (found)
                    {
                        dataset2.Add(DictionaryToObject(dictionary));
                        found = false;
                    }
                }
            }
            else
            {
                foreach (var dictionary in dictionariedDataSet)
                {
                    dataset2.Add(DictionaryToObject(dictionary));
                }

            }


            return dataset2;
        }


        private static dynamic DictionaryToObject(IDictionary<String, Object> dictionary)
        {
            var expandoObj = new ExpandoObject();
            var expandoObjCollection = (ICollection<KeyValuePair<String, Object>>)expandoObj;

            foreach (var keyValuePair in dictionary)
            {
                expandoObjCollection.Add(keyValuePair);
            }
            dynamic eoDynamic = expandoObj;
            return eoDynamic;
        }


        #region String helpers
        public static string? FirstCharToLowerCase(string str)
        {
            if (!string.IsNullOrEmpty(str) && char.IsUpper(str[0]))
                return str.Length == 1 ? char.ToLower(str[0]).ToString() : char.ToLower(str[0]) + str[1..];

            return str;
        }

        public static string? ValidateForDataTable(string str)
        {
            var textInfo = new CultureInfo("en-US", false).TextInfo;
            return Regex.Replace(FirstCharToLowerCase(textInfo.ToTitleCase(str)) ?? "", @"\s+", "");
        }
        #endregion


        private async Task<List<string>> SetupHeaderDataTable(int microAreaId, bool forForm = true)
        {

            var oRqf = Request.HttpContext.Features.Get<IRequestCultureFeature>();
            var oCulture = oRqf.RequestCulture.Culture;
            var sLanguage = "IT";
            if (oCulture.Name.ToLower().Contains("en"))
            {
                sLanguage = "EN";
            }
            var tableColumns = new List<string>();
            if (forForm)
            {
                if (sLanguage == "IT")
                {
                    tableColumns = new List<string>()
                    {
                        "Stato",
                        "Societa",
                        "Reparto",
                        "Data Inserimento",
                        "IdInserimento",
                        "Id",
                    };
                }
                else
                {
                    tableColumns = new List<string>()
                    {
                        "State",
                        "Company",
                        "Department",
                        "Date Insert",
                        "Id",
                        "IdInserimento",
                    };
                }
            }
            else
            {
                if (sLanguage == "IT")
                {
                    tableColumns = new List<string>()
                    {
                        "Stato",
                        "Id",
                        "Societa",
                        "Reparto",
                        "NomeUtente Inserimento",
                        "Data Inserimento",
                        "Nome Utente Validatore",
                        "Data Validazione",
                        "Data Inizio",
                        "Data Fine",
                        "IdInserimento",
                    };
                }
                else
                {
                    tableColumns = new List<string>()
                    {
                        "State",
                        "Id",
                        "Company",
                        "Department",
                        "Name UserInsert",
                        "Date Insert",
                        "Name User Validator",
                        "Date Validation",
                        "Date Start",
                        "Date End",
                        "IdInserimento",
                    };
                }
            }
            var campimavalidati = new List<string?>();
            var campimanonvalidati = new List<string?>();
            if (sLanguage == "IT")
            {
                campimavalidati = await _context.CampiMa
                                    .Where(x => x.IdMicroArea == microAreaId)
                                    .Select(y => y.Descrizione + " Non Validata").ToListAsync();

                campimanonvalidati = await _context.CampiMa
                                        .Where(x => x.IdMicroArea == microAreaId)
                                        .Select(y => y.Descrizione + " Validata").ToListAsync();
            }
            else
            {
                campimavalidati = await _context.CampiMa
                                    .Where(x => x.IdMicroArea == microAreaId)
                                    .Select(y => ValidateForDataTable(y.Descrizione + " Not Validated")).ToListAsync();

                campimanonvalidati = await _context.CampiMa
                                        .Where(x => x.IdMicroArea == microAreaId)
                                        .Select(y => ValidateForDataTable(y.Descrizione + " Validated")).ToListAsync();
            }


            if (campimavalidati != null && campimanonvalidati != null)
            {
                for (var i = 0; i < campimavalidati.Count; i++)
                {
                    tableColumns.Add(campimavalidati[i] ?? "");
                    tableColumns.Add(campimanonvalidati[i] ?? "");
                }
            }

            tableColumns.Add("Actions");
            return tableColumns;
        }


        [HttpGet]
        public async Task<IActionResult> IndexInserimenti(int? id)
        {
            var idArea = await _context.MicroArea.Where(x => x.Id == id).Select(y => y.IdArea).FirstAsync();


            ViewData["Header"] = HeaderClassBuilders.Inserimenti.IndexPopolamentoMA(_sharedLocalizer, _basePath, (int)id, idArea);
            ViewData["BasePath"] = _basePath;
            ViewData["IdMicroArea"] = id;

            ViewBag.SetupTable = await SetupHeaderDataTable((int)id);

            ViewBag.Societa = await _context.Societa.Select(x => new { x.Id, x.Descrizione }).ToListAsync();

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> AggiungiInserimento(int id)
        {
            var idArea = await _context.MicroArea.Where(x => x.Id == id).Select(y => y.IdArea).FirstAsync();
            var user = await _userManager.FindByEmailAsync(_claimGetterService.GetUserClaim(ClaimTypes.Email));
            var userHaveValidator = await _context.UserArea.Where(x => x.IdArea == idArea && x.IdUser == user.Id && x.IdUserType == 1).AnyAsync();
            var isAdmin = await _claimGetterService.IsAdmin();

            ViewData["Header"] = HeaderClassBuilders.Inserimenti.CreateInserimento(_sharedLocalizer, _basePath, id, idArea);
            ViewData["BasePath"] = _basePath;
            ViewData["CanValidate"] = isAdmin || userHaveValidator;

            //get email from claimGetterService
            var email = user.Email;


            //get userReparto permissions and Societa with email by joining the user table
            var userReparto = await _context.UserReparto
                .Include(x => x.IdUserNavigation)
                .Where(x => x.IdUserNavigation.Email == email)
                .Select(x => x.IdReparto)
                .ToListAsync();

            var userSocieta = await _context.UserSocieta
                .Include(x => x.IdUserNavigation)
                .Where(x => x.IdUserNavigation.Email == email)
                .Select(x => x.IdSocieta)
                .ToListAsync();

            var reparti = new List<Reparto>();
            var societa = new List<Societa>();

            //if user is not in role admin filter the reparto and societa by the user permissions, else not do it
            if (!isAdmin)
            {
                reparti = await _context.Reparto.Where(x => userReparto.Contains(x.Id)).ToListAsync();
                if (reparti.Count == 0)
                {
                    if (!_context.Reparto.Any())
                    {
                        return RedirectToAction("Index", "Home", new { Redirect = "NoRepartoAtAll" });
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home", new { Redirect = "NoRepartoContactAdmin" });
                    }
                }

                societa = await _context.Societa.Where(x => userSocieta.Contains(x.Id)).ToListAsync();
                if (societa.Count == 0)
                {
                    if (!_context.Societa.Any())
                    {
                        return RedirectToAction("Index", "Home", new { Redirect = "NoSocietaAtAll" });
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home", new { Redirect = "NoSocietaContactAdmin" });
                    }
                }
            }
            else
            {
                reparti = await _context.Reparto.ToListAsync();
                if (reparti.Count == 0)
                {
                    return RedirectToAction("Index", "Reparto", new { Redirect = "NoReparto" });
                }

                societa = await _context.Societa.ToListAsync();
                if (societa.Count == 0)
                {
                    return RedirectToAction("Index", "Societa", new { Redirect = "NoSocieta" });
                }
            }


            ViewBag.Reparto = reparti;
            ViewBag.Societa = societa;

            ViewBag.TipologieCampi = await _context.TipologieCampiMicroArea.ToListAsync();
            ViewBag.UDM = await _context.UnitaMisura.ToListAsync();

            ViewBag.OperatoriDiConversione = await _context.OperatoriDiConversione.ToListAsync();

            var lastIdInserimento = 0;
            if (_context.InserimentiFissi.Any())
            {
                lastIdInserimento = _context.InserimentiFissi.Select(a => a.IdInserimento).Max() + 1;
            }

            var model = new StoriciCreateFormView();
            model.CampiFissi.IdInserimento = lastIdInserimento;
            model.CampiFissi.IdMicroArea = id;
            model.CampiFissi.Stato = 0;

            model.CampiDinamici = await _context.CampiMa
                .Where(a => a.IdMicroArea == id)
                .Select(x => new CampiDinamici
                {
                    Id = x.Id,
                    Descrizione = x.Descrizione ?? "",
                    IdTipo = x.IdTipologia,
                    IdUDM = x.IdUDM,
                    Valore = "",
                })
                .ToArrayAsync();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AggiungiInserimento(StoriciCreateFormView form, IFormCollection collection)
        {

            form.CampiFissi.Stato = collection["CampiFissi.Stato"].First() == "true" ? 1 : 0;

            using (var dbContextTransaction = _context.Database.BeginTransaction())
            {
                var listaCampiDinamici = new List<CampiDinamici>();

                foreach (var k in collection)
                {
                    if (k.Key.Contains("CampoDinamico_") && !k.Key.Contains("-Conversion"))
                    {
                        var value = k.Value.ToString();

                        var idConversionForm = $"{k.Key}-Conversion";
                        var possibleConvesion = collection.Select(x => x.Key).FirstOrDefault(x => x == idConversionForm);
                        if (possibleConvesion != null)
                        {
                            //take last char of possibleConvesion and parse into int from collection with idConversionForm
                            int idConversion = int.Parse(collection[idConversionForm].First().ToString());
                            if (idConversion != 0)
                            {
                                //take the conversion from db
                                var conversion = await _context.OperatoriDiConversione.Where(x => x.Id == idConversion).FirstOrDefaultAsync();

                                //if 1 add to value the conversion, if 2 subtract, if 3 divide, if 4 multiply
                                switch (conversion.IdOperazione)
                                {
                                    case 1:
                                        value = (int.Parse(value) + conversion.FattoreDiConversione).ToString();
                                        break;
                                    case 2:
                                        value = (int.Parse(value) - conversion.FattoreDiConversione).ToString();
                                        break;
                                    case 3:
                                        value = (int.Parse(value) / conversion.FattoreDiConversione).ToString();
                                        break;
                                    case 4:
                                        value = (int.Parse(value) * conversion.FattoreDiConversione).ToString();
                                        break;
                                    default:
                                        break;
                                }
                            }

                        }

                        Data.Inserimenti inserimentoDinamico = new()
                        {
                            IdInserimento = form.CampiFissi.IdInserimento,
                            IdCampo = int.Parse(k.Key.Replace("CampoDinamico_", "")),
                            ValoreCampo = value,
                            ValoreValidato = form.CampiFissi.Stato == 1 ? (string)value : null,
                        };

                        await _context.AddAsync(inserimentoDinamico);

                    }
                }

                var user = await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Email)?.Value);

                InserimentiFissi inserimentoFisso = new()
                {
                    IdInserimento = form.CampiFissi.IdInserimento,
                    IdMicroArea = form.CampiFissi.IdMicroArea,
                    DataFine = form.CampiFissi.DataFine,
                    DataInizio = form.CampiFissi.DataInizio,
                    Stato = form.CampiFissi.Stato,
                    DataInserimento = DateTime.Now,
                    RepartoId = form.CampiFissi.RepartoId,
                    SocietaId = form.CampiFissi.SocietaId,
                    IdValidatore = form.CampiFissi.Stato == 1 ? user.Id : null,
                    DataValidazione = form.CampiFissi.Stato == 1 ? DateTime.Now : null,
                    IdUtente = user.Id
                };

                await _context.AddAsync(inserimentoFisso);

                await _context.SaveChangesAsync();

                dbContextTransaction.Commit();

            }

            return RedirectToAction(nameof(IndexInserimenti), new { Id = form.CampiFissi.IdMicroArea });
        }

        [HttpGet]
        public async Task<IActionResult> EditInserimento(int? id)
        {
            var user = await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Email)?.Value);

            var model = await _context.InserimentiFissi
                .Where(x => x.IdInserimento == id)
                .Select(x => new StoriciCreateFormView
                {
                    CampiFissi = new CampiFissi
                    {
                        IdInserimento = x.IdInserimento,
                        IdMicroArea = x.IdMicroArea,
                        DataFine = x.DataFine,
                        DataInizio = x.DataInizio,
                        Stato = x.Stato,
                        RepartoId = x.RepartoId,
                        SocietaId = x.SocietaId,
                    },
                    CampiDinamici = new CampiDinamici[] { }
                })
                .FirstOrDefaultAsync();

            if (model == null)
            {
                return NotFound();
            }


            var idArea = await _context.MicroArea.Where(x => x.Id == model.CampiFissi.IdMicroArea).Select(y => y.IdArea).FirstAsync();
            var userHaveValidator = await _context.UserArea.Where(x => x.IdArea == idArea && x.IdUser == user.Id && x.IdUserType == 1).AnyAsync();
            var isAdmin = await _claimGetterService.IsAdmin();


            ViewData["Header"] = HeaderClassBuilders.Inserimenti.EditInserimento(_sharedLocalizer, _basePath, model.CampiFissi.IdMicroArea, idArea);
            ViewData["BasePath"] = _basePath;
            ViewData["CanValidate"] = isAdmin || userHaveValidator;

            var email = user.Email;

            var userReparto = await _context.UserReparto
               .Include(x => x.IdUserNavigation)
               .Where(x => x.IdUserNavigation.Email == email)
               .Select(x => x.IdReparto)
               .ToListAsync();

            var userSocieta = await _context.UserSocieta
                .Include(x => x.IdUserNavigation)
                .Where(x => x.IdUserNavigation.Email == email)
                .Select(x => x.IdSocieta)
                .ToListAsync();

            //get the role of the user

            //if user is not in role admin filter the reparto and societa by the user permissions, else not do it
            if (isAdmin)
            {
                ViewBag.Reparto = await _context.Reparto.ToListAsync();
                ViewBag.Societa = await _context.Societa.ToListAsync();
            }
            else
            {
                ViewBag.Reparto = await _context.Reparto.Where(x => userReparto.Contains(x.Id)).ToListAsync();
                ViewBag.Societa = await _context.Societa.Where(x => userSocieta.Contains(x.Id)).ToListAsync();
            }

            ViewBag.TipologieCampi = await _context.TipologieCampiMicroArea.ToListAsync();
            ViewBag.UDM = await _context.UnitaMisura.ToListAsync();

            ViewBag.OperatoriDiConversione = await _context.OperatoriDiConversione.ToListAsync();

            model.CampiDinamici = await _context.Inserimenti
                .Include(o => o.IdCampoNavigation)
                .Where(x => x.IdInserimento == id)
                .Select(x => new CampiDinamici
                {
                    Id = x.IdCampo,
                    Descrizione = x.IdCampoNavigation.Descrizione ?? "",
                    IdTipo = x.IdCampoNavigation.IdTipologia,
                    IdUDM = x.IdCampoNavigation.IdUDM,
                    Valore = x.ValoreCampo ?? ""
                })
                .ToArrayAsync();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditInserimento(StoriciCreateFormView form, IFormCollection collection)
        {

            using (var dbContextTransaction = _context.Database.BeginTransaction())
            {
                var listaCampiDinamici = await _context.Inserimenti.Where(x => x.IdInserimento == form.CampiFissi.IdInserimento).ToListAsync();

                foreach (var k in collection)
                {
                    if (k.Key.Contains("CampoDinamico_") && !k.Key.Contains("-Conversion"))
                    {
                        var value = k.Value.ToString();

                        var idConversionForm = $"{k.Key}-Conversion";
                        var possibleConvesion = collection.Select(x => x.Key).FirstOrDefault(x => x == idConversionForm);
                        if (possibleConvesion != null)
                        {
                            //take last char of possibleConvesion and parse into int from collection with idConversionForm
                            int idConversion = int.Parse(collection[idConversionForm].First().ToString());
                            if (idConversion != 0)
                            {
                                //take the conversion from db
                                var conversion = await _context.OperatoriDiConversione.Where(x => x.Id == idConversion).FirstOrDefaultAsync();

                                //if 1 add to value the conversion, if 2 subtract, if 3 divide, if 4 multiply
                                switch (conversion.IdOperazione)
                                {
                                    case 1:
                                        value = (int.Parse(value) + conversion.FattoreDiConversione).ToString();
                                        break;
                                    case 2:
                                        value = (int.Parse(value) - conversion.FattoreDiConversione).ToString();
                                        break;
                                    case 3:
                                        value = (int.Parse(value) / conversion.FattoreDiConversione).ToString();
                                        break;
                                    case 4:
                                        value = (int.Parse(value) * conversion.FattoreDiConversione).ToString();
                                        break;
                                    default:
                                        break;
                                }
                            }

                        }

                        var oldField = "";
                        foreach (var k2 in collection)
                        {
                            if (k2.Key == $"old_{k.Key}")
                            {
                                oldField = k2.Value;
                            }
                        }

                        if (!k.Key.Contains("old"))
                        {
                            var inserimentoToEdit = listaCampiDinamici.Where(x => x.IdCampo == int.Parse(k.Key.Replace("CampoDinamico_", ""))).FirstOrDefault();

                            inserimentoToEdit.ValoreCampo = form.CampiFissi.Stato == 1 ? oldField : (string)k.Value;
                            inserimentoToEdit.ValoreValidato = form.CampiFissi.Stato == 1 ? (string)k.Value : null;

                            _context.Update(inserimentoToEdit);
                        }

                    }
                }

                var user = await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Email)?.Value);

                var inserimentoFisso = await _context.InserimentiFissi.Where(x => x.IdInserimento == form.CampiFissi.IdInserimento).FirstOrDefaultAsync();

                if (inserimentoFisso == null)
                {
                    return NotFound();
                }

                inserimentoFisso.IdInserimento = form.CampiFissi.IdInserimento;
                inserimentoFisso.IdMicroArea = form.CampiFissi.IdMicroArea;
                inserimentoFisso.DataFine = form.CampiFissi.DataFine;
                inserimentoFisso.DataInizio = form.CampiFissi.DataInizio;
                inserimentoFisso.Stato = form.CampiFissi.Stato;
                inserimentoFisso.DataInserimento = DateTime.Now;
                inserimentoFisso.RepartoId = form.CampiFissi.RepartoId;
                inserimentoFisso.SocietaId = form.CampiFissi.SocietaId;
                inserimentoFisso.IdValidatore = form.CampiFissi.Stato == 1 ? user.Id : null;
                inserimentoFisso.DataValidazione = form.CampiFissi.Stato == 1 ? DateTime.Now : null;
                inserimentoFisso.IdUtente = user.Id;

                _context.Update(inserimentoFisso);

                await _context.SaveChangesAsync();

                dbContextTransaction.Commit();

            }

            return RedirectToAction(nameof(IndexInserimenti), new { Id = form.CampiFissi.IdMicroArea });
        }

        [HttpGet]
        public async Task<IActionResult> DetailsInserimento(int? id)
        {
            var user = await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Email)?.Value);
            var isAdmin = await _claimGetterService.IsAdmin();

            var model = await _context.InserimentiFissi
                .Where(x => x.IdInserimento == id)
                .Select(x => new StoriciCreateFormView
                {
                    CampiFissi = new CampiFissi
                    {
                        IdInserimento = x.IdInserimento,
                        IdMicroArea = x.IdMicroArea,
                        DataFine = x.DataFine,
                        DataInizio = x.DataInizio,
                        Stato = x.Stato,
                        RepartoId = x.RepartoId,
                        SocietaId = x.SocietaId,
                    },
                    CampiDinamici = new CampiDinamici[] { }
                })
                .FirstOrDefaultAsync();

            if (model == null)
            {
                return NotFound();
            }


            var idArea = await _context.MicroArea.Where(x => x.Id == model.CampiFissi.IdMicroArea).Select(y => y.IdArea).FirstAsync();
            var userHaveValidator = await _context.UserArea.Where(x => x.IdArea == idArea && x.IdUser == user.Id && x.IdUserType == 1).AnyAsync();

            ViewData["Header"] = HeaderClassBuilders.Inserimenti.DetailsInserimento(_sharedLocalizer, _basePath, model.CampiFissi.IdMicroArea, idArea);
            ViewData["BasePath"] = _basePath;
            ViewData["CanValidate"] = isAdmin || userHaveValidator;

            ViewBag.Reparto = await _context.Reparto.ToListAsync();
            ViewBag.Societa = await _context.Societa.ToListAsync();

            ViewBag.TipologieCampi = await _context.TipologieCampiMicroArea.ToListAsync();
            ViewBag.UDM = await _context.UnitaMisura.ToListAsync();

            model.CampiDinamici = await _context.Inserimenti
                .Include(o => o.IdCampoNavigation)
                .Where(x => x.IdInserimento == id)
                .Select(x => new CampiDinamici
                {
                    Id = x.IdCampo,
                    Descrizione = x.IdCampoNavigation.Descrizione ?? "",
                    IdTipo = x.IdCampoNavigation.IdTipologia,
                    IdUDM = x.IdCampoNavigation.IdUDM,
                    Valore = x.ValoreCampo ?? ""
                })
                .ToArrayAsync();

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> RemoveInserimento(int? id)
        {
            var user = await _userManager.FindByEmailAsync(_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Email)?.Value);
            var isAdmin = await _claimGetterService.IsAdmin();

            var model = await _context.InserimentiFissi
                .Where(x => x.IdInserimento == id)
                .Select(x => new StoriciCreateFormView
                {
                    CampiFissi = new CampiFissi
                    {
                        IdInserimento = x.IdInserimento,
                        IdMicroArea = x.IdMicroArea,
                        DataFine = x.DataFine,
                        DataInizio = x.DataInizio,
                        Stato = x.Stato,
                        RepartoId = x.RepartoId,
                        SocietaId = x.SocietaId,
                    },
                    CampiDinamici = new CampiDinamici[] { }
                })
                .FirstOrDefaultAsync();

            if (model == null)
            {
                return NotFound();
            }


            var idArea = await _context.MicroArea.Where(x => x.Id == model.CampiFissi.IdMicroArea).Select(y => y.IdArea).FirstAsync();
            var userHaveValidator = await _context.UserArea.Where(x => x.IdArea == idArea && x.IdUser == user.Id && x.IdUserType == 1).AnyAsync();

            ViewData["Header"] = HeaderClassBuilders.Inserimenti.DeleteInserimento(_sharedLocalizer, _basePath, model.CampiFissi.IdMicroArea, idArea);
            ViewData["BasePath"] = _basePath;
            ViewData["CanValidate"] = isAdmin || userHaveValidator;

            ViewBag.Reparto = await _context.Reparto.ToListAsync();
            ViewBag.Societa = await _context.Societa.ToListAsync();

            ViewBag.TipologieCampi = await _context.TipologieCampiMicroArea.ToListAsync();
            ViewBag.UDM = await _context.UnitaMisura.ToListAsync();

            model.CampiDinamici = await _context.Inserimenti
                .Include(o => o.IdCampoNavigation)
                .Where(x => x.IdInserimento == id)
                .Select(x => new CampiDinamici
                {
                    Id = x.IdCampo,
                    Descrizione = x.IdCampoNavigation.Descrizione ?? "",
                    IdTipo = x.IdCampoNavigation.IdTipologia,
                    IdUDM = x.IdCampoNavigation.IdUDM,
                    Valore = x.ValoreCampo ?? ""
                })
                .ToArrayAsync();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> RemoveInserimento(int? id, IFormCollection form)
        {
            var inserimenti = await _context.InserimentiFissi.Where(x => x.IdInserimento == id).FirstOrDefaultAsync();
            var inserimentiDinamici = await _context.Inserimenti.Where(x => x.IdInserimento == id).ToListAsync();

            id = inserimenti.IdMicroArea;

            using (var dbContextTransaction = _context.Database.BeginTransaction())
            {
                try
                {
                    _context.InserimentiFissi.Remove(inserimenti);
                    _context.Inserimenti.RemoveRange(inserimentiDinamici);
                    await _context.SaveChangesAsync();
                    dbContextTransaction.Commit();
                }
                catch (Exception ex)
                {
                    dbContextTransaction.Rollback();
                    throw ex;
                }
            }

            return RedirectToAction(nameof(IndexInserimenti), new { Id = id });
        }

        [HttpGet]
        public async Task<IActionResult> ImportaInserimenti()
        {
            ViewData["Header"] = HeaderClassBuilders.Inserimenti.ImportaInserimenti(_sharedLocalizer, _basePath);
            ViewData["BasePath"] = _basePath;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ImportaInserimenti([FromForm(Name = "files")] List<IFormFile> files)
        {
            try
            {
                ViewData["Header"] = HeaderClassBuilders.Inserimenti.ImportaInserimenti(_sharedLocalizer, _basePath);
                ViewData["BasePath"] = _basePath;

                var userId = _claimGetterService.UserId();

                //read file position zero from "files"
                IFormFile file = files[0];
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    file.CopyTo(memoryStream);
                    memoryStream.Position = 0;

                    XSSFWorkbook hssfwb = new XSSFWorkbook(memoryStream);
                    int numberOfSheet = hssfwb.NumberOfSheets;
                    for (int i = 0; i < numberOfSheet; i++)
                    {
                        ISheet sheet = hssfwb.GetSheetAt(i);

                        string sheetName = sheet.SheetName;
                        int cellCount = sheet.GetRow(0).LastCellNum;

                        //Recupero l'id della micro area
                        int idMicroArea = _context.MicroArea
                            .Where(w => w.Descrizione == sheetName)
                            .Select(s => s.Id).FirstOrDefault();

                        //Recupero l'anagrafica dei campi dinamici della micro area
                        List<CampiMa> campiMa = _context.CampiMa
                            .Where(w => w.IdMicroArea == idMicroArea)
                            .ToList();

                        //Recupero l'header del file
                        List<string> positionFieldFirstRow = new List<string>();
                        IRow firstRow = sheet.GetRow(0);
                        for (int indexFirstRow = firstRow.FirstCellNum; indexFirstRow < cellCount; indexFirstRow++)
                        {
                            if (firstRow.GetCell(indexFirstRow) != null)
                            {
                                string? cell = firstRow.GetCell(indexFirstRow).ToString();
                                positionFieldFirstRow.Add(cell);
                            }
                        }

                        int lastFixInsert = _context.InserimentiFissi.OrderByDescending(o => o.IdInserimento).Select(s => s.IdInserimento).FirstOrDefault();

                        List<InserimentiFissi> inserimentiFissi = new List<InserimentiFissi>();
                        List<Inserimenti> inserimentiDinamici = new List<Inserimenti>();
                        for (int j = (sheet.FirstRowNum + 1); j <= sheet.LastRowNum; j++)
                        {
                            IRow row = sheet.GetRow(j);
                            if (row == null) continue;
                            if (row.Cells.All(d => d.CellType == CellType.Blank)) continue;

                            InserimentiFissi inserimentoFisso = new InserimentiFissi();
                            inserimentoFisso.IdMicroArea = idMicroArea;
                            inserimentoFisso.IdInserimento = lastFixInsert++;
                            inserimentoFisso.IdUtente = userId;

                            for (int idexCell = 0; idexCell < row.LastCellNum; idexCell++)
                            {
                                string? cellValue = row.GetCell(idexCell).ToString();
                                if (row.GetCell(idexCell) != null && idexCell < 4)
                                {
                                    if (idexCell == 0) { inserimentoFisso.DataInizio = DateTime.Parse(cellValue); }
                                    if (idexCell == 1) { inserimentoFisso.DataFine = DateTime.Parse(cellValue); }
                                    if (idexCell == 2)
                                    {
                                        int repartoId = _context.Reparto.Where(w => w.Nome == cellValue).Select(s => s.Id).FirstOrDefault();
                                        inserimentoFisso.RepartoId = repartoId;
                                    }
                                    if (idexCell == 3)
                                    {
                                        int societaId = _context.Societa.Where(w => w.Descrizione == cellValue).Select(s => s.Id).FirstOrDefault();
                                        inserimentoFisso.SocietaId = societaId;
                                    }
                                }

                                if (row.GetCell(idexCell) != null && idexCell >= 4)
                                {
                                    //Recupero la descrizione tramite la posizione
                                    string fieldDescrption = positionFieldFirstRow[idexCell];

                                    //Recupero l'ID
                                    int idDynamicField = campiMa
                                        .Where(w => w.Descrizione == fieldDescrption)
                                        .Select(s => s.Id)
                                        .FirstOrDefault();

                                    Inserimenti inserimentoDinamico = new Inserimenti();
                                    inserimentoDinamico.IdCampo = idDynamicField;
                                    inserimentoDinamico.ValoreCampo = cellValue;
                                    inserimentoDinamico.IdInserimento = lastFixInsert;
                                    inserimentiDinamici.Add(inserimentoDinamico);
                                }
                            }

                            inserimentiFissi.Add(inserimentoFisso);

                        }

                        _context.InserimentiFissi.AddRange(inserimentiFissi);
                        _context.Inserimenti.AddRange(inserimentiDinamici);
                        _context.SaveChanges();
                    }
                }
            }
            catch (Exception e)
            {
                throw (e);
            }

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> ExportMultiplo()
        {

            ViewData["Header"] = HeaderClassBuilders.Inserimenti.ExportMultiplo(_sharedLocalizer, _basePath);
            ViewData["BasePath"] = _basePath;

            ViewBag.Aree = await _context.Area.Select(x => new { x.Id, x.Descrizione }).ToListAsync();

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ExportMultiplo(ExportMultiplo form)
        {
            if (form == null)
            {
                return View(form);
            }

            var oRqf = Request.HttpContext.Features.Get<IRequestCultureFeature>();
            var oCulture = oRqf.RequestCulture.Culture;
            var sLanguage = "IT";
            if (oCulture.Name.ToLower().Contains("en"))
            {
                sLanguage = "EN";
            }

            var fileName = "";
            foreach (var idArea in form.IdsAree)
            {
                var nomeArea = await _context.Area.Where(x => x.IdArea == idArea).Select(x => x.Descrizione).FirstOrDefaultAsync();
                fileName += $"{nomeArea}-{idArea}_";
            }
            fileName = string.Concat(fileName.AsSpan(0, fileName.Length - 1), ".xlsx");

            byte[] bytearr;
            using (MemoryStream ms = new MemoryStream())
            {
                IWorkbook workbook = new XSSFWorkbook();

                foreach (var idArea in form.IdsAree)
                {
                    var nomeArea = await _context.Area.Where(x => x.Id == idArea).Select(x => x.Descrizione).FirstOrDefaultAsync();

                    var microaree = await _context.MicroArea.Where(x => x.IdArea == idArea).Select(x => x.Id).ToListAsync();

                    foreach (var microarea in microaree)
                    {
                        var nomeMicroArea = await _context.MicroArea.Where(x => x.Id == microarea).Select(x => x.Descrizione).FirstOrDefaultAsync();
                        ISheet excelSheet = workbook.CreateSheet($"{nomeArea}|{idArea}-{nomeMicroArea}|{microarea}");

                        var dataset = await GetDataSetInserimentoById(microarea);
                        var first = true;

                        var rowCount = 0;

                        if (dataset.Count == 0)
                        {
                            IRow row = excelSheet.CreateRow(rowCount);
                            var cells = await SetupHeaderDataTable(microarea, false);
                            //remove last cell because not needed
                            cells.RemoveAt(cells.Count - 1);

                            var cellCount = 0;
                            foreach (var cell in cells)
                            {
                                row.CreateCell(cellCount).SetCellValue(cell);
                                cellCount++;
                            }

                        }

                        foreach (var set in dataset)
                        {
                            IRow row = excelSheet.CreateRow(rowCount);
                            rowCount++;
                            var dict = new RouteValueDictionary(set);

                            var cellCount = 0;
                            if (first)
                            {
                                foreach (var prop in dict)
                                {
                                    row.CreateCell(cellCount).SetCellValue(prop.Key);
                                    cellCount++;
                                }
                                first = false;
                                row = excelSheet.CreateRow(rowCount);
                                rowCount++;
                                cellCount = 0;
                            }


                            foreach (var prop in dict)
                            {
                                if (prop.Value == null)
                                {
                                    row.CreateCell(cellCount).SetCellValue("NULL");
                                    cellCount++;
                                    continue;
                                }

                                var typeOfValue = prop.Value.GetType();

                                if (typeOfValue == typeof(string))
                                {
                                    row.CreateCell(cellCount).SetCellValue(!string.IsNullOrEmpty((string)prop.Value) ? prop.Value.ToString() : " ");
                                    cellCount++;
                                    continue;
                                }
                                else if (typeOfValue == typeof(int))
                                {
                                    row.CreateCell(cellCount).SetCellValue(int.Parse(prop.Value.ToString()));
                                    cellCount++;
                                    continue;
                                }
                                else
                                {
                                    row.CreateCell(cellCount).SetCellValue(prop.Value.ToString());
                                    cellCount++;
                                    continue;
                                }
                            }
                        }
                    }
                }
                workbook.Write(ms, false);
                bytearr = ms.ToArray();
                ms.Close();
            }


            return File(bytearr, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);

        }

        [HttpGet]
        public async Task<IActionResult> DownloadCSV()
        {

            ViewData["Header"] = HeaderClassBuilders.Inserimenti.DownloadCSV(_sharedLocalizer, _basePath);
            ViewData["BasePath"] = _basePath;

            ViewBag.Aree = await _context.Area.Select(x => new { x.Id, x.Descrizione }).ToListAsync();
            return View(new DownloadCSVFormViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> DownloadCSV(DownloadCSVFormViewModel form)
        {
            try
            {
                IRequestCultureFeature? requestCultureFeatures = Request.HttpContext.Features.Get<IRequestCultureFeature>();
                string? culture = requestCultureFeatures?.RequestCulture.Culture.Name;

                Area? area = await (from a in _context.Area
                                    join l in _context.Lingue on new { Id = a.IdLingua } equals new { Id = l.Id }
                                    where a.Id == form.IdArea
                                    && l.SiglaEstesa == culture
                                    select a)
                             .FirstOrDefaultAsync();

                List<MicroArea> microAreee = await (from microArea in _context.MicroArea
                                                    where microArea.IdArea == form.IdArea
                                                    select microArea)
                                              .ToListAsync();

                List<int> microAreeIds = microAreee
                    .Select(s => s.Id)
                    .ToList();

                List<CampiMa> campiDinamici = await (from _campiDinamici in _context.CampiMa
                                                     where microAreeIds.Contains(_campiDinamici.IdMicroArea)
                                                     select _campiDinamici)
                                                           .ToListAsync();

                byte[] bytearr;
                using (MemoryStream ms = new MemoryStream())
                {
                    IWorkbook wb = new XSSFWorkbook();
                    foreach (MicroArea microArea in microAreee)
                    {
                        ISheet sheetData = wb.CreateSheet(microArea.Descrizione);
                        ICellStyle headerStyle = GetHeaderStyle(ref wb);

                        ICreationHelper cH = wb.GetCreationHelper();
                        IRow row = sheetData.CreateRow(0);

                        ICell cell = row.CreateCell(0);
                        cell.CellStyle = headerStyle;
                        cell.SetCellValue(cH.CreateRichTextString(_sharedLocalizer["Start_Date"].Value));

                        cell = row.CreateCell(1);
                        cell.CellStyle = headerStyle;
                        cell.SetCellValue(cH.CreateRichTextString(_sharedLocalizer["End_Date"].Value));

                        cell = row.CreateCell(2);
                        cell.CellStyle = headerStyle;
                        cell.SetCellValue(cH.CreateRichTextString(_sharedLocalizer["Rep"].Value));

                        cell = row.CreateCell(3);
                        cell.CellStyle = headerStyle;
                        cell.SetCellValue(cH.CreateRichTextString(_sharedLocalizer["Societa"].Value));

                        List<CampiMa> campiMaMicroArea = campiDinamici
                            .Where(w => w.IdMicroArea == microArea.Id)
                            .ToList();

                        int indexCell = 3;
                        foreach (var campoMa in campiMaMicroArea)
                        {
                            indexCell++;

                            cell = row.CreateCell(indexCell);
                            cell.CellStyle = headerStyle;
                            cell.SetCellValue(cH.CreateRichTextString(campoMa.Descrizione));

                        }
                    }

                    wb.Write(ms, false);
                    bytearr = ms.ToArray();
                    ms.Close();
                }

                string fileName = $"{area.Descrizione}.xlsx";//"Archivio Giornaliera.xlsx";
                string mimeType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

                return File(bytearr, mimeType, fileName);
            }
            catch (Exception ex)
            {
                Dispose();
                throw (ex);
                //TODO trycarch;
            }
        }

        private static ICellStyle GetHeaderStyle(ref IWorkbook excel)
        {
            NPOI.SS.UserModel.IFont font = excel.CreateFont();
            font.IsBold = true;
            //Added this explicitly, initial font size is tiny
            font.FontHeightInPoints = 11;

            NPOI.SS.UserModel.ICellStyle style = excel.CreateCellStyle();
            style.FillForegroundColor = NPOI.SS.UserModel.IndexedColors.Grey25Percent.Index;
            style.FillPattern = NPOI.SS.UserModel.FillPattern.SolidForeground;
            style.FillBackgroundColor = NPOI.SS.UserModel.IndexedColors.Grey25Percent.Index;

            style.BorderBottom = style.BorderLeft = style.BorderRight = style.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
            style.BottomBorderColor = style.LeftBorderColor = style.RightBorderColor = style.TopBorderColor = NPOI.SS.UserModel.IndexedColors.Black.Index;

            style.SetFont(font);
            return style;
        }
    }
}
