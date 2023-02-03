using AutoMapper;
using Common.DTO;
using Common.Mappings;
using FluentAssertions;
using Wishlis.Domain;
using Wishlis.Tests.Fixtures;

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

    [Fact]
    public void Map_UserDtoToUser()
    {
        var mapper = MappingFixtures.GetMapper();
        var userDto = UserServiceFixtures.GetUserDto();
        var user = mapper.Map<User>(userDto);
        user.Should().NotBeNull();
        user.Name.Should().NotBeNull();
    }
    
    [Fact]
    public void Map_UserToUserDto()
    {
        var mapper = MappingFixtures.GetMapper();
        var user = UserServiceFixtures.GetUser();
        var userDto = mapper.Map<UserDto>(user);
        userDto.Should().NotBeNull();
        userDto.Name.Should().NotBeNull();
    }
}