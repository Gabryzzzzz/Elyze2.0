// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Security.Claims;
using Elyze.Data;
using Microsoft.AspNetCore.Identity;

namespace Elyze.Helpers
{

    public interface IClaimGetterService
    {
        string? GetSurnameClaim();
        string? GetUserClaim(string claim = ClaimTypes.Email);
        Task<bool> IsAdmin();
        Task<bool> IsAdminByEmail(string email);
        bool IsLogged();
        string? UserId();
    }

    //make a class to store the name of the roles
    public static class UserRoles
    {
        public const string Administrator = "Administrator";
        public const string Standard = "Standard";
        public const string Both = $"Administrator,Standard";
    }

    public class ClaimGetterService : IClaimGetterService
    {

        //contexthttp
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<AspNetUsers> _userManager;

        //costructor
        public ClaimGetterService(IHttpContextAccessor httpContextAccessor, UserManager<AspNetUsers> userManager)
        {
            _httpContextAccessor = httpContextAccessor;
            this._userManager = userManager;
        }

        //get claim
        public string? GetSurnameClaim()
        {
            if (_httpContextAccessor.HttpContext == null)
            {
                return null;
            }
            return _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Name)?.Value;
        }

        public string? GetUserClaim(string claim = ClaimTypes.Email)
        {
            if (_httpContextAccessor.HttpContext == null)
            {
                return null;
            }
            return _httpContextAccessor.HttpContext.User.FindFirst(claim)?.Value;
        }

        public async Task<bool> IsAdmin()
        {
            //check if user is admin with the userManager, get the email with the GetUserClaim method
            var user = await _userManager.FindByEmailAsync(GetUserClaim());
            //check if user is admin
            return await _userManager.IsInRoleAsync(user, UserRoles.Administrator);
        }

        public async Task<bool> IsAdminByEmail(string email)
        {
            //check if user is admin with the userManager, get the email with the GetUserClaim method
            var user = await _userManager.FindByEmailAsync(email);
            //check if user is admin
            return await _userManager.IsInRoleAsync(user, UserRoles.Administrator);
        }

        //function that check if user is logged
        public bool IsLogged()
        {
            if (_httpContextAccessor.HttpContext == null)
            {
                return false;
            }
            if (_httpContextAccessor.HttpContext.User.Identity == null)
            {
                return false;
            }
            //check if user is logged in the context
            return _httpContextAccessor.HttpContext.User.Identity.IsAuthenticated;

        }

        public string UserId()
        {
            if (_httpContextAccessor.HttpContext == null || _httpContextAccessor.HttpContext.User != null)
            {
                return "";
            }
            var userId = _userManager.GetUserId(_httpContextAccessor.HttpContext.User);
            return userId;
        }

    }
}
