using Asp.Versioning;
using Asp.Versioning.Conventions;

namespace Wishlis.Api.Swagger;

public static class SwaggerHelper
{
    public static void AddAndConfigureSwagger(this IServiceCollection services)
    {
        services.AddApiVersioning(options =>
            {
                options.DefaultApiVersion = new ApiVersion(1);
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.ApiVersionReader = new UrlSegmentApiVersionReader();
            }).AddMvc(
                options =>
                {
                    options.Conventions.Add(new VersionByNamespaceConvention());
                })
            .AddApiExplorer(setup =>
            {
                setup.GroupNameFormat = "'v'VVV";
                setup.SubstituteApiVersionInUrl = true;
            });

        services.AddSwaggerGen();
        services.ConfigureOptions<NamedSwaggerGenOptions>();
    }
}