using AutoMapper;
using Common.Mappings;

namespace Wishlis.Tests.Fixtures;

public static class MappingFixtures
{
    public static Mapper GetMapper()
    {
        var config = new MapperConfiguration(
            cfg =>
            {
                cfg.AddProfile<DefaultMappingProfile>();
            });
        return new Mapper(config);
    }
}