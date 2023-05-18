// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Elyze.Data;
using Elyze.Helpers.Headers;
using Elyze.Models.UserPermissions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Localization;

namespace Elyze.Controllers
{
    public class UserSedeController : Controller
    {
        private readonly ElyzeContext _context;
        private readonly UserManager<AspNetUsers> _userManager;
        private readonly IHtmlLocalizer<SharedResource> _sharedLocalizer;
        private readonly string _basePath;
        public UserSedeController(ElyzeContext context, UserManager<AspNetUsers> userManager, IHtmlLocalizer<SharedResource> sharedLocalizer, IConfiguration configuration)
        {
            _context = context;
            _userManager = userManager;
            _sharedLocalizer = sharedLocalizer;
            _basePath = configuration.GetSection("Host:BaseHostPath").Value;
        }

        // GET: UserGruppo/Create
        [HttpGet]
        public async Task<IActionResult> SetAuthorizations(string id)
        {
            ViewBag.Sedi = _context.Sede.ToList();

            ViewData["Header"] = HeaderClassBuilders.UserSede.Index(_sharedLocalizer);
            ViewData["BasePath"] = _basePath;
            ViewData["UserId"] = id;
            var user = await _userManager.FindByIdAsync(id);
            ViewData["User"] = user.Surname + " " + user.Name;


            UserSedeFormViewModel form = new()
            {
                SedePermissions = _context.Sede.Select(y => new SedePermission
                {
                    IdSede = y.Id,
                    Selected = false
                }).ToList()
            };

            foreach (var permissions in form.SedePermissions)
            {
                var element = _context.UserSede.Where(x => x.IdUser == id && x.IdSede == permissions.IdSede);
                if (element.Any())
                {
                    permissions.Selected = true;
                }
            }


            return View(form);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SetAuthorizations(IFormCollection collection)
        {
            string userId = collection["UserId"];

            _context.UserSede.RemoveRange(_context.UserSede.Where(x => x.IdUser == userId));

            List<Data.UserSede> userSedi = new();

            foreach (var checkbox in collection.Where(x => x.Key.Contains("Sede_")))
            {
                var IdSede = Convert.ToInt32(checkbox.Key.Split("_")[1]);

                Data.UserSede userSede = new()
                {
                    IdUser = userId,
                    IdSede = IdSede,
                };
                userSedi.Add(userSede);
            }

            _context.UserSede.AddRange(userSedi);
            await _context.SaveChangesAsync();

            return RedirectToAction("Configurations", "Users", new { id = userId });
        }
    }
}
