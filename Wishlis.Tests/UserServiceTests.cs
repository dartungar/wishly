using FluentAssertions;
using Wishlis.Domain;
using Wishlis.Tests.Fixtures;

namespace Wishlis.Tests;

public class UserServiceTests
{
    [Fact]
    public async void UserServiceInsert_Inserts_User()
    {
        var userService = UserServiceFixtures.UserService();
        var dto = UserServiceFixtures.GetUserDto();
        await userService.Object.Insert(dto);
        var insertedUser = await userService.Object.Search(dto.PublicId);
        insertedUser.Should().NotBeNull();
    }

    [Fact]
    public async void UserServiceGet_Returns_User()
    {
        await DbFixtures.InsertTestUsers();
        var userService = UserServiceFixtures.UserService();
        var result = await userService.Object.Get();
        result.ToArray().Should().BeOfType<User[]>();
        result.ToArray().Should().HaveCount(c => c > 0);
        await DbFixtures.DeleteTestUsers();
    }
    
    [Fact]
    public async void UserServiceDelete_InsertsUser()
    {
        await DbFixtures.DeleteTestUsers();
        var user = new User(543, "TEST_DANIEL", "555-444", new DateTime(1991, 7, 1));
        var userService = UserServiceFixtures.UserService();
        await userService.Object.Insert(user);
        var insertedUser = await userService.Object.Search(user.Name);
        insertedUser.Should().NotBeEmpty();
        insertedUser.Should().HaveCount(1);
    }

    [Fact]
    public async void UserServiceDelete_DeletesUser()
    {
        await DbFixtures.DeleteTestUsers();
        var user = new User(543, "TEST_DANIEL", "555-444", new DateTime(1991, 7, 1));
        var userService = UserServiceFixtures.UserService();
        await userService.Object.Insert(user);
        var insertedUser = await userService.Object.Search(user.Name);
        await userService.Object.Delete(insertedUser.First().Id);
        var deletedUser = await userService.Object.Search(user.Name);
        deletedUser.Should().BeEmpty();
    }

}