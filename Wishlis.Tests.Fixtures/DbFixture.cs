using Microsoft.Extensions.Options;
using Moq;
using Wishlis.Infrastructure;

namespace Wishlis.Tests.Fixtures;

public class DbFixture
{
    //public IOptions<DbOptions> DbOptions { get; }

    public DbFixture()
    {
        // var dbOptions = new DbOptions()
        //     { ConnectionString = "Server=127.0.0.1;Port=5432;Database=wishlis;User Id=admin;Password=admin;" };
        // var mockOptions = new Mock<IOptions<DbOptions>>();
        // mockOptions.Setup(o => o.Value).Returns(dbOptions);
        // DbOptions = mockOptions.Object;
    }
}