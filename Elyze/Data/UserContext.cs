// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
using System.Reflection.Emit;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TenantService;

namespace Elyze.Data
{
    public class UserContext : IdentityDbContext<AspNetUsers>
    {
        public IConfiguration _configuration { get; }
        private readonly IHttpContextAccessor _httpContext;
        private readonly ITenantService _tenantService;

        public UserContext(DbContextOptions<UserContext> options, IConfiguration configuration, IHttpContextAccessor httpContextAccessor, ITenantService tenantService) : base(options)
        {
            _configuration = configuration;
            _httpContext = httpContextAccessor;
            _tenantService = tenantService;
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<AspNetUsers>()
                .Property(b => b.Attivo)
                .HasDefaultValue(true);
            base.OnModelCreating(builder);

            string standardRoleId = "3DDBE80D-AC50-4288-8CEB-4298FF4C37F2";
            string adminRoleId = "24DD64C7-EB35-4DBE-935A-B285FDCA0B27";
            builder.Entity<IdentityRole>().HasData(
                new IdentityRole() { Id = standardRoleId, Name = "Standard", NormalizedName = "STANDARD" },
                new IdentityRole() { Id = adminRoleId, Name = "Administrator", NormalizedName = "ADMINISTRATOR" });


        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            bool isTesting = _configuration.GetSection("Tenant:Test:IsTesting").Get<bool>();
            if (isTesting)
            {
                string connection = _configuration.GetSection("Tenant:Test:ConnectionTest").Get<string>();
                optionsBuilder.UseSqlServer(connection);
                return;
            }

            bool isOnline = _configuration.GetSection("Tenant:IsOnLine").Get<bool>();
            if (!isOnline)
            {
                string onPremiseConnectionString = _configuration.GetSection("Tenant:OnPremiseConfigurarion:OnPremiseConnectionString").Get<string>();
                optionsBuilder.UseSqlServer(onPremiseConnectionString);
                return;
            }

            string? company = this._httpContext?.HttpContext?.Request.Cookies["Company"];
            if (string.IsNullOrWhiteSpace(company) && isOnline)
            {
                throw new Exception("Invalid Tenant");
            }
            else
            {
                bool isMultiTenant = _configuration.GetSection("Tenant:IsMultiTenant").Get<bool>();
                if(!isMultiTenant)
                {
                    string singleTenantConnection = _configuration.GetSection("Tenant:SingleTenantContext").Get<string>();
                    optionsBuilder.UseSqlServer(singleTenantConnection);
                }
                else
                {
                    TenantResponse tenantResponse = _tenantService.GetTenant(company);
                    if (tenantResponse == null)
                    {
                        throw new TenantService.TenantException("Invalid Response Tenant");
                    }

                    optionsBuilder.UseSqlServer(tenantResponse.ConnectionString);
                }
            }
        }
    }
}

