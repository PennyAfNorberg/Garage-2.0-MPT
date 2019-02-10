using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Garage_2._0_MPT.Models;

namespace Garage_2._0_MPT
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


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            var migrationAssembly = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;
            services.AddDbContext<Garage_2_0_MPTContext>(opt => opt.UseSqlServer(Configuration.GetConnectionString("Garage_2_0_MPTContext"),
                sql => sql.MigrationsAssembly(migrationAssembly)));
            services.AddIdentity<GarageUser, IdentityRole>(options =>
            {
                //options.SignIn.RequireConfirmedEmail = true;
                options.Tokens.EmailConfirmationTokenProvider = "emailconf";

                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredUniqueChars = 4;

                options.User.RequireUniqueEmail = true;

                options.Lockout.AllowedForNewUsers = true;
                options.Lockout.MaxFailedAccessAttempts = 3;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(10);
            })
    .AddEntityFrameworkStores<Garage_2_0_MPTContext>()
    .AddDefaultTokenProviders()
    .AddTokenProvider<EmailConfirmationTokenProvider<GarageUser>>("emailconf")
    .AddPasswordValidator<DoesNotContainPasswordValidator<GarageUser>>();

            services.AddScoped<IUserClaimsPrincipalFactory<GarageUser>,
    GarageUserClaimsPrincipalFactory>();

            services.Configure<DataProtectionTokenProviderOptions>(options =>
       options.TokenLifespan = TimeSpan.FromHours(3));
            services.Configure<EmailConfirmationTokenProviderOptions>(options =>
                options.TokenLifespan = TimeSpan.FromDays(2));

            services.ConfigureApplicationCookie(options => options.LoginPath = "/ParkedVehicles/Login");
          


           services.AddAuthentication()
    .AddFacebook("facebook", options =>
    {
        options.AppId =Configuration.GetSection("facebook")["AppId"];
        options.AppSecret = Configuration.GetSection("facebook")["AppSecret"];
        options.SignInScheme = IdentityConstants.ExternalScheme;
    });

            //  services.AddDbContext<Garage_2_0_MPTContext>(options =>
            //         options.UseSqlServer(Configuration.GetConnectionString("Garage_2_0_MPTContext")));


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            
            app.UseHttpsRedirection();

            app.UseAuthentication();

            app.UseStaticFiles();
            app.UseCookiePolicy();
            // ---
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=ParkedVehicles}/{action=Index}/{id?}");
            });
        }
    }
}
