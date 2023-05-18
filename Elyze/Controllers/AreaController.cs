// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Elyze.Data;
using Elyze.Helpers.DataTables;
using Elyze.Helpers.Headers;
using Elyze.Models.Area;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Localization;
using Microsoft.EntityFrameworkCore;

namespace Elyze.Controllers
{

    public class AreaController : Controller
    {
        private readonly ElyzeContext _context;
        public IConfiguration Configuration { get; }
        private readonly IHtmlLocalizer<SharedResource> _sharedLocalizer;
        private readonly string _basePath;
        private string _isForConfindustria;
        private string _estensione;

        public AreaController(IConfiguration oConfiguration, ElyzeContext context, IHtmlLocalizer<SharedResource> sharedLocalizer, IConfiguration configuration)
        {
            _context = context;
            Configuration = oConfiguration;
            _sharedLocalizer = sharedLocalizer;
            _basePath = configuration.GetSection("Host:BaseHostPath").Value;

            _isForConfindustria = oConfiguration.GetValue<bool>("CustomStyle:Confindustria") ? "Confindustria" : "Normale";
            _estensione = _isForConfindustria == "Normale" ? "png" : "svg";
        }

        //endpoint for the server side datatable
        [HttpPost]
        public async Task<IActionResult> GetAreaDataTableAsync()
        {
            var form = HelpersDataTables.ContextToDatatable(Request);

            DataTableResult result = new()
            {
                Draw = form.Draw,
                RecordsTotal = await _context.Area.CountAsync()
            };

            var areaLinguaFilter = int.Parse(Request.Form["AreaLinguaFilter"].FirstOrDefault() ?? "0");

            var query = from area in _context.Area
                        join lingua in _context.Lingue on area.IdLingua equals lingua.Id
                        //group area by area.IdArea
                        //into g
                        //select new { g.Key, Data = g.ToList() };
                        where ((form.SearchValue.Equals("") ||
                            (area.Descrizione != null && area.Descrizione.ToLower().Contains(form.SearchValue)) ||
                            (area.DataCreazione.ToString().ToLower().Contains(form.SearchValue)) ||
                            (area.DataSpegnimento.ToString().ToLower().Contains(form.SearchValue)) ||
                            (lingua.Id.ToString().ToLower().Contains(form.SearchValue)) ||
                            (lingua.Sigla != null && lingua.Sigla.ToLower().Contains(form.SearchValue)) ||
                            (area.Stato != null && (area.Stato == true ? "true" : "false").ToLower().Contains(form.SearchValue))))
                        select new AreaViewDataTableModel
                        {
                            Id = area.Id,
                            IdArea = area.IdArea,
                            Descrizione = area.Descrizione ?? "",
                            DataCreazione = area.DataCreazione.ToShortDateString(),
                            DataSpegnimento = area.DataSpegnimento.ToShortDateString(),
                            Stato = area.Stato,
                            Lingua = lingua.Sigla,
                            IdLingua = lingua.Id
                        };

            if (_sharedLocalizer["Area_Description"].Value == form.SortColumn)
            {
                query = form.SortColumnDirection == "asc" ? query.OrderBy(x => x.Descrizione) : query.OrderByDescending(x => x.Descrizione);
            }
            else
            if (_sharedLocalizer["Astatus"].Value == form.SortColumn)
            {
                query = form.SortColumnDirection == "asc" ? query.OrderBy(x => x.Stato) : query.OrderByDescending(x => x.Stato);
            }
            else
            if (_sharedLocalizer["Area_CreationDate"].Value == form.SortColumn)
            {
                query = form.SortColumnDirection == "asc" ? query.OrderBy(x => x.DataCreazione) : query.OrderByDescending(x => x.DataCreazione);
            }
            else if (_sharedLocalizer["Area_ValidityDate"].Value == form.SortColumn)
            {
                query = form.SortColumnDirection == "asc" ? query.OrderBy(x => x.DataSpegnimento) : query.OrderByDescending(x => x.DataSpegnimento);
            }
            else if (_sharedLocalizer["Language"].Value == form.SortColumn)
            {
                query = form.SortColumnDirection == "asc" ? query.OrderBy(x => x.Lingua) : query.OrderByDescending(x => x.Lingua);
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
                    x.Lingua
                };
                dataset2.ForEach(y =>
                {
                    if (x.IdArea == y.IdArea)
                    {
                        languages.Add(y.Lingua);
                    }
                });
                x.Lingua = string.Join(" - ", languages.Distinct().ToList());
            });

            if (areaLinguaFilter != 0)
            {
                dataset1 = dataset1.Where(x => x.IdLingua == areaLinguaFilter).ToList();
            }

            result.RecordsFiltered = dataset1.Count();
            result.Data = dataset1.Skip(form.Start).Take(form.Length).Cast<object>().ToList();

            return Json(result);
        }

        // GET: Area
        //[Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Index()
        {
            if (Request.Query["Redirect"].FirstOrDefault() != null)
            {
                ViewData["Redirect"] = Request.Query["Redirect"].First();
            }

            ViewData["Header"] = HeaderClassBuilders.Area.Index(_sharedLocalizer, _basePath);
            ViewBag.Lingue = _context.Lingue.Select(x => new { Id = x.Id, Descrizione = x.Descrizione }).ToList();

            var oRqf = Request.HttpContext.Features.Get<IRequestCultureFeature>();
            var oCulture = oRqf.RequestCulture.Culture.Name;

            ViewData["Lingua"] = _context.Lingue.Where(x => x.SiglaEstesa == oCulture).Select(x => x.Id).FirstOrDefault();

            return View();
        }

        // GET: Area/Details/5
        //[Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Details(int? id)
        {
            ViewData["Header"] = HeaderClassBuilders.Area.Details(_sharedLocalizer, _basePath);

            var idArea = await _context.Area.Where(y => y.Id == id).Select(x => x.IdArea).FirstOrDefaultAsync();
            var iconeSdg = await _context.AreaSDG.Where(z => z.IdArea == id).Select(y => y.IdSdg).ToArrayAsync();

            ViewBag.SDG = _context.SDG.ToList();
            ViewBag.Lingue = _context.Lingue.ToList();
            ViewBag.IdArea = idArea;

            var areaToEdit = await _context.Area.Select(x => new AreaFormViewModel()
            {
                Id = x.Id,
                DataCreazione = x.DataCreazione,
                DataSpegnimento = x.DataSpegnimento,
                Descrizione = x.Descrizione ?? "",
                IdIconaArea = x.IdIconaArea,
                IdLingua = x.IdLingua,
                Stato = x.Stato,
                IdsIconeSdg = iconeSdg,
                IdArea = idArea
            }).FirstOrDefaultAsync(x => x.Id == id);

            if (areaToEdit == null)
            {
                return NotFound();
            }

            List<AreaIcons> areaIcons = _context.AreaIcons
                .Where(x => x.Id == areaToEdit.IdIconaArea)
                .Select(x => new AreaIcons()
                {
                    Id = x.Id,
                    Descrizione = x.Descrizione,
                    Codice = x.Codice,
                    LocalPath = string.Format(x.LocalPath, _isForConfindustria, _estensione)
                })
                .ToList();

            ViewBag.Icone = areaIcons;

            return View(areaToEdit);
        }

        // GET: Area/DownloadCSV
        //[Authorize(Roles = "Administrator")]
        public async Task<IActionResult> DownloadCSV()
        {
            var iMultitenant = Configuration.GetSection("Tenant:MultiTenant").Get<int>();
            var iImport = Configuration.GetSection("Features:Import").Get<int>();

            if (iImport == 0 && iMultitenant == 0)
            {
                return StatusCode(403);
            }

            return View(await _context.Area.ToListAsync());
        }

        // POST: Area/DownloadCSV
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //[Authorize(Roles = "Administrator")]
        public IActionResult DownloadCSV([Bind("IdArea")] int idArea)
        {
            var iMultitenant = Configuration.GetSection("Tenant:MultiTenant").Get<int>();

            var iImport = Configuration.GetSection("Features:Import").Get<int>();

            if (iImport == 0 && iMultitenant == 0)
            {
                return StatusCode(403);
            }


            var oRqf = Request.HttpContext.Features.Get<IRequestCultureFeature>();
            var oCulture = oRqf.RequestCulture.Culture;
            var sLanguage = "IT";

            if (oCulture.Name.ToLower().Contains("en"))
            {
                sLanguage = "EN";
            }

            //var oArea = _context.Area.Where(x => x.IdArea == idArea && x.Lingua == sLanguage);
            var aoMicroAree = _context.MicroArea.ToList();
            var aiMAId = new List<int>();

            //foreach (var oMA in aoMicroAree)
            //{
            //    aiMAId.Add(oMA.MicroAreaId);
            //}


            //var aoMAFields = _context.CampiMa.Where(m => aiMAId.Contains(m.MicroAreaId) && m.Lingua == sLanguage);
            //var sCsv = (sLanguage == "IT") ? "DataInizio;DataFine;Reparto;Società;Stabilimento;" : "StartDate;EndDate;Departement;Company;Plant;";

            //foreach (CampiMa oCMA in aoMAFields)
            //{
            //    sCsv += oCMA.Descrizione + " " + idArea + "," + oCMA.MicroAreaId + "," + oCMA.IdCampo + ";";
            //}

            //Response.Headers.Add("Content-Disposition", "attachment; filename=\"Elyze-importazione-area-" + oArea.First().Descrizione + "-" + string.Format("{0:M/d/yyyy}", DateTime.Now) + ".csv\"");
            return Content(null, "text/csv");

        }

        // GET: Area/DownloadFIle

        public async Task<IActionResult> DownloadFile(int id)
        {
            int iMultitenant = Configuration.GetSection("Tenant:MultiTenant").Get<int>();
            int iRepo = Configuration.GetSection("Features:Repository").Get<int>();

            if (iRepo == 0 && iMultitenant == 0)
            {
                return StatusCode(403);
            }

            var oRepository = await _context.Repository.FindAsync(id);

            if (oRepository == null)
            {
                return NotFound();
            }

            string sPath = this.Configuration.GetSection("File:Repo").Get<string>();
            string sUserId = HttpContext.Session.GetString("UserId");
            sPath = Path.Combine(sPath, sUserId);
            string sFilePatha = "";

            if (User.IsInRole("Administrator"))
            {
                string[] dirs = Directory.GetDirectories(@"C:\inetpub\wwwroot\Elyze\wwwroot\FIlesRepo");
                foreach (string dir in dirs)
                {
                    sFilePatha = Path.Combine(dir, oRepository.FilePath);
                    if (System.IO.File.Exists(sFilePatha))
                    {
                        byte[] asFileBytes = System.IO.File.ReadAllBytes(sFilePatha);
                        string sFileName = oRepository.FilePath;
                        return File(asFileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, sFileName);
                    }
                }

            }

            bool bExists = System.IO.Directory.Exists(sPath);

            string sFilePath = Path.Combine(sPath, oRepository.FilePath);

            if (System.IO.File.Exists(sFilePath))
            {
                byte[] asFileBytes = System.IO.File.ReadAllBytes(sFilePath);
                string sFileName = oRepository.FilePath;
                return File(asFileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, sFileName);
            }

            return View(oRepository);
        }

        // GET: Area/DeleteFIle
        public async Task<IActionResult> DeleteFile(int id)
        {
            int iMultitenant = Configuration.GetSection("Tenant:MultiTenant").Get<int>();
            int iRepo = Configuration.GetSection("Features:Repository").Get<int>();

            if (iRepo == 0 && iMultitenant == 0)
            {
                return StatusCode(403);
            }

            var oRepository = await _context.Repository.FindAsync(id);

            if (oRepository == null)
            {
                return NotFound();
            }

            string sPath = this.Configuration.GetSection("File:Repo").Get<string>();
            string sUserId = HttpContext.Session.GetString("UserId");
            sPath = Path.Combine(sPath, sUserId);

            string sFileFormPath = Path.Combine(sPath, oRepository.FilePath);

            _context.Repository.Remove(oRepository);
            await _context.SaveChangesAsync();

            System.IO.File.Delete(sFileFormPath);

            return RedirectToAction(nameof(Data.Repository));
        }

        // GET: Area/Repository
        //public async Task<IActionResult> Repository(int? id)
        //{
        //    int iMultitenant = Configuration.GetSection("Tenant:MultiTenant").Get<int>();
        //    int iRepo = Configuration.GetSection("Features:Repository").Get<int>();

        //    var oRqf = Request.HttpContext.Features.Get<IRequestCultureFeature>();
        //    var oCulture = oRqf.RequestCulture.Culture;
        //    string sLanguage = "IT";

        //    if (oCulture.Name == "en-US")
        //    {
        //        sLanguage = "EN";
        //    }


        //    if (iRepo == 0 && iMultitenant == 0)
        //    {
        //        return StatusCode(403);
        //    }

        //    List<Repository> aoRepo = null;
        //    List<Area> aoArea = new List<Area>();

        //    if (id == null)
        //    {
        //        aoRepo = await _context.Repository.ToListAsync();
        //    }
        //    else
        //    {
        //        aoRepo = await _context.Repository.Where(a => a.AreaId == id).ToListAsync();
        //    }

        //    foreach (Repository oCurrent in aoRepo)
        //    {
        //        //var oArea = _context.Area.Where(x => x.IdArea == oCurrent.AreaId && x.Lingua == sLanguage).First();

        //        //if (oArea != null)
        //        //{
        //        //    aoArea.Add(oArea);
        //        //}
        //        //else
        //        //{
        //        //}
        //        Area oNewArea = new Area();
        //        oNewArea.Descrizione = "";
        //        aoArea.Add(oNewArea);

        //    }

        //    ViewBag.Aree = aoArea;

        //    return View(aoRepo);
        //}

        // GET: Area/UploadFile
        //public async Task<IActionResult> UploadFile()
        //{
        //    int iMultitenant = Configuration.GetSection("Tenant:MultiTenant").Get<int>();
        //    int iRepo = Configuration.GetSection("Features:Repository").Get<int>();

        //    var oRqf = Request.HttpContext.Features.Get<IRequestCultureFeature>();
        //    var oCulture = oRqf.RequestCulture.Culture;
        //    string sLanguage = "IT";

        //    if (oCulture.Name == "en-US")
        //    {
        //        sLanguage = "EN";
        //    }
        //    User usr = new User();
        //    IEnumerable<Claim> claims = HttpContext.User.Claims;
        //    var id = Convert.ToInt32(User.FindFirst("id").Value);

        //    if (iRepo == 0 && iMultitenant == 0)
        //    {
        //        return StatusCode(403);
        //    }
        //    var ar = _context.UserArea.Where(i => i.UserId == id).Select(m => m.IdArea).ToList();
        //    //if (User.IsInRole("Administrator"))
        //    //{
        //    //    ViewBag.Aree = _context.Area.Where(x => x.Lingua == sLanguage);
        //    //}
        //    //else
        //    //{
        //    //    ViewBag.Aree = _context.Area.Where(x => x.Lingua == sLanguage && ar.Contains(x.IdArea));
        //    //}

        //    return View();
        //}

        // POST: Area/Import
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UploadFile([Bind("Titolo,Descrizione,AreaId")] Data.Repository oRepository, [FromForm(Name = "files")] IFormFile oFile)
        {
            int iMultitenant = Configuration.GetSection("Tenant:MultiTenant").Get<int>();
            int iRepo = Configuration.GetSection("Features:Repository").Get<int>();

            if (iRepo == 0 && iMultitenant == 0)
            {
                return StatusCode(403);
            }

            string sPath = this.Configuration.GetSection("File:Repo").Get<string>();
            string sUserId = HttpContext.Session.GetString("UserId");
            sPath = Path.Combine(sPath, sUserId);

            bool bExists = System.IO.Directory.Exists(sPath);

            if (!bExists)
            {
                System.IO.Directory.CreateDirectory(sPath);
            }

            List<string> asFiles = new List<string>();
            var sFileFormPath = "";

            string sFilename = oFile.FileName;
            sFileFormPath = Path.Combine(sPath, sFilename);

            oRepository.FilePath = sFilename;

            using (var oStream = System.IO.File.Create(sFileFormPath))
            {
                await oFile.CopyToAsync(oStream);

                if (ModelState.IsValid)
                {
                    _context.Add(oRepository);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Data.Repository), new { id = oRepository.AreaId });
                }
            }

            return View(oRepository);
        }

        // GET: Area/Import
        public async Task<IActionResult> Import()
        {
            int iMultitenant = Configuration.GetSection("Tenant:MultiTenant").Get<int>();
            int iImport = Configuration.GetSection("Features:Import").Get<int>();

            if (iImport == 0 && iMultitenant == 0)
            {
                return StatusCode(403);
            }

            ViewBag.Message = "";

            return View();
        }

        // POST: Area/Import
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Import([FromForm(Name = "files")] List<IFormFile> aoFiles)
        //{
        //    int iMultitenant = Configuration.GetSection("Tenant:MultiTenant").Get<int>();
        //    int iImport = Configuration.GetSection("Features:Import").Get<int>();

        //    if (iImport == 0 && iMultitenant == 0)
        //    {
        //        return StatusCode(403);
        //    }

        //    string sPath = this.Configuration.GetSection("File:Path").Get<string>();
        //    List<string> asFiles = new List<string>();
        //    var sFileFormPath = "";

        //    foreach (var oFormFile in aoFiles)
        //    {
        //        if (oFormFile.Length > 0)
        //        {
        //            sFileFormPath = Path.Combine(sPath, Path.GetRandomFileName());

        //            using (var oStream = System.IO.File.Create(sFileFormPath))
        //            {
        //                await oFormFile.CopyToAsync(oStream);
        //                asFiles.Add(sFileFormPath);
        //            }
        //        }
        //    }

        //    foreach (var sFilePath in asFiles)
        //    {
        //        string sCsvData = System.IO.File.ReadAllText(sFilePath);
        //        bool bFirstRow = true;
        //        List<int> aiFieldId = new List<int>();
        //        List<int> aiMicroAreaId = new List<int>();
        //        int iMAId = -1;

        //        var oIns = _context.Inserimenti.OrderByDescending(x => x.IdInserimento);

        //        int iInserimentoGruop = (oIns.Count() > 0) ? Convert.ToInt32(oIns.First().IdInserimento) : 0;
        //        int iFieldIndex = 0;

        //        var oInserimento = new Inserimenti();

        //        foreach (string sRow in sCsvData.Split('\n'))
        //        {
        //            if (!string.IsNullOrEmpty(sRow))
        //            {
        //                if (bFirstRow)
        //                {
        //                    bFirstRow = false;



        //                    foreach (string sIns in sRow.Split(';'))
        //                    {
        //                        if (iFieldIndex >= 5)
        //                        {
        //                            string[] asHeader = sIns.Trim().Split(' ');



        //                            if (asHeader.Count() > 1)
        //                            {
        //                                string[] asInfos = asHeader[asHeader.Count() - 1].Trim().Split(',');



        //                                if (asInfos.Count() > 2)
        //                                {
        //                                    aiMicroAreaId.Add(Convert.ToInt32(asInfos[1]));
        //                                    aiFieldId.Add(Convert.ToInt32(asInfos[2]));
        //                                }
        //                                else
        //                                {
        //                                    aiMicroAreaId.Add(-1);
        //                                    aiFieldId.Add(-1);
        //                                }
        //                            }
        //                            else
        //                            {
        //                                aiMicroAreaId.Add(-1);
        //                                aiFieldId.Add(-1);
        //                            }
        //                            iFieldIndex++;
        //                        }
        //                        else
        //                        {
        //                            iFieldIndex++;
        //                        }
        //                    }

        //                }
        //                else
        //                {
        //                    var iField = 0;



        //                    var asRow = sRow.Split(';');
        //                    var bNewIns = true;

        //                    foreach (var sIns in asRow)
        //                    {
        //                        if (string.IsNullOrEmpty(sIns) || iField < 5)
        //                        {
        //                            iField++;
        //                            continue;
        //                        }

        //                        if (aiFieldId[iField - 5] < 0)
        //                        {
        //                            iField++;
        //                            continue;
        //                        }

        //                        if (string.IsNullOrEmpty(sIns))
        //                        {
        //                            iField++;
        //                            continue;
        //                        }

        //                        oInserimento = new Inserimenti();
        //                        oInserimento.UtenteId = Convert.ToInt32(HttpContext.Session.GetString("UserId"));
        //                        oInserimento.IdCampo = aiFieldId[iField - 5];
        //                        oInserimento.ValoreCampo = sIns;
        //                        oInserimento.DataInizio = DateTime.Parse(asRow[0]);
        //                        oInserimento.DataFine = DateTime.Parse(asRow[1]);
        //                        oInserimento.DataInserimento = DateTime.Now;
        //                        oInserimento.Stato = 1;

        //                        string sReparto = asRow[2].Trim().Replace("\r\n", string.Empty);
        //                        Reparto oStabilimento = _context.Reparto.Where(r => r.Nome == sReparto).FirstOrDefault();

        //                        if (oStabilimento == null)
        //                        {
        //                            iField++;
        //                            continue;
        //                        }
        //                        else
        //                        {
        //                            oInserimento.RepartoId = oStabilimento.Id;
        //                        }

        //                        string sSocieta = asRow[3].Trim().Replace("\r\n", string.Empty);
        //                        Societa oSocieta = _context.Societa.Where(s => s.Descrizione == sSocieta).FirstOrDefault();

        //                        if (oSocieta == null)
        //                        {
        //                            iField++;
        //                            continue;
        //                        }
        //                        else
        //                        {
        //                            oInserimento.SocietaId = oSocieta.Id;
        //                        }

        //                        string sSede = asRow[4].Trim().Replace("\r\n", string.Empty);
        //                        Sede oSede = _context.Sede.Where(s => s.Descrizione == sSede).FirstOrDefault();

        //                        if (oSede == null)
        //                        {
        //                            iField++;
        //                            continue;
        //                        }
        //                        else
        //                        {
        //                            oInserimento.StabilimentoId = oSede.Id;
        //                        }

        //                        int iCurrentMA = aiMicroAreaId[iField - 5];

        //                        if (iCurrentMA != iMAId || bNewIns)
        //                        {
        //                            iMAId = iCurrentMA;
        //                            iInserimentoGruop++;
        //                            bNewIns = false;
        //                        }

        //                        oInserimento.MicroAreaId = aiMicroAreaId[iField - 5];
        //                        oInserimento.IdInserimento = iInserimentoGruop;

        //                        iField++;

        //                        _context.Add(oInserimento);
        //                        await _context.SaveChangesAsync();
        //                    }
        //                }
        //            }
        //        }

        //        if (System.IO.File.Exists(sFileFormPath))
        //        {
        //            System.IO.File.Delete(sFileFormPath);
        //        }
        //    }

        //    ViewBag.Message = "I dati sono stati importati correttamente.";

        //    return View();
        //}

        // GET: Area/Create
        //[Authorize(Roles = "Administrator")]
        public IActionResult Create()
        {
            try
            {
                ViewData["Header"] = HeaderClassBuilders.Area.Create(_sharedLocalizer, _basePath);
                var lastIdArea = 1;
                if (_context.Area.Any())
                {
                    lastIdArea = _context.Area.Select(a => a.IdArea).Max() + 1;
                }

                ViewBag.IdArea = lastIdArea;

                List<AreaIcons> areaIcons = _context.AreaIcons
                    .Select(x => new AreaIcons()
                    {
                        Id = x.Id,
                        Descrizione = x.Descrizione,
                        Codice = x.Codice,
                        LocalPath = string.Format(x.LocalPath, _isForConfindustria, _estensione)
                    })
                    .ToList();

                ViewBag.Icone = areaIcons;
                ViewBag.SDG = _context.SDG.ToList();
                ViewBag.Lingue = _context.Lingue.ToList();

                return View(new AreaFormViewModel());
            }
            catch (Exception ex)
            {
                return View("Error", ex.Message);
            }


        }

        //POST: Area/Create
        //To protect from overposting attacks, enable the specific properties you want to bind to.
        //For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.


        [HttpPost]
        [ValidateAntiForgeryToken]
        //[Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Create(AreaFormViewModel area)
        {
            try
            {
                using (var dbContextTransaction = _context.Database.BeginTransaction())
                {
                    var lastIdArea = 1;
                    if (_context.Area.Any())
                    {
                        lastIdArea = _context.Area.Select(a => a.IdArea).Max() + 1;
                    }

                    var areaToAdd = new Data.Area
                    {
                        Descrizione = area.Descrizione,
                        Stato = area.Stato,
                        DataCreazione = area.DataCreazione,
                        DataSpegnimento = area.DataSpegnimento,
                        IdLingua = area.IdLingua,
                        IdIconaArea = area.IdIconaArea,
                        IdArea = lastIdArea,
                    };
                    _context.Add(areaToAdd);
                    await _context.SaveChangesAsync();

                    if (area.IdsIconeSdg != null)
                    {
                        foreach (var idIcona in area.IdsIconeSdg)
                        {
                            var sdgToAreaToAdd = new AreaSDG
                            {
                                IdArea = areaToAdd.Id,
                                IdSdg = idIcona
                            };
                            _context.Add(sdgToAreaToAdd);
                        }
                    }
                    await _context.SaveChangesAsync();

                    dbContextTransaction.Commit();
                }

                return RedirectToAction("Index", "Area");
            }
            catch (Exception)
            {
                return View();
                throw;
            }

        }

        // GET: Area/Edit/5
        //[Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Edit(int id)
        {
            ViewData["Header"] = HeaderClassBuilders.Area.Edit(_sharedLocalizer, _basePath);
            var idArea = await _context.Area.Where(y => y.Id == id).Select(x => x.IdArea).FirstOrDefaultAsync();
            var iconeSdg = await _context.AreaSDG.Where(z => z.IdArea == id).Select(y => y.IdSdg).ToArrayAsync();


            List<AreaIcons> areaIcons = _context.AreaIcons
                .Select(x => new AreaIcons()
                {
                    Id = x.Id,
                    Descrizione = x.Descrizione,
                    Codice = x.Codice,
                    LocalPath = string.Format(x.LocalPath, _isForConfindustria, _estensione)
                })
                .ToList();

            ViewBag.Icone = areaIcons;
            ViewBag.SDG = _context.SDG.ToList();
            ViewBag.Lingue = _context.Lingue.ToList();
            ViewBag.IdArea = idArea;

            var areaToEdit = await _context.Area.Select(x => new AreaFormViewModel()
            {
                Id = x.Id,
                DataCreazione = x.DataCreazione,
                DataSpegnimento = x.DataSpegnimento,
                Descrizione = x.Descrizione ?? "",
                IdIconaArea = x.IdIconaArea,
                IdLingua = x.IdLingua,
                Stato = x.Stato,
                IdsIconeSdg = iconeSdg,
                IdArea = idArea
            }).FirstOrDefaultAsync(x => x.Id == id);

            return View(areaToEdit);
        }


        // POST: Area/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //[Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Edit(int id, AreaFormViewModel area)
        {
            if (id != area.Id)
            {
                return NotFound();
            }

            var areaToUpdate = await _context.Area.FirstOrDefaultAsync(x => x.Id == id);
            if (areaToUpdate == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                using (var dbContextTransaction = _context.Database.BeginTransaction())
                {
                    areaToUpdate.DataCreazione = area.DataCreazione;
                    areaToUpdate.DataSpegnimento = area.DataSpegnimento;
                    areaToUpdate.Stato = area.Stato;
                    areaToUpdate.Descrizione = area.Descrizione;
                    areaToUpdate.IdIconaArea = area.IdIconaArea;
                    areaToUpdate.IdLingua = area.IdLingua;

                    _context.Update(areaToUpdate);

                    var areaSDGToDelete = _context.AreaSDG.Where(x => x.IdArea == id).ToList();
                    _context.AreaSDG.RemoveRange(areaSDGToDelete);

                    if (area.IdsIconeSdg != null)
                    {
                        foreach (var idIcona in area.IdsIconeSdg)
                        {
                            var sdgToAreaToAdd = new AreaSDG
                            {
                                IdArea = id,
                                IdSdg = idIcona
                            };
                            _context.Add(sdgToAreaToAdd);
                        }
                    }
                    await _context.SaveChangesAsync();

                    dbContextTransaction.Commit();
                }

                return RedirectToAction(nameof(Index));
            }
            return View(area);
        }

        // GET: Area/Edit/5
        //[Authorize(Roles = "Administrator")]
        [HttpGet]
        public async Task<IActionResult> AggiungiLingua(int id)
        {
            ViewData["Header"] = HeaderClassBuilders.Area.AggiungiLingua(_sharedLocalizer, _basePath);
            var idArea = await _context.Area.Where(y => y.Id == id).Select(x => x.IdArea).FirstOrDefaultAsync();
            var iconeSdg = await _context.AreaSDG.Where(z => z.IdArea == id).Select(y => y.IdSdg).ToArrayAsync();

            ViewBag.SDG = _context.SDG.ToList();
            ViewBag.IdArea = idArea;

            var areaToAddLanguage = await _context.Area.Select(x => new AreaFormViewModel()
            {
                Id = x.Id,
                DataCreazione = x.DataCreazione,
                DataSpegnimento = x.DataSpegnimento,
                Descrizione = x.Descrizione ?? "",
                IdIconaArea = x.IdIconaArea,
                IdLingua = x.IdLingua,
                Stato = x.Stato,
                IdsIconeSdg = iconeSdg,
                IdArea = idArea
            }).FirstOrDefaultAsync(x => x.Id == id);

            if (areaToAddLanguage == null)
            {
                return NotFound();
            }

            List<AreaIcons> areaIcons = _context.AreaIcons
                .Where(x => x.Id == areaToAddLanguage.IdIconaArea)
                .Select(x => new AreaIcons()
                {
                    Id = x.Id,
                    Descrizione = x.Descrizione,
                    Codice = x.Codice,
                    LocalPath = string.Format(x.LocalPath, _isForConfindustria, _estensione)
                })
                .ToList();

            ViewBag.Icone = areaIcons;
            ViewBag.Lingue = _context.Lingue.Where(x => x.Id != areaToAddLanguage.IdLingua).ToList();

            return View(areaToAddLanguage);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        //[Authorize(Roles = "Administrator")]
        public async Task<IActionResult> AggiungiLingua(int idArea, AreaFormViewModel area)
        {
            if (ModelState.IsValid)
            {
                using (var dbContextTransaction = _context.Database.BeginTransaction())
                {

                    var areaToAdd = new Data.Area
                    {
                        Descrizione = area.Descrizione,
                        Stato = area.Stato,
                        DataCreazione = area.DataCreazione,
                        DataSpegnimento = area.DataSpegnimento,
                        IdLingua = area.IdLingua,
                        IdIconaArea = area.IdIconaArea,
                        IdArea = idArea,
                    };
                    _context.Add(areaToAdd);
                    await _context.SaveChangesAsync();

                    //roba brutta.... ma necessaria...

                    if (await _context.Area.CountAsync() > 1)
                    {
                        var idFirstAreaWithIdArea = await _context.Area.Where(x => x.IdArea == idArea && x.Id != areaToAdd.Id).Select(x => x.Id).FirstOrDefaultAsync();

                        var userAreaPermissions = await _context.UserArea.Where(x => x.IdArea == idFirstAreaWithIdArea).ToListAsync();
                        foreach (var permissions in userAreaPermissions)
                        {
                            var anotherPermission = new Data.UserArea();
                            anotherPermission.IdArea = areaToAdd.Id;
                            anotherPermission.IdUser = permissions.IdUser;
                            anotherPermission.IdUserType = permissions.IdUserType;
                            _context.Add(anotherPermission);
                        }

                    }

                    //fine roba brutta

                    if (area.IdsIconeSdg != null)
                    {
                        foreach (var idIcona in area.IdsIconeSdg)
                        {
                            var sdgToAreaToAdd = new AreaSDG
                            {
                                IdArea = areaToAdd.Id,
                                IdSdg = idIcona
                            };
                            _context.Add(sdgToAreaToAdd);
                        }
                    }
                    await _context.SaveChangesAsync();

                    dbContextTransaction.Commit();

                    return RedirectToAction(nameof(Index));
                }
            }

            return View(area);
        }



        // GET: Area/Delete/5
        //[Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Delete(int? id)
        {
            ViewData["Header"] = HeaderClassBuilders.Area.Delete(_sharedLocalizer, _basePath);

            var idArea = await _context.Area.Where(y => y.Id == id).Select(x => x.IdArea).FirstOrDefaultAsync();
            var iconeSdg = await _context.AreaSDG.Where(z => z.IdArea == id).Select(y => y.IdSdg).ToArrayAsync();

            ViewBag.SDG = _context.SDG.ToList();
            ViewBag.Lingue = _context.Lingue.ToList();
            ViewBag.IdArea = idArea;

            var areaToDelete = await _context.Area.Select(x => new AreaFormViewModel()
            {
                Id = x.Id,
                DataCreazione = x.DataCreazione,
                DataSpegnimento = x.DataSpegnimento,
                Descrizione = x.Descrizione ?? "",
                IdIconaArea = x.IdIconaArea,
                IdLingua = x.IdLingua,
                Stato = x.Stato,
                IdsIconeSdg = iconeSdg,
                IdArea = idArea
            }).FirstOrDefaultAsync(x => x.Id == id);

            if (areaToDelete == null)
            {
                return NotFound();
            }

            List<AreaIcons> areaIcons = _context.AreaIcons
                .Where(x => x.Id == areaToDelete.IdIconaArea)
                .Select(x => new AreaIcons()
                {
                    Id = x.Id,
                    Descrizione = x.Descrizione,
                    Codice = x.Codice,
                    LocalPath = string.Format(x.LocalPath, _isForConfindustria, _estensione)
                })
                .ToList();

            ViewBag.Icone = areaIcons;

            return View(areaToDelete);
        }

        // POST: Area/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        //[Authorize(Roles = "Administrator")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var area = await _context.Area.FindAsync(id);
            var ma = _context.MicroArea.ToList();
            IQueryable<CampiMa> fieldMa = null;

            _context.Area.Remove(area);
            //rimozione microaree collegate
            //if (ma.Any())
            //{
            //    foreach (var k in ma)
            //    {
            //        _context.MicroArea.Remove(k);
            //        fieldMa = _context.CampiMa.Where(m => m.MicroAreaId == k.MicroAreaId);
            //        if (fieldMa.Any())
            //        {
            //            foreach (var f in fieldMa)
            //            {
            //                _context.CampiMa.Remove(f);

            //            }
            //        }
            //    }
            //}
            //rimozione dei campi delle microaree 


            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AreaExists(int id)
        {
            return _context.Area.Any(e => e.Id == id);
        }
    }
}
