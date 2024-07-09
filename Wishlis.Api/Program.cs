using Asp.Versioning;
using Asp.Versioning.ApiExplorer;
using Asp.Versioning.Conventions;
using Microsoft.AspNetCore.OData;
using Microsoft.OData.ModelBuilder;
using Microsoft.OpenApi.Models;
using Wishlis.Api.Swagger;
using Wishlis.Application.Mappers;
using Wishlis.Application.Services;
using Wishlis.Domain.Entities;
using Wishlis.Domain.Repositories;
using Wishlis.Infrastructure.LiteDB;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddControllers();
builder.Services.AddAndConfigureSwagger();

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

app.UseHttpsRedirection();
app.UseRouting();
app.UseEndpoints(x => x.MapControllers());
app.Run();