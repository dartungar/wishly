using Asp.Versioning.ApiExplorer;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Wishlis.Api.Auth;
using Wishlis.Api.Swagger;
using Wishlis.Application.Interfaces;
using Wishlis.Application.Mappers;
using Wishlis.Application.Services;
using Wishlis.Domain.Repositories;
using Wishlis.Infrastructure.LiteDB;

namespace Wishlis.Api;

public class Startup
{
    private readonly IWebHostEnvironment _env;
    public IConfiguration Configuration { get; }

    public Startup(IConfiguration configuration, IWebHostEnvironment env)
    {
        _env = env;
        Configuration = configuration;
    }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddPolicy("AllowSpecificOrigins",
                builder => builder.WithOrigins("http://localhost:4200")
                    .AllowAnyHeader()
                    .AllowAnyMethod());
        });
        
        services.AddControllers();
        services.AddAndConfigureSwagger();
        
        // auth
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer();
        services.AddAuthorization();
        services.ConfigureOptions<JwtBearerConfigureOptions>();

        // services
        services.AddAutoMapper(typeof(MappingProfile));
        services.AddTransient<IUserService, UserService>();
        services.AddTransient<IWishlistItemService, WishlistItemService>();

        // infrastructure
        var n = Configuration.GetChildren().ToList();
        services.Configure<LiteDbOptions>(Configuration.GetSection(nameof(LiteDbOptions)));
        services.AddScoped<ILiteDbContext, LiteDbContext>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IWishlistItemRepository, WishlistItemRepository>();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.UseCors("AllowSpecificOrigins");
        
        if (env.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                var apiVersionDescriptionProvider = app.ApplicationServices.GetRequiredService<IApiVersionDescriptionProvider>();
        
                foreach (var description in apiVersionDescriptionProvider.ApiVersionDescriptions)
                {
                    options.SwaggerEndpoint(
                        $"/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant()); 
                } 
            });
        }

        app.UseHttpsRedirection();
        app.UseRouting();
        app.UseEndpoints(x => x.MapControllers());
    }
}