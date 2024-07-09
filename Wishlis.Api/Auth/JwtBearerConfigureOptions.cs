using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;

namespace Wishlis.Api.Auth;

public class JwtBearerConfigureOptions : IConfigureNamedOptions<JwtBearerOptions>
{
    private const string ConfigurationSectionName = "JwtBearer";
    private readonly IConfiguration _configuration;

    public JwtBearerConfigureOptions(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void Configure(JwtBearerOptions options)
    {
        _configuration.GetSection(ConfigurationSectionName).Bind(options);
    }

    public void Configure(string? name, JwtBearerOptions options)
    {
        Configure(options);
    }
}