using Common.DTO;

namespace Wishlis.Tests.Fixtures;

public static class DbFixtures
{
    public static async Task InsertTestUsers()
    {
        var service = UserServiceFixtures.UserService();
        var mapper = MappingFixtures.GetMapper();
        var testUsers = UserServiceFixtures.GetUsers();
        foreach (var u in testUsers)
        {
            var dto = mapper.Map<UserDto>(u);
            await service.Object.Insert(dto);
        }
    }

    public static async Task DeleteTestUsers()
    {
        var service = UserServiceFixtures.UserService();
        var testUsers = await service.Object.Search("Test");
        var testUsersOther = await service.Object.Search("TEST");
        testUsers = testUsers.Concat(testUsersOther);
        foreach (var u in testUsers)
        {
            await service.Object.Delete(u.Id);
        }
    }
}