namespace Wishlis.Infrastructure.Logging;
using NLog;

public static class WishlisLogger
{
    private const string LOGGER_NAME = "WishlisLogger";
    
    public static Logger GetLogger()
    {
        Setup();
        return LogManager.GetLogger(LOGGER_NAME);
    }
    private static void Setup()
    {
        LogManager.Setup().LoadConfiguration(builder =>
        {
            builder.ForLogger().FilterMinLevel(LogLevel.Debug).WriteToConsole();
            builder.ForLogger().FilterMinLevel(LogLevel.Warn).WriteToFile("${basedir}/${level}/${date}.log");
        });
    }
}