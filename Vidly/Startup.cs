using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vidly.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using AutoMapper;
using Microsoft.AspNetCore.Identity.UI.Services;
using Vidly.Areas.Identity.Services;
using Vidly.Models;
using Microsoft.AspNetCore.Rewrite;

namespace Vidly
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>()
           .AddEntityFrameworkStores<ApplicationDbContext>()
           .AddUserManager<ApplicationUserManager>()
           .AddDefaultTokenProviders();


            //Password Strength Setting 
            services.Configure<IdentityOptions>(options =>
            {
                // Password settings 
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 8;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = true;
                options.Password.RequireLowercase = false;
                options.Password.RequiredUniqueChars = 6;

                // Lockout settings 
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
                options.Lockout.MaxFailedAccessAttempts = 10;
                options.Lockout.AllowedForNewUsers = true;

                // User settings 
                options.User.RequireUniqueEmail = true;
            });

            //Setting the Account Login page 
            services.ConfigureApplicationCookie(options =>
            {
                // Cookie settings 
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
                options.LoginPath = "/Account/Login"; // If the LoginPath is not set here,  
                                                      // ASP.NET Core will default to /Account/Login 
                options.LogoutPath = "/Account/Logout"; // If the LogoutPath is not set here,  
                                                        // ASP.NET Core will default to /Account/Logout 
                options.AccessDeniedPath = "/Account/AccessDenied"; // If the AccessDeniedPath is  
                                                                    // not set here, ASP.NET Core  
                                                                    // will default to  
                                                                    // /Account/AccessDenied 
                options.SlidingExpiration = true;
            });


            //authorization policy for global authorization filter below
            //var pol = new AuthorizationPolicyBuilder()
            //        .RequireAuthenticatedUser()
            //        .RequireRole("Admin", "HR")
            //        .Build();

            services.AddMvc(options =>
            {
                //options.Filters.Add(new AuthorizeFilter(pol));//global authorization filter
            })
            .SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
            .AddRazorPagesOptions(options =>
            {
                options.AllowAreas = true;
                options.Conventions.AuthorizeAreaFolder("Identity", "/Account/Manage");
                options.Conventions.AuthorizeAreaPage("Identity", "/Account/Logout");
                //role based authorization for Razor pages
                // options.Conventions.AuthorizePage("/customers/CustomerForm", "OnlyAdminAccess");
            })
            .AddXmlSerializerFormatters().AddXmlDataContractSerializerFormatters();

            services.AddSingleton<IEmailSender, EmailSender>();

            //Policy based role checks
            services.AddAuthorization(options =>
            {
                options.AddPolicy("OnlyAdminAccess", policy => policy.RequireRole("User"));
                options.AddPolicy("CanManageMovies", policy => policy.RequireUserName("admin@vidly.com"));
            });

            ////Facebook login
            services.AddAuthentication()
            .AddFacebook(facebookOptions =>
            {
                facebookOptions.AppId = Configuration["Authentication:Facebook:AppId"];
                facebookOptions.AppSecret = Configuration["Authentication:Facebook:AppSecret"];
            })
            .AddGoogle(googleOptions =>
            {
                googleOptions.ClientId = Configuration["Authentication:Google:ClientId"];
                googleOptions.ClientSecret = Configuration["Authentication:Google:ClientSecret"];
            });
            services.AddTransient<IEmailSender, EmailSender>();
            services.AddAutoMapper();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider serviceProvider,  UserManager<ApplicationUser> userManager)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseAuthentication();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            // Workaround for AccessDenied URL error in MSFT code - redirects to login page
            RewriteOptions rewrite = new RewriteOptions().AddRedirect("^Account/AccessDenied(.*)", "Identity/Account/Login");
            app.UseRewriter(rewrite);
        }
    }
}
