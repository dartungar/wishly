using Common.DTO;
using Wishlis.Application.Users;
using Wishlis.Domain;
using Wishlis.Infrastructure.Repositories;

namespace Wishlis.Tests.Integration;

public class UserServiceIntegrationTests : IDisposable
{
    public UserServiceIntegrationTests()
    {
        DeleteTestUser().Wait();
    }
    
    [Fact]
    public async void Adds_User_To_Db()
    {
        var dto = UserServiceFixture.GetUserDto();
        var sut = CreateUserService();
        
        await sut.Insert(dto);
        var insertedUsers = await sut.Search(dto.PublicId);
        
        insertedUsers.Should().NotBeEmpty();
        insertedUsers.FirstOrDefault().Should().BeOfType<UserDto>();
        await sut.Delete(dto.Id);
    }

    [Fact]
    public async void Updates_User_In_Db()
    {
        var dto = UserServiceFixture.GetUserDto();
        var sut = CreateUserService();
        
        await sut.Insert(dto);
        var insertedUsers = await sut.Search(dto.PublicId);
        
        var changedDto = new UserDto(insertedUsers.First().Id, "CHANGED NAME", "test-test1", new DateTime(2000, 10, 10));
        await sut.Update(changedDto);
        
        var updatedUsers = await sut.Search(dto.PublicId);

        var updatedUser = updatedUsers.FirstOrDefault();
        
        updatedUser.Should().NotBeNull();
        updatedUser.Should().BeOfType<UserDto>();
        updatedUser.Id.Should().Be(changedDto.Id);
        updatedUser.Name.Should().Be(changedDto.Name);
        updatedUser.PublicId.Should().Be(changedDto.PublicId);
        updatedUser.DateOfBirth.Should().Be(changedDto.DateOfBirth);
        
        await sut.Delete(dto.Id);
    }

    [Fact]
    public async void Deletes_User_From_Db()
    {
        var user = new User(543, "TEST_DANIEL_OMG2", "555-444", new DateTime(1991, 7, 1));
        var sut = CreateUserService();
        await sut.Insert(user);
        var insertedUser = await sut.Search(user.Name);
        
        await sut.Delete(insertedUser.First().Id);
        var deletedUser = await sut.Search(user.Name);
        
        deletedUser.Should().BeEmpty();
    }

    private UserService CreateUserService()
    {
        var dbFixture = new DbFixture();
        var mapper = MappingFixtures.GetMapper();
        var repo = new UserRepository(dbFixture.DbOptions);
        return new UserService(repo, mapper);
    }
    
    private async Task DeleteTestUser()
    {
        var service = CreateUserService();
        var test_user = service.Search(UserServiceFixture.GetUserDto().PublicId).Result.FirstOrDefault();
        if (test_user != null)
        {
            await service.Delete(test_user.Id);
        }
    }

    public async void Dispose()
    {
        await DeleteTestUser();
    }
}

