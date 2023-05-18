// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.DirectoryServices.Protocols;
using System.Net;
using System.Security.Claims;
using Elyze.Data;
using Elyze.Helpers;
using Elyze.Models.Account;
using Elyze.Models.AspNetUsers;
using EmailService;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Elyze.Controllers
{
    //[Authorize]
    public class AccountController : Controller
    {
        private readonly UserManager<AspNetUsers> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<AspNetUsers> _signInManager;
        private readonly IConfiguration _configuration;
        private readonly IEmailSender _emailSender;
        private readonly ElyzeContext _context;
        private readonly string _basePath;

        public AccountController(UserManager<AspNetUsers> userManager, RoleManager<IdentityRole> roleManager, SignInManager<AspNetUsers> signInManager, IConfiguration configuration, IEmailSender emailSender, ElyzeContext context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
            _signInManager = signInManager;
            _emailSender = emailSender;
            _context = context;
            _basePath = configuration.GetSection("Host:BaseHostPath").Value;

        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult AccessDenied()
        {
            ViewData["BasePath"] = _basePath;
            return View();
        }

        private bool LdapLogin(string userName, string password)
        {
            try
            {
                string ldapDirectory = _configuration.GetSection("Tenant;LDAPDirectory").Get<string>();
                int ldapPort = _configuration.GetSection("Tenant:LDAPPort").Get<int>();

                LdapDirectoryIdentifier ldapDirectoryIdentifier = new LdapDirectoryIdentifier(ldapDirectory, ldapPort);
                LdapConnection ldapConnection = new LdapConnection(ldapDirectoryIdentifier);
                ldapConnection.Bind(new NetworkCredential(userName, password));

            }
            catch
            {
                return false;
            }

            return true;
        }

        //[Authorize(Roles = UserRoles.Standard)]
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        //[Authorize(Roles = UserRoles.Both)]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            string? company = Request.Cookies["Company"];
            if (string.IsNullOrEmpty(company))
            {
                return RedirectToAction(nameof(AccountController.Login), "Account");
            }
            else
            {
                return RedirectToAction(nameof(AccountController.Login), "Account", new { id = company });
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login(string? id)
        {
            AccountLoginFormViewModel userFormViewModel = new AccountLoginFormViewModel();
            try
            {
                bool isOnLine = _configuration.GetSection("Tenant:IsOnLine").Get<bool>();
                if (!isOnLine)
                {
                    userFormViewModel.UrlCompliance = true;
                    return View(userFormViewModel);
                }

                if (!string.IsNullOrEmpty(id))
                {
                    Response.Cookies.Append("Company", id);
                    userFormViewModel.UrlCompliance = true;
                }
                else
                {
                    userFormViewModel.UrlCompliance = false;
                }

                return View(userFormViewModel);
            }
            catch (Exception ex)
            {
                return View(userFormViewModel);
            }
        }

        //[HttpPost("/Account/Login/{company}")]
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(AccountLoginFormViewModel accountFormViewModel/*, [FromRoute(Name = "company")] string route*/)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    ModelState.AddModelError("", "Invalid login attempt");
                    return View(accountFormViewModel);
                }

                AspNetUsers? user = await _userManager.FindByEmailAsync(accountFormViewModel.Email);
                if (user == null)
                {
                    ModelState.AddModelError("", "Invalid UserName or Password");
                    return View(accountFormViewModel);
                }

                if (!await _userManager.CheckPasswordAsync((AspNetUsers)user, accountFormViewModel.Password))
                {
                    ModelState.AddModelError("", "Invalid UserName or Password");
                    return View(accountFormViewModel);
                }
                else
                {
                    //Check if user is admin
                    var isAdmin = await _userManager.IsInRoleAsync(user, "Administrator");

                    var identity = new ClaimsIdentity(IdentityConstants.ApplicationScheme);
                    identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Id));
                    identity.AddClaim(new Claim(ClaimTypes.Name, user.Name));
                    identity.AddClaim(new Claim(ClaimTypes.Surname, user.Surname));
                    identity.AddClaim(new Claim(ClaimTypes.Email, user.Email));
                    identity.AddClaim(new Claim(ClaimTypes.Role, isAdmin ? "Administrator" : "Standard"));
                    identity.AddClaim(new Claim(ClaimTypes.Expiration, DateTime.UtcNow.AddHours(8).ToString()));

                    ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(identity);
                    await HttpContext.SignInAsync(IdentityConstants.ApplicationScheme, claimsPrincipal);

                    return RedirectToAction(nameof(HomeController.Index), "Home");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Invalid login attempt");
                return View(accountFormViewModel);
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ForgotPassword()
        {
            ForgotPasswordFormViewModel _ForgotPasswordFormViewModel = new ForgotPasswordFormViewModel();
            return View("ForgotPassword", _ForgotPasswordFormViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordFormViewModel forgotPasswordModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(forgotPasswordModel);
                }

                var user = await _userManager.FindByEmailAsync(forgotPasswordModel.Email);
                if (user == null)
                {
                    return RedirectToAction(nameof(ForgotPasswordConfirmation));
                }

                var token = await _userManager.GeneratePasswordResetTokenAsync(user);

                string company = Request.Cookies["Company"];

                string callback = Url.Action(nameof(ResetPassword), "Account", new { token, email = user.Email, tenant = company }, Request.Scheme);

                var mail = (from _mail in _context.TemplateMail
                            join _tipoMail in _context.TipologiaMail on new { TipoMailId = _mail.TipoMailId } equals new { TipoMailId = _tipoMail.Id }
                            where _tipoMail.Descrizione.ToUpper() == SiteCostants.TEMPLATE_MAIL_RESET_PASSWORD
                            select new
                            {
                                Oggetto = _mail.TemplateOggetto,
                                Corpo = String.Format(@_mail.TemplateCorpo, callback)
                            })
                            .FirstOrDefault();

                if (mail == null)
                {
                    return NotFound();
                }

                HttpStatusCode statusCode = _emailSender.SendMailAsync(mail.Oggetto, mail.Corpo, user.Email).Result;

            }
            catch (Exception ex)
            {
                //TODO trycatch
            }

            return RedirectToAction(nameof(ForgotPasswordConfirmation));
        }

        [AllowAnonymous]
        public IActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ResetPassword(string token, string email, string tenant)
        {
            ResetPasswordFormViewModel model = new ResetPasswordFormViewModel { Token = token, Email = email, Company = tenant };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<IActionResult> ResetPassword(ResetPasswordFormViewModel resetPasswordModel)
        {
            if (!ModelState.IsValid)
            {
                return View(resetPasswordModel);
            }

            AspNetUsers user = await _userManager.FindByEmailAsync(resetPasswordModel.Email);
            if (user == null)
            {
                RedirectToAction(nameof(ResetPasswordConfirmation));
            }

            IdentityResult resetPassResult = await _userManager.ResetPasswordAsync(user, resetPasswordModel.Token, resetPasswordModel.Password);
            if (!resetPassResult.Succeeded)
            {
                foreach (var error in resetPassResult.Errors)
                {
                    ModelState.TryAddModelError(error.Code, error.Description);
                }

                return View();
            }

            return RedirectToAction(nameof(ResetPasswordConfirmation), new { tenant = resetPasswordModel.Company });
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ResetPasswordConfirmation(string tenant)
        {
            return View("ResetPasswordConfirmation", tenant);
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Register([FromBody] UsersViewFormModel registerUser)
        {
            if (registerUser == null)
            {
                return NotFound();
            }
            var userExist = await _userManager.FindByEmailAsync(registerUser.Email);
            if (userExist != null)
            {
                return StatusCode(StatusCodes.Status403Forbidden, new { Status = "Error", Message = "User Already Exists!" });
            }

            var userNew = new AspNetUsers()
            {
                UserName = registerUser.Email,
                Email = registerUser.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                Name = registerUser.Name ?? "",
                Surname = registerUser.Surname ?? ""
            };

            var result = await _userManager.CreateAsync(userNew, registerUser.Password);
            if (result.Succeeded)
            {
                return StatusCode(StatusCodes.Status201Created, new { Status = "Success", Message = "User Created SuccssFully!" });
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Status = "Error", Message = "User Failed to create" });
            }
        }

        //public async Task<IActionResult> PowerBi()
        //{
        //    string powerBILink = "";
        //    bool isExternal = _configuration.GetSection("BI:IsExternal").Get<bool>();
        //    if (isExternal)
        //    {
        //        powerBILink = "";
        //    }
        //    else
        //    {
        //        powerBILink = _configuration.GetSection("BI:BILink").Get<string>();
        //    }

        //    return View(powerBILink);
        //}
    }
}
