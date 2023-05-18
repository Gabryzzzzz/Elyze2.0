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
    public class UserGruppoController : Controller
    {
        private readonly ElyzeContext _context;
        private readonly UserManager<AspNetUsers> _userManager;
        private readonly IHtmlLocalizer<SharedResource> _sharedLocalizer;
        private readonly string _basePath;
        public UserGruppoController(ElyzeContext context, UserManager<AspNetUsers> userManager, IHtmlLocalizer<SharedResource> sharedLocalizer, IConfiguration configuration)
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
            ViewBag.Gruppi = _context.Gruppo.ToList();

            ViewData["Header"] = HeaderClassBuilders.UserGruppo.Index(_sharedLocalizer, _basePath, id);
            ViewData["BasePath"] = _basePath;
            ViewData["UserId"] = id;
            var user = await _userManager.FindByIdAsync(id);
            ViewData["User"] = user.Surname + " " + user.Name;


            UserGruppoFormViewModel form = new()
            {
                GruppoPermissions = _context.Gruppo.Select(y => new GruppoPermission
                {
                    IdGruppo = y.Id,
                    Selected = false
                }).ToList()
            };

            foreach (var permissions in form.GruppoPermissions)
            {
                var element = _context.UserGruppo.Where(x => x.IdUser == id && x.IdGruppo == permissions.IdGruppo);
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

            _context.UserGruppo.RemoveRange(_context.UserGruppo.Where(x => x.IdUser == userId));

            List<Data.UserGruppo> userGruppos = new();

            foreach (var checkbox in collection.Where(x => x.Key.Contains("Gruppo_")))
            {
                var idGruppo = Convert.ToInt32(checkbox.Key.Split("-")[1]);

                Data.UserGruppo userGruppo = new()
                {
                    IdUser = userId,
                    IdGruppo = idGruppo,
                };
                userGruppos.Add(userGruppo);
            }

            _context.UserGruppo.AddRange(userGruppos);
            await _context.SaveChangesAsync();

            return RedirectToAction("Configurations", "Users", new { id = userId });
        }
    }
}
