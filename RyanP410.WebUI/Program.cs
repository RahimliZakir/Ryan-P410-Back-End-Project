using Microsoft.EntityFrameworkCore;
using RyanP410.WebUI.Models.DataContexts;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

IConfiguration conf = builder.Configuration;

IServiceCollection services = builder.Services;

services.AddControllersWithViews();

services.AddDbContext<RyanDbContext>(options =>
{
    options.UseSqlServer("cString");
});

WebApplication app = builder.Build();
IWebHostEnvironment env = builder.Environment;

if (env.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseStaticFiles();

app.UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(name: "areas", pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

    endpoints.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}");
});

app.Run();
