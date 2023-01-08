using System.Reflection;
using NLog;
using NLog.Web;
using Wishlis.Application.Mappings;
using Wishlis.Infrastructure;
using Wishlis.Infrastructure.Logging;

var logger = WishlisLogger.GetLogger();
logger.Debug("Starting application...");

try
{
    var builder = WebApplication.CreateBuilder(args);

    // Add services to the container.

    builder.Services.AddControllersWithViews();

    builder.Logging.ClearProviders();
    builder.Host.UseNLog();

    builder.Services.AddAutoMapper(Assembly.GetAssembly(typeof(DefaultMappingProfile)));

    builder.Services.AddOptions<DbOptions>(builder.Configuration.GetSection(DbOptions.SectionName).Value);


    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (!app.Environment.IsDevelopment())
    {
        app.UseExceptionHandler("/error");
    
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
    }

    app.UseHttpsRedirection();
    app.UseStaticFiles();
    app.UseRouting();


    app.MapControllerRoute(
        name: "default",
        pattern: "{controller}/{action=Index}/{id?}");

    app.MapFallbackToFile("index.html");

    app.Run();
}
catch(Exception e)
{
    logger.Error(e, "Stopped application because of exception");
}
finally
{
    LogManager.Shutdown();
}

