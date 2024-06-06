using Microsoft.Extensions.Configuration;

namespace Wishlis.MessageConsumer;

public static class Configuration
{
    public static IConfiguration Create()
    {
        var builder = new ConfigurationBuilder();
        builder.SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
 
        return builder.Build();
    }
}