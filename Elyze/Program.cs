// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
using System.Globalization;
using Elyze.Data;
using Elyze.Helpers;
using EmailService;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using TenantService;

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IClaimGetterService, ClaimGetterService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.AllowAnyHeader();
                          policy.AllowAnyMethod();
                          policy.AllowAnyOrigin();
                      });
});

builder.Services.AddControllersWithViews();

builder.Services.AddHttpContextAccessor();

builder.Services.AddSession();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login";
    });

builder.Services.AddDbContext<UserContext>();

builder.Services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();

builder.Services.AddIdentity<AspNetUsers, IdentityRole>()
    .AddEntityFrameworkStores<UserContext>()
    .AddDefaultTokenProviders()
    .AddRoles<IdentityRole>();

builder.Services.Configure<DataProtectionTokenProviderOptions>(opt =>
   opt.TokenLifespan = TimeSpan.FromHours(2));

builder.Services.AddDbContext<ElyzeContext>();

builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");
builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    var oSupportedCultures = new List<CultureInfo>
    {
        new CultureInfo("it-IT"),
        new CultureInfo("en-US"),
        /*new CultureInfo("de-DE"),
        new CultureInfo("fr-FR"),
        new CultureInfo("en-GB")*/
    };

    options.DefaultRequestCulture = new RequestCulture("it-IT");
    options.SupportedCultures = oSupportedCultures;
    options.SupportedUICultures = oSupportedCultures;
});

builder.Services.AddMvc()
              .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix, options => options.ResourcesPath = "Resources")
              .AddDataAnnotationsLocalization();

var emailConfig = builder.Configuration
    .GetSection("EmailConfiguration")
    .Get<EmailConfiguration>();
builder.Services.AddSingleton(emailConfig);
builder.Services.AddScoped<IEmailSender, EmailSender>();

var tenantConfig = builder.Configuration
    .GetSection("Tenant:TenantConfiguration")
    .Get<TenantConfiguration>();
builder.Services.AddSingleton(tenantConfig);
builder.Services.AddScoped<ITenantService, TenantService.TenantService>();

var app = builder.Build();

var oLocOptions = app.Services.GetService<IOptions<RequestLocalizationOptions>>();

if (oLocOptions != null)
    app.UseRequestLocalization(oLocOptions.Value);

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();

}

app.UseExceptionHandler("/Home/Error");

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseCors(MyAllowSpecificOrigins);

app.UseAuthentication();
app.UseAuthorization();

//app.MapControllerRoute(
//    name: "Login",
//    pattern: "{controller=Account}/{action=Login}/{company?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
