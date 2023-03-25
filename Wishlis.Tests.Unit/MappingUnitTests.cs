using AutoMapper;
using Common.DTO;
using Common.Mappings;
using Wishlis.Domain;
using Wishlis.Tests.Fixtures;
using Xunit;

namespace Wishlis.Tests.Unit;

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
        var mapper = GetMapper();
        var userDto = UserServiceFixture.GetUserDto();
        var user = mapper.Map<User>(userDto);
        user.Should().NotBeNull();
        user.Name.Should().NotBeNull();
    }
    
    [Fact]
    public void Map_UserToUserDto()
    {
        var mapper = GetMapper();
        var user = UserServiceFixture.GetUser();
        var userDto = mapper.Map<UserDto>(user);
        userDto.Should().NotBeNull();
        userDto.Name.Should().NotBeNull();
        userDto.Id.Should().BeOfType(typeof(int));
        userDto.DateOfBirth.Should().Be(user.DateOfBirth);
    }

    private Mapper GetMapper()
    {
        var config = new MapperConfiguration(
            cfg =>
            {
                cfg.AddProfile<DefaultMappingProfile>();
            });
        return new Mapper(config);
    }
}