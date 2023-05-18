// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Security.Claims;
using Elyze.Data;
using Elyze.Helpers;
using Elyze.Helpers.DataTables;
using Elyze.Helpers.Headers;
using Elyze.Models.AspNetUsers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Localization;
using Microsoft.EntityFrameworkCore;

namespace Elyze.Controllers
{

    public class UsersController : Controller
    {
        private readonly UserContext _context;
        private readonly IHtmlLocalizer<SharedResource> _sharedLocalizer;
        private readonly string _basePath;
        private readonly UserManager<AspNetUsers> _userManager;

        public UsersController(UserContext context, IHtmlLocalizer<SharedResource> sharedLocalizer, IConfiguration configuration, UserManager<AspNetUsers> userManager)
        {
            _context = context;
            _sharedLocalizer = sharedLocalizer;
            _basePath = configuration.GetSection("Host:BaseHostPath").Value;
            _userManager = userManager;
        }

        //[Authorize(Roles = "Administrator")]
        // GET: Users
        [HttpGet]
        [Authorize(Roles = UserRoles.Administrator)]
        public async Task<IActionResult> Index()
        {
            ViewData["Header"] = HeaderClassBuilders.User.Index(_sharedLocalizer, _basePath);

            return View();
        }

        //[Authorize(Roles = "Administrator")]
        // GET: Users
        [HttpGet]
        public async Task<IActionResult> Configurations(string id)
        {
            ViewData["Header"] = HeaderClassBuilders.User.Configurations(_sharedLocalizer, _basePath);
            ViewData["UserId"] = id;
            return View();
        }

        //[Authorize(Roles = "Administrator")]
        // GET: Users/Details/5
        public async Task<IActionResult> Details(string id)
        {
            ViewData["Header"] = HeaderClassBuilders.User.Details(_sharedLocalizer, _basePath);

            if (id == null)
            {
                return NotFound();
            }

            var user = await GetUserByIdForDeleteDetails(id);

            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }
        //[Authorize(Roles = "Administrator")]
        // GET: Users/Create
        [AllowAnonymous]
        public IActionResult Create()
        {
            ViewData["Header"] = HeaderClassBuilders.User.Create(_sharedLocalizer, _basePath);

            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[Authorize(Roles = "Administrator")]
        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Surname,Email,ConfirmEmail,Password,ConfirmPassword,IsAdmin")] UsersViewFormModel user)
        {
            //  var verified = Crypto.VerifyHashedPassword("ABEc1YGAsWHgI3CALZky+rS3La5njJfDO88W5GnhpaJI5uGB/ObsyfOSk8/u5Xs9YA==", user.Password); decodifica


            if (ModelState.IsValid)
            {
                var userNew = new AspNetUsers()
                {
                    Email = user.Email,
                    SecurityStamp = Guid.NewGuid().ToString(),
                    Name = user.Name ?? "",
                    Surname = user.Surname ?? "",
                    UserName = user.Name + user.Surname,
                };

                var result = await _userManager.CreateAsync(userNew, user.Password);

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(userNew, user.IsAdmin ? UserRoles.Administrator : UserRoles.Standard);
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, new { Status = "Error", Message = result.Errors });
                }
            }
            return View(user);
        }

        //[Authorize(Roles = "Administrator")]
        // GET: Users/Edit/5
        public async Task<IActionResult> EditPassword(string? id)
        {
            ViewData["Header"] = HeaderClassBuilders.User.EditPassword(_sharedLocalizer, _basePath);

            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users.Select(x => new UserViewChangePasswordModel()
            {
                Id = x.Id,
                Email = x.Email
            }).FirstOrDefaultAsync(x => x.Id == id);

            if (user == null)
            {
                return NotFound();
            }
            return View("EditPassword", user);
        }

        //Edit password view
        [HttpPost]
        public async Task<IActionResult> EditPassword(UserViewChangePasswordModel user)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var userToEdit = await GetUserByIdAspNet(user.Id);
                    if (userToEdit == null)
                    {
                        return NotFound();
                    }
                    var result = await _userManager.ChangePasswordAsync(userToEdit, user.OldPassword, user.Password);
                    if (result.Succeeded)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        return StatusCode(StatusCodes.Status500InternalServerError, new { Status = "Error", Message = result.Errors.First().Description });
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, new { Status = "Error", Message = "User Failed to edit" });
                }
            }
            return View(user);
        }


        //[Authorize(Roles = "Administrator")]
        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(string? id)
        {
            ViewData["Header"] = HeaderClassBuilders.User.Edit(_sharedLocalizer, _basePath);

            if (id == null)
            {
                return NotFound();
            }

            var user = await GetUserByIdForEdit(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[Authorize(Roles = "Administrator")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,Name,Surname,Email,ConfirmEmail, OldPassword, Password,ConfirmPassword,IsAdmin")] UserViewEditFormModel user)
        {
            if (id != user.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var userToEdit = await GetUserByIdAspNet(id);

                    if (userToEdit == null)
                    {
                        return NotFound();
                    }

                    var res2 = await _userManager.RemoveFromRoleAsync(userToEdit, UserRoles.Administrator);
                    var res3 = await _userManager.RemoveFromRoleAsync(userToEdit, UserRoles.Standard);
                    var res4 = await _userManager.AddToRoleAsync(userToEdit, user.IsAdmin ? UserRoles.Administrator : UserRoles.Standard);

                    userToEdit.Name = user.Name;
                    userToEdit.Surname = user.Surname;
                    _context.Users.Update(userToEdit);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _context.Users.AnyAsync(x => x.Id == id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }
        //[Authorize(Roles = "Administrator")]
        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(string? id)
        {
            ViewData["Header"] = HeaderClassBuilders.User.Delete(_sharedLocalizer, _basePath);

            if (id == null)
            {
                return NotFound();
            }

            var user = await GetUserByIdForDeleteDetails(id);

            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: Users/Delete/5
        //[Authorize(Roles = "Administrator")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var userToDelete = await GetUserByIdAspNet(id);

            if (userToDelete == null)
            {
                return NotFound();
            }

            var user = await _userManager.DeleteAsync(userToDelete);

            return RedirectToAction(nameof(Index));
        }


        //[Authorize(Roles = "Administrator, Standard")]
        public async Task<IActionResult> ChangeUsrData()
        {
            if (User.Identity is not ClaimsIdentity claimsIdentity)
            {
                return NotFound();
            }
            if (claimsIdentity == null)
            {
                return NotFound();
            }
            var usrid = claimsIdentity.FindFirst("id");
            if (usrid == null)
            {
                return NotFound();
            }
            var user = await _context.Users.FindAsync(Convert.ToInt32(usrid.Value));
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }
        //[Authorize(Roles = "Administrator, Standard")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangeUsrData([Bind("UserId,Nome,Cognome,Mail,Password,Admin")] AspNetUsers user)
        {
            if (User.Identity is not ClaimsIdentity claimsIdentity)
            {
                return NotFound();
            }
            var usrid = claimsIdentity.FindFirst("id");
            if (usrid == null)
            {
                return NotFound();
            }


            if (ModelState.IsValid)
            {
                try
                {
                    //var oldata = await GetUsr(user.Id);

                    //user.Name ??= oldata.Name;
                    //user.Surname ??= oldata.Surname;
                    //user.Email ??= oldata.Email;
                    //user.ad = oldata.Admin;
                    //user.Password ??= System.Web.Helpers.Crypto.HashPassword(user.Password);
                    //user.Password ??= oldata.Password;



                    //_context.Update(user);
                    //await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    //if (!UserExists(user.UserId))
                    //{
                    //    return NotFound();
                    //}
                    //else
                    //{
                    //    throw;
                    //}
                }
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("Index", "Home");
        }



        private async Task<UserViewDetailsDeleteFormModel?> GetUserByIdForDeleteDetails(string id)
        {
            var query = from u in _context.Users
                        join ur in _context.UserRoles on u.Id equals ur.UserId
                        join r in _context.Roles on ur.RoleId equals r.Id
                        where u.Id == id
                        select new UserViewDetailsDeleteFormModel()
                        {
                            Id = u.Id,
                            Name = u.Name,
                            Surname = u.Surname,
                            Email = u.Email,
                            NameRole = r.Name
                        };
            return await query.SingleAsync();
        }

        private async Task<UserViewEditFormModel?> GetUserByIdForEdit(string id)
        {
            var query = from u in _context.Users
                        join ur in _context.UserRoles on u.Id equals ur.UserId
                        join r in _context.Roles on ur.RoleId equals r.Id
                        where u.Id == id
                        select new UserViewEditFormModel()
                        {
                            Id = u.Id,
                            Name = u.Name,
                            Surname = u.Surname,
                            Email = u.Email,
                            IsAdmin = r.Name == UserRoles.Administrator,
                        };

            return await query.SingleAsync();
        }

        [HttpPost]
        public async Task<IActionResult> GetUsersDataTable()
        {
            var form = HelpersDataTables.ContextToDatatable(Request);

            DataTableResult result = new()
            {
                Draw = form.Draw,
                RecordsTotal = await _context.Users.CountAsync()
            };

            var query = from u in _context.Users
                        join ur in _context.UserRoles on u.Id equals ur.UserId
                        join r in _context.Roles on ur.RoleId equals r.Id
                        where (form.SearchValue.Equals("") ||
                            (u.Name != null && u.Name.ToLower().Contains(form.SearchValue)) ||
                            (u.Surname != null && u.Surname.ToLower().Contains(form.SearchValue)) ||
                            (u.Email != null && u.Email.ToLower().Contains(form.SearchValue)) ||
                            (r.Name != null && r.Name.ToLower().Contains(form.SearchValue)))
                        select new UsersViewDataTableModel()
                        {
                            Id = u.Id,
                            Name = u.Name,
                            Surname = u.Surname,
                            Email = u.Email,
                            NameRole = r.Name,
                        };

            if (_sharedLocalizer["Name"].Value == form.SortColumn)
            {
                query = form.SortColumnDirection == "asc" ? query.OrderBy(x => x.Name) : query.OrderByDescending(x => x.Name);
            }
            else
            if (_sharedLocalizer["Surname"].Value == form.SortColumn)
            {
                query = form.SortColumnDirection == "asc" ? query.OrderBy(x => x.Surname) : query.OrderByDescending(x => x.Surname);
            }
            else
            if (_sharedLocalizer["Mail"].Value == form.SortColumn)
            {
                query = form.SortColumnDirection == "asc" ? query.OrderBy(x => x.Email) : query.OrderByDescending(x => x.Email);
            }
            else
            if (_sharedLocalizer["Role"].Value == form.SortColumn)
            {
                query = form.SortColumnDirection == "asc" ? query.OrderBy(x => x.NameRole) : query.OrderByDescending(x => x.NameRole);
            }
            else
            {
                query = query.OrderBy(x => x.Id);
            }

            result.RecordsFiltered = query.Count();
            result.Data = await query.Skip(form.Start).Take(form.Length).Cast<object>().ToListAsync();

            return Json(result);
        }

        private async Task<AspNetUsers?> GetUserByIdAspNet(string id)
        {
            return await _context.Users.FindAsync(id);
        }






    }
}
