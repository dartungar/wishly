using Common.DTO;
using Moq;
using Wishlis.Domain;
using Wishlis.Infrastructure.Repositories;
using Wishlis.Services.Users;
using Wishlis.Tests.Fixtures;

namespace Wishlis.Tests.Fixtures;

public class UserServiceFixture : IDisposable
{
    DbFixture _dbFixture;
    public UserService UserService { get; }

    public UserServiceFixture()
    {
        _dbFixture = new DbFixture();
        var mapper = MappingFixtures.GetMapper();
        var repo = new UserRepository(_dbFixture.DbOptions);
        UserService = new UserService(repo, mapper);
    }

    public static UserDto GetUserDto()
    {
        return new UserDto(
            Id: 9999,
            Name: "TestUser",
            PublicId: "test_test",
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

    public void Dispose()
    {
    }
}