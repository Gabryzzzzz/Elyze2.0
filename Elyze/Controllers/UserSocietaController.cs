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
    public class UserSocietaController : Controller
    {
        private readonly ElyzeContext _context;
        private readonly UserManager<AspNetUsers> _userManager;
        private readonly IHtmlLocalizer<SharedResource> _sharedLocalizer;
        private readonly string _basePath;

        public UserSocietaController(ElyzeContext context, UserManager<AspNetUsers> userManager, IHtmlLocalizer<SharedResource> sharedLocalizer, IConfiguration configuration)
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
            ViewBag.Societa = _context.Societa.ToList();

            ViewData["Header"] = HeaderClassBuilders.UserSocieta.Index(_sharedLocalizer, _basePath, id);
            ViewData["BasePath"] = _basePath;
            ViewData["UserId"] = id;
            var user = await _userManager.FindByIdAsync(id);
            ViewData["User"] = user.Surname + " " + user.Name;

            UserSocietaFormViewModel form = new()
            {
                SocietaPermissions = _context.Societa.Select(y => new SocietaPermission
                {
                    IdSocieta = y.Id,
                    Selected = false
                }).ToList()
            };

            foreach (var permissions in form.SocietaPermissions)
            {
                var element = _context.UserSocieta.Where(x => x.IdUser == id && x.IdSocieta == permissions.IdSocieta);
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

            _context.UserSocieta.RemoveRange(_context.UserSocieta.Where(x => x.IdUser == userId));

            List<Data.UserSocieta> userSocietas = new();

            foreach (var checkbox in collection.Where(x => x.Key.Contains("Societa_")))
            {
                var IdSocieta = Convert.ToInt32(checkbox.Key.Split("_")[1]);

                Data.UserSocieta userSocieta = new()
                {
                    IdUser = userId,
                    IdSocieta = IdSocieta,
                };
                userSocietas.Add(userSocieta);
            }

            _context.UserSocieta.AddRange(userSocietas);
            await _context.SaveChangesAsync();

            return RedirectToAction("Configurations", "Users", new { id = userId });
        }
    }
}
