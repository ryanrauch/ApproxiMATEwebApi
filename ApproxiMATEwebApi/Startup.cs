using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ApproxiMATEwebApi.Data;
using ApproxiMATEwebApi.Models;
using ApproxiMATEwebApi.Services;
using ApproxiMATEwebApi.Authorization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using ApproxiMATEwebApi.Helpers;

namespace ApproxiMATEwebApi
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
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString(Constants.DefaultSQLConnectionStringKey)));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.AddScoped<IUserClaimsPrincipalFactory<ApplicationUser>, ApplicationUserClaimsPrincipalFactory>();

            // Add application services.
            services.AddTransient<IEmailSender, EmailSender>();

            services.AddTransient<IRegionFunctions, RegionFunctionsRandolphFranklin> ();

            //JWT??? make sure this is in correct order
            //services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            //        .AddJwtBearer(options =>
            //        {
            //            options.TokenValidationParameters = new TokenValidationParameters
            //            {
            //                ValidateIssuer = true,
            //                ValidateAudience = true,
            //                ValidateLifetime = true,
            //                ValidateIssuerSigningKey = true,
            //                ValidIssuer = Configuration["Jwt:Issuer"],
            //                ValidAudience = Configuration["Jwt:Issuer"],
            //                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]))
            //            };
            //        });
            //var jwtAppSettingOptions = Configuration.GetSection(nameof(JwtIssuerOptions));
            //services.Configure<JwtIssuerOptions>(options =>
            //{
            //    options.Issuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)];
            //    options.Audience = jwtAppSettingOptions[nameof(JwtIssuerOptions.Audience)];
            //    options.SigningCredentials = new SigningCredentials(_signingKey, SecurityAlgorithms.HmacSha256);
            //});

            services.AddMvc();

            //Jwt added from example here:
            //github.com/ruidfigueiredo/WebApiJwtExample/blob/master/Startup.cs
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = "Jwt";
                options.DefaultChallengeScheme = "Jwt";
            })
            //.AddCookie(cfg=>cfg.SlidingExpiration = true) //this is only added for dual-auth
            .AddJwtBearer("Jwt", options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateAudience = false,
                    //ValidAudience = "the audience you want to validate",
                    ValidateIssuer = false,
                    //ValidIssuer = "the isser you want to validate",
                    ValidateIssuerSigningKey = true,
                    //IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Constants.JwtSecretNeedsToBeSecured)),
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration[Constants.JwtSecretKey])),
                    ValidateLifetime = true, //validate the expiration and not before values in the token
                    ClockSkew = TimeSpan.FromMinutes(5) //5 minute tolerance for the expiration date
                };
            });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("AdministratorPolicy", policy =>
                    policy.Requirements.Add(new AdministratorRequirement(AccountType.Administrative)));
            });
            
            //TODO:
            //claim types do not work with JwtBearer token??

            services.AddSingleton<IAuthorizationHandler, AdministratorHandler>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
