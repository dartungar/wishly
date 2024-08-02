using Asp.Versioning.ApiExplorer;
using Wishlis.Api.Swagger;
using Wishlis.Application.Mappers;
using Wishlis.Application.Services;
using Wishlis.Domain.Repositories;
using Wishlis.Infrastructure.LiteDB;

namespace Wishlis.Api;

public class Startup
{
    public IConfiguration Configuration { get; }

    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
        // Add services to the container.
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        services.AddControllers();
        services.AddAndConfigureSwagger();

        // services
        services.AddAutoMapper(typeof(MappingProfile));
        services.AddTransient<IPersonService, PersonService>();
        services.AddTransient<IWishlistItemService, WishlistItemService>();

        // infrastructure
        var n = Configuration.GetChildren().ToList();
        services.Configure<LiteDbOptions>(Configuration.GetSection(nameof(LiteDbOptions)));
        services.AddScoped<ILiteDbContext, LiteDbContext>();
        services.AddScoped<IPersonRepository, PersonRepository>();
        services.AddScoped<IWishlistItemRepository, WishlistItemRepository>();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
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