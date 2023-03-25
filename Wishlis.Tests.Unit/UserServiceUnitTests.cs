using Common.DTO;
using Common.Exceptions;
using Wishlis.Application.Users;
using Wishlis.Domain;
using Wishlis.Tests.Fixtures;

namespace Wishlis.Tests.Unit;

// TODO: create tests after service goes beyond dumb CRUD
public class UserServiceUnitTests : IClassFixture<UserServiceFixture>
{
    private UserService _service;
    public UserServiceUnitTests(UserServiceFixture fixture)
    {
        _service = fixture.UserService;
    }
    
    
    [Theory]
    [InlineData(1899, 1, 1, false)]
    [InlineData(1990, 1, 1, true)]
    public async void Validates_DateOfBirth(int year, int month, int day, bool isValid)
    {
        var dto = new UserDto(1, "TestName", "testpublicid", new DateTime(year, month, day));
        var sut = _service;

        if (isValid)
        {
            await sut.Invoking(x => x.Insert(dto)).Should().NotThrowAsync<WishlisLogicException>();
            await sut.Invoking(x => x.Update(dto)).Should().NotThrowAsync<WishlisLogicException>();
        }
        else
        {
            await sut.Invoking(x => x.Insert(dto)).Should().ThrowAsync<WishlisLogicException>();
            await sut.Invoking(x => x.Update(dto)).Should().ThrowAsync<WishlisLogicException>();
        }
        
    }
}