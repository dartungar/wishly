using Asp.Versioning.ApiExplorer;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Wishlis.Api.Auth;
using Wishlis.Api.Swagger;
using Wishlis.Application.Mappers;
using Wishlis.Application.Services;
using Wishlis.Domain.Repositories;
using Wishlis.Infrastructure.LiteDB;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddControllers();
builder.Services.AddAndConfigureSwagger();

// auth
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer();
builder.Services.AddAuthorization();
builder.Services.ConfigureOptions<JwtBearerConfigureOptions>();

// services
builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddTransient<IPersonService, PersonService>();
builder.Services.AddTransient<IWishlistItemService, WishlistItemService>();


// infrastructure
builder.Services.Configure<LiteDbOptions>(builder.Configuration.GetSection(nameof(LiteDbOptions)));
builder.Services.AddScoped<ILiteDbContext, LiteDbContext>();
builder.Services.AddScoped<IPersonRepository, PersonRepository>();
builder.Services.AddScoped<IWishlistItemRepository, WishlistItemRepository>();


var app = builder.Build();

// pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        var apiVersionDescriptionProvider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();
        
        foreach (var description in apiVersionDescriptionProvider.ApiVersionDescriptions)
        {
            options.SwaggerEndpoint(
                $"/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant()); 
        } 
    });
}

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseEndpoints(x => x.MapControllers());

await app.RunAsync();