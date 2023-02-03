using Common.DTO;
using Microsoft.Extensions.Options;
using Moq;
using Wishlis.Application.Services;
using Wishlis.Domain;
using Wishlis.Infrastructure;
using Wishlis.Infrastructure.Repositories;

namespace Wishlis.Tests.Fixtures;

public static class UserServiceFixtures
{
    public static Mock<UserService> UserService()
    {
        var dbOptions = new DbOptions()
            { ConnectionString = "Server=127.0.0.1;Port=5432;Database=wishlis;User Id=admin;Password=admin;" };
        var mockOptions = new Mock<IOptions<DbOptions>>();
        mockOptions.Setup(o => o.Value).Returns(dbOptions);
        var mapper = MappingFixtures.GetMapper();
        var repo = new UserRepository(mockOptions.Object);
        return new Mock<UserService>(repo, mapper);
    }


    
    public static UserDto GetUserDto()
    {
        return new UserDto(
            Id: 9999,
            Name: "TestUser",
            PublicId: "test_test_test",
            DateOfBirth: new DateTime(1991, 11, 7));
    }
    
    public static User GetUser()
    {
        return new User(
            id: 9999,
            name: "Danila Nikolaev",
            publicId: "555-abc123",
            dateOfBirth: new DateTime(1991, 11, 7)
        );
    }

    public static List<User> GetUsers()
    {
        return new List<User>()
        {
            new User(
                id: 1,
                name: "Test1",
                publicId: "555-abc123",
                dateOfBirth: new DateTime(1991, 11, 7)
            ),
            new User(
                id: 2,
                name: "Test2",
                publicId: "ololo-345",
                dateOfBirth: new DateTime(965, 3, 11)
            ),
            new User(
                id: 3,
                name: "Test3",
                publicId: "954-234",
                dateOfBirth: new DateTime(1894, 6, 25)
            )
        };
    }
}