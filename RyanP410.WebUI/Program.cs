WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

IConfiguration conf = builder.Configuration;

IServiceCollection services = builder.Services;

services.AddControllersWithViews();

WebApplication app = builder.Build();
IWebHostEnvironment env = builder.Environment;

if (env.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}");
});

app.Run();
