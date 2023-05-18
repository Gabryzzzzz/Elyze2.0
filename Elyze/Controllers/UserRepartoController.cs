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
    public class UserRepartoController : Controller
    {
        private readonly ElyzeContext _context;
        private readonly UserManager<AspNetUsers> _userManager;
        private readonly IHtmlLocalizer<SharedResource> _sharedLocalizer;
        private readonly string _basePath;
        public UserRepartoController(ElyzeContext context, UserManager<AspNetUsers> userManager, IHtmlLocalizer<SharedResource> sharedLocalizer, IConfiguration configuration)
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
            ViewBag.Reparti = _context.Reparto.ToList();

            ViewData["Header"] = HeaderClassBuilders.UserReparto.Index(_sharedLocalizer, _basePath, id);
            ViewData["BasePath"] = _basePath;
            ViewData["UserId"] = id;
            var user = await _userManager.FindByIdAsync(id);
            ViewData["User"] = user.Surname + " " + user.Name;


            UserRepartoFormViewModel form = new()
            {
                RepartoPermissions = _context.Reparto.Select(y => new RepartoPermission
                {
                    IdReparto = y.Id,
                    Selected = false
                }).ToList()
            };

            foreach (var permissions in form.RepartoPermissions)
            {
                var element = _context.UserReparto.Where(x => x.IdUser == id && x.IdReparto == permissions.IdReparto);
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

            _context.UserReparto.RemoveRange(_context.UserReparto.Where(x => x.IdUser == userId));

            List<Data.UserReparto> userReparti = new();

            foreach (var checkbox in collection.Where(x => x.Key.Contains("Reparto_")))
            {
                var IdReparto = Convert.ToInt32(checkbox.Key.Split("_")[1]);

                Data.UserReparto userReparto = new()
                {
                    IdUser = userId,
                    IdReparto = IdReparto,
                };
                userReparti.Add(userReparto);
            }

            _context.UserReparto.AddRange(userReparti);
            await _context.SaveChangesAsync();

            return RedirectToAction("Configurations", "Users", new { id = userId });
        }
    }
}
