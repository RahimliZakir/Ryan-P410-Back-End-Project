using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using RyanP410.WebUI.AppCode.DataSeeds;
using RyanP410.WebUI.AppCode.Extensions;
using RyanP410.WebUI.AppCode.ModelBinders;
using RyanP410.WebUI.AppCode.Providers;
using RyanP410.WebUI.Models.DataContexts;
using RyanP410.WebUI.Models.Entities.Membership;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

IConfiguration conf = builder.Configuration;

IServiceCollection services = builder.Services;

services.AddRouting(options =>
{
    options.LowercaseUrls = true;
});

services.AddControllersWithViews(cfg =>
{
    cfg.ModelBinderProviders.Insert(0, new BooleanBinderProvider());

    AuthorizationPolicy builder = new AuthorizationPolicyBuilder()
                                      .RequireAuthenticatedUser()
                                      .Build();

    cfg.Filters.Add(new AuthorizeFilter(builder));
})
.AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore); ; ;

services.AddDbContext<RyanDbContext>(options =>
{
    options.UseSqlServer(conf.GetConnectionString("cString"));
});

services.AddMediatR(typeof(Program));

services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();

services.AddFluentIdentity();

WebApplication app = builder.Build();
IWebHostEnvironment env = builder.Environment;

if (env.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseStaticFiles();

app.SeedData().Wait();

app.UseRouting();

app.UseFluentIdentity();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(name: "SignInRoute",
         pattern: "signin.html",
         defaults: new
         {
             controller = "Account",
             action = "SignIn"
         });

    endpoints.MapControllerRoute(name: "RegisterRoute",
        pattern: "register.html",
        defaults: new
        {
            controller = "Account",
            action = "Register"
        });

    endpoints.MapControllerRoute(name: "AccessDeniedRoute",
        pattern: "accessdenied.html",
        defaults: new
        {
            controller = "Account",
            action = "AccessDenied"
        });

    endpoints.MapControllerRoute(name: "areas", pattern: "{area:exists}/{controller=Persons}/{action=Index}/{id?}");

    endpoints.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}");
});

app.Run();
