namespace Wishlis.Infrastructure;

public class DbOptions
{
    public static string SectionName = "DbOptions";
    public string ConnectionString { get; set; } = string.Empty;
}