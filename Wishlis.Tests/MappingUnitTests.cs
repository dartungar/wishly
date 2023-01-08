using AutoMapper;
using Wishlis.Application.Mappings;

namespace Wishlis.Tests;

public class MappingUnitTests
{
    [Fact]
    public void MappingUnitTest()
    {
        var config = new MapperConfiguration(
            cfg => cfg.AddProfile<DefaultMappingProfile>());
        config.AssertConfigurationIsValid();
    }
}