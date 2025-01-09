using Amazon;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.Extensions.NETCore.Setup;
using Amazon.Runtime;
using Asp.Versioning.ApiExplorer;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Wishlis.Api.Auth;
using Wishlis.Api.Swagger;
using Wishlis.Api.Utils;
using Wishlis.Application.Interfaces;
using Wishlis.Application.Services;
using Wishlis.Domain.Interfaces;
using Wishlis.Infrastructure.DynamoDB;
using Wishlis.Infrastructure.LiteDB;
using Wishlis.Infrastructure.SearchCache;

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
        
        services.AddControllers().AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.Converters.Add(new DateOnlyJsonConverter());
        });;
        services.AddAndConfigureSwagger();
        
        // auth
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer();
        services.AddAuthorization();
        services.ConfigureOptions<JwtBearerConfigureOptions>();

        // services
        services.AddTransient<IUserService, UserService>();
        services.AddTransient<IWishlistItemService, WishlistItemService>();

        // infrastructure
        services.Configure<LiteDbOptions>(Configuration.GetSection(nameof(LiteDbOptions)));
        services.AddScoped<ILiteDbContext, LiteDbContext>();
        services.AddScoped<IUserRepository, UserDynamoDbRepository>();
        services.AddScoped<IWishlistItemRepository, WishlistItemDynamoDbRepository>();
        services.AddSingleton<ISearchCache, InMemorySearchCache>();
        
        // AWS
        services.AddDefaultAWSOptions(new AWSOptions
        {
            Credentials = new BasicAWSCredentials(
                Environment.GetEnvironmentVariable("AWS_ACCESS_KEY_ID") ?? Configuration["AWS:AWS_ACCESS_KEY_ID"],
                Environment.GetEnvironmentVariable("AWS_SECRET_ACCESS_KEY") ?? Configuration["AWS:AWS_SECRET_ACCESS_KEY"]
            ),
            Region = RegionEndpoint.GetBySystemName(Environment.GetEnvironmentVariable("AWS_REGION") ?? Configuration["AWS:AWS_REGION"])  
        });
        services.AddAWSService<IAmazonDynamoDB>();
        services.AddSingleton<IDynamoDBContext, DynamoDBContext>();
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
            
            app.UseDeveloperExceptionPage();
        }

        app.UseHttpsRedirection();
        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();
        app.UseEndpoints(x => x.MapControllers());
    }
}