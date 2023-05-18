// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Elyze.Data;
using Elyze.Helpers.Headers;
using Elyze.Models.Home;
using Elyze.Models.UserPermissions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Localization;
using Microsoft.EntityFrameworkCore;

namespace Elyze.Controllers
{
    public class UserAreaController : Controller
    {
        private readonly ElyzeContext _context;
        private readonly UserManager<AspNetUsers> _userManager;
        private readonly IHtmlLocalizer<SharedResource> _sharedLocalizer;
        private readonly string _basePath;
        public UserAreaController(ElyzeContext context, UserManager<AspNetUsers> userManager, IHtmlLocalizer<SharedResource> sharedLocalizer, IConfiguration configuration)
        {
            _context = context;
            _userManager = userManager;
            _sharedLocalizer = sharedLocalizer;
            _basePath = configuration.GetSection("Host:BaseHostPath").Value;
        }

        // GET: UserArea/Create
        [HttpGet]
        public async Task<IActionResult> SetAuthorizations(string id)
        {
            ViewData["Header"] = HeaderClassBuilders.UserArea.Index(_sharedLocalizer, _basePath, id);
            ViewData["BasePath"] = _basePath;
            ViewData["UserId"] = id;
            var user = await _userManager.FindByIdAsync(id);
            ViewData["User"] = user.Surname + " " + user.Name;
            ViewBag.UserTypes = await _context.UserTypesArea.ToListAsync();

            var oRqf = Request.HttpContext.Features.Get<IRequestCultureFeature>();
            var oCulture = oRqf.RequestCulture.Culture.Name;
            var aree = await _context.Area
                .Include(z => z.IdLinguaNavigation)
                .Where(x => x.IdLinguaNavigation.SiglaEstesa == oCulture)
                .Select(y =>
                    new HomeAreaListViewModel
                    {
                        Nome = y.Descrizione ?? "",
                        IdArea = y.Id
                    }
                ).ToListAsync();
            ViewBag.Aree = aree;

            UserAreaFormViewModel form = new()
            {
                AreaPermissions = aree

                .Select(y => new AreaPermission
                {
                    IdArea = y.IdArea,
                    Selected = false,
                    IdType = 0
                }).ToList()
            };

            foreach (var permissions in form.AreaPermissions)
            {
                var element = _context.UserArea.Where(x => x.IdUser == id && x.IdArea == permissions.IdArea);
                if (element.Any())
                {
                    permissions.Selected = true;
                    permissions.IdType = element.First().IdUserType;
                }
            }

            return View(form);
        }

        [HttpPost]
        public async Task<IActionResult> SetAuthorizations(IFormCollection collection)
        {
            try
            {

                List<UserArea> userAreas = new();

                string userId = collection["UserId"];

                _context.UserArea.RemoveRange(_context.UserArea.Where(x => x.IdUser == userId));

                foreach (var checkbox in collection.Where(x => x.Key.Contains("Area_")))
                {
                    //Questo è l'ID chiave primaria "Id"
                    var idArea = Convert.ToInt32(checkbox.Key.Split("_")[1]);
                    //Con quell'id mi prendo l'IdArea
                    var area = await _context.Area.Where(y => y.Id == idArea).Select(x => x.IdArea).FirstAsync();
                    //Con l'Id Area mi prendo tutti gli "Id" delle aree con lo stesso IdArea (preso nella riga sopra)
                    var areeWithIdArea = await _context.Area.Where(y => y.IdArea == area).Select(x => x.Id).ToListAsync();

                    var idUserType = Convert.ToInt32(collection["UserType_" + checkbox.Key.Split("_")[1]]);

                    foreach (var idAreaKey in areeWithIdArea)
                    {
                        UserArea userArea = new()
                        {
                            IdArea = idAreaKey,
                            IdUser = userId,
                            IdUserType = idUserType
                        };
                        userAreas.Add(userArea);
                    }

                }

                await _context.AddRangeAsync(userAreas);
                await _context.SaveChangesAsync();

                return RedirectToAction("Configurations", "Users", new { id = userId });
            }
            catch (Exception)
            {
                throw;
            }

        }


    }
}
