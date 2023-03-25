using System.Reflection;
using Common.Mappings;
using Microsoft.OpenApi.Models;
using Serilog;
using Wishlis.Application.Users;
using Wishlis.Application.WishlistItems;
using Wishlis.Domain.Repositories;
using Wishlis.Infrastructure;
using Wishlis.Infrastructure.Db;
using Wishlis.Infrastructure.Repositories;

var CORS_POLICY_NAME = "WishlisCorsPolicy";

Log.Logger = new LoggerConfiguration()
    .WriteTo.Debug()
    .WriteTo.File("logs.txt")
    .CreateLogger();

try
{
    Log.Information("Starting WishLis WebApp...");
    var builder = WebApplication.CreateBuilder(args);

    // Add services to the container.

    builder.Services.AddCors();
    // builder.Services.AddCors(options => options.AddPolicy(CORS_POLICY_NAME, b =>
    // {
    //     b.WithOrigins("http://localhost:44440").AllowAnyMethod().AllowAnyHeader();
    // }));
    
    
    builder.Logging.ClearProviders();
    builder.Logging.AddSerilog();

    builder.Services.AddAutoMapper(Assembly.GetAssembly(typeof(DefaultMappingProfile)));

    builder.Services.Configure<DbOptions>(builder.Configuration.GetSection(DbOptions.SectionName));

    // application services
    builder.Services.AddTransient<UserService>();
    builder.Services.AddTransient<WishlistItemService>();
    
    // application repositories
    builder.Services.AddScoped<IUserRepository, UserRepository>();
    builder.Services.AddScoped<IWishlistItemRepository, WishlistItemRepository>();

    builder.Services.AddSingleton<DbInitializer>();
    
    builder.Services.AddControllersWithViews();
    
    builder.Services.AddSwaggerGen(options =>
    {
        options.SwaggerDoc("v1", new OpenApiInfo
        {
            Version = "v1",
            Title = "WishLis API",
        });
    });

    
    
    // INITIALIZE
    
    var app = builder.Build();

    var dbInitializer = app.Services.GetRequiredService<DbInitializer>();
    dbInitializer.InitDb();

    // Configure the HTTP request pipeline.
    if (!app.Environment.IsDevelopment())
    {
        app.UseExceptionHandler("/error");
    
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
        
        // swagger
        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
            options.RoutePrefix = string.Empty;
        });
    }

    app.UseHttpsRedirection();
    app.UseStaticFiles();
    app.UseRouting();

    // app.UseCors(CORS_POLICY_NAME);
    app.UseCors(b => b
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
    
    app.MapControllerRoute(
        name: "default",
        pattern: "{controller}/{action=Index}/{id?}");

    app.MapFallbackToFile("index.html");

    app.Run();
}
catch(Exception e)
{
    Log.Fatal(e, "Stopped application because of exception");
}
finally
{
    Log.CloseAndFlush();
}

