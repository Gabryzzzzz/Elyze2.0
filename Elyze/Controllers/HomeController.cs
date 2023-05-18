// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Elyze.Data;
using Elyze.Helpers;
using Elyze.Helpers.Headers;
using Elyze.Models;
using Elyze.Models.Home;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Localization;
using Microsoft.EntityFrameworkCore;

namespace Elyze.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IHtmlLocalizer<SharedResource> _sharedLocalizer;
        private readonly ElyzeContext _context;
        private readonly IClaimGetterService _claimGetterService;
        private readonly UserManager<AspNetUsers> _userManager;
        private readonly string _basePath;
        private readonly IConfiguration _configuration;
        private string _isForConfindustria;
        private string _estensione;
        private readonly IHttpContextAccessor _httpContext;

        public HomeController(IHtmlLocalizer<SharedResource> sharedLocalizer, IConfiguration configuration, ElyzeContext context, IClaimGetterService claimGetterService, UserManager<AspNetUsers> _userManager, IHttpContextAccessor httpContextAccessor)
        {
            _sharedLocalizer = sharedLocalizer;
            _context = context;
            _claimGetterService = claimGetterService;
            this._userManager = _userManager;
            _basePath = configuration.GetSection("Host:BaseHostPath").Value;
            _configuration = configuration;

            _isForConfindustria = _configuration.GetValue<bool>("CustomStyle:Confindustria") ? "Confindustria" : "Normale";
            _estensione = _isForConfindustria == "Normale" ? "png" : "svg";
            _httpContext = httpContextAccessor;
        }

        public IActionResult ChangeLanguage(string id)
        {
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(id)),
                    new CookieOptions
                    {
                        Expires = DateTimeOffset.UtcNow.AddDays(7)
                    }
            );

            return LocalRedirect("/");
        }

        public async Task<IActionResult> Index()
        {
            if (Request.Query["Redirect"].FirstOrDefault() != null)
            {
                ViewData["Redirect"] = Request.Query["Redirect"].First();
            }


            ViewData["Header"] = HeaderClassBuilders.Home.Index(_sharedLocalizer);
            IRequestCultureFeature? requestFeatures = Request.HttpContext.Features.Get<IRequestCultureFeature>();
            string? culture = requestFeatures?.RequestCulture.Culture.Name;
            Features features = _configuration.GetSection("Features").Get<Features>();

            var email = _claimGetterService.GetUserClaim();
            var aree = new List<HomeAreaListViewModel>();
            //if user is admin show all area, else show only area of user
            if (await _claimGetterService.IsAdmin())
            {
                aree = await _context.Area
                    .Include(y => y.IdIconaAreaNavigation)
                    .Include(z => z.IdLinguaNavigation)
                    .Where(x => x.IdLinguaNavigation.SiglaEstesa == culture)
                    .Select(y =>
                                           new HomeAreaListViewModel
                                           {
                                               Nome = y.Descrizione ?? "",
                                               LocalPath = string.Format(y.IdIconaAreaNavigation.LocalPath, _isForConfindustria, _estensione),
                                               IdArea = y.Id
                                           }).ToListAsync();
            }
            else
            {
                var userArea = await _context.UserArea
                .Include(y => y.IdUserNavigation)
                .Where(x => x.IdUserNavigation.Email == email)
                .Select(y => y.IdArea)
                .ToListAsync();


                aree = await _context.Area
                    .Include(y => y.IdIconaAreaNavigation)
                    .Include(z => z.IdLinguaNavigation)
                    .Where(x => x.IdLinguaNavigation.SiglaEstesa == culture && userArea.Contains(x.Id))
                    .Select(y =>
                        new HomeAreaListViewModel
                        {
                            Nome = y.Descrizione ?? "",
                            LocalPath = string.Format(y.IdIconaAreaNavigation.LocalPath, _isForConfindustria, _estensione),
                            IdArea = y.Id
                        }
                    ).ToListAsync();
            }

            HomeViewModel homeViewModel = new HomeViewModel();
            homeViewModel.Features = features;
            homeViewModel.HomeAreaListViewModels = aree;


            return View(homeViewModel);
        }

        public IActionResult Gerarchie()
        {
            ViewData["Header"] = HeaderClassBuilders.Home.Gerarchie(_sharedLocalizer, _basePath);

            return View();
        }

        public IActionResult Impostazioni()
        {
            ViewData["Header"] = HeaderClassBuilders.Home.Impostazioni(_sharedLocalizer, _basePath);
            ViewBag.Features = _configuration.GetSection("Features").Get<Features>(); ;

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Error()
        {
            ViewData["BasePath"] = _basePath;
            return View();
        }

        public async Task<IActionResult> PowerBi()
        {
            string powerBILink = "";
            bool isExternal = _configuration.GetSection("BI:IsExternal").Get<bool>();
            if (isExternal)
            {
                powerBILink = _httpContext?.HttpContext?.Request.Cookies["BILink"];
            }
            else
            {
                powerBILink = _configuration.GetSection("BI:BILink").Get<string>();
            }

            return View("PowerBi", powerBILink);
        }
    }
}
