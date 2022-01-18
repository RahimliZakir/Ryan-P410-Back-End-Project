using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using RyanP410.WebUI.AppCode.Providers;
using RyanP410.WebUI.Models.DataContexts;
using RyanP410.WebUI.Models.Entities.Membership;

namespace RyanP410.WebUI.AppCode.Extensions
{
    public static partial class Extension
    {
        public static IServiceCollection AddFluentIdentity(this IServiceCollection services)
        {
            services.AddIdentity<RyanUser, RyanRole>()
                    .AddEntityFrameworkStores<RyanDbContext>()
                    .AddDefaultTokenProviders();

            services.AddScoped<UserManager<RyanUser>>()
                    .AddScoped<RoleManager<RyanRole>>()
                    .AddScoped<SignInManager<RyanUser>>();

            services.AddScoped<IClaimsTransformation, ClaimsTransformationProvider>();

            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 8;
                options.Password.RequiredUniqueChars = 1;

                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(25);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;

                options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@";
                options.User.RequireUniqueEmail = true;
            });

            services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(13);

                options.LoginPath = "/signin.html";
                options.AccessDeniedPath = "/accessdenied.html";
                options.SlidingExpiration = true;

                options.Cookie.Name = ".MaleFashion.eCommerce.Cookie.Analyisers";

                options.Cookie.SameSite = SameSiteMode.Strict;
            });

            services.AddAuthentication();

            services.AddAuthorization(options =>
            {
                string[] principals = services.GetClaims(typeof(Program));

                foreach (string principal in principals)
                {
                    options.AddPolicy(principal, cfg =>
                    {
                        cfg.RequireClaim(principal, "1");
                    });
                }
            });

            return services;
        }

        public static IApplicationBuilder UseFluentIdentity(this IApplicationBuilder app)
        {
            app.UseAuthentication();
            app.UseAuthorization();

            return app;
        }
    }
}
