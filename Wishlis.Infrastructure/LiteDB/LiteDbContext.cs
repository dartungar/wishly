using LiteDB;
using LiteDB.Async;
using Microsoft.Extensions.Options;

namespace Wishlis.Infrastructure.LiteDB;

public interface ILiteDbContext
{
    public LiteDatabaseAsync Database { get; }
}

public class LiteDbContext : ILiteDbContext
{
    public LiteDatabaseAsync Database { get; }
    
    public LiteDbContext(IOptions<LiteDbOptions> options)
    {
        Database = new LiteDatabaseAsync($"filename={options.Value.DatabaseLocation};connection=shared");
    }
}

