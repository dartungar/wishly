using LiteDB;
using LiteDB.Async;
using Wishlis.Domain.Entities;
using Wishlis.Domain.Repositories;

namespace Wishlis.Infrastructure.LiteDB;

public class UserRepository : IUserRepository
{
    private readonly ILiteDbContext _dbContext;
    private ILiteCollectionAsync<User> Users => _dbContext.Database.GetCollection<User>(nameof(User));

    public UserRepository(ILiteDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<int> Create(User user)
    {
        var inserted = await Users.InsertAsync(user);
        return BsonMapper.Global.Deserialize<int>(inserted);
    }

    public Task Update(User user)
    {
        return Users.UpdateAsync(user);
    }

    public Task<User> GetById(int id)
    {
        return Users.FindByIdAsync(id);
    }

    public async Task AddUserToFavorites(int favoriteUserId, int ownerUserId)
    {
        var user = await Users.FindByIdAsync(ownerUserId);
        user.FavoriteUserIds.Add(favoriteUserId);
        await Users.UpdateAsync(user);
    }

    public async Task<IEnumerable<User>> GetFavoriteUsers(int userId)
    {
        var user = await Users.FindByIdAsync(userId);
        return await Users.Query().Where(x => user.FavoriteUserIds.Contains(x.Id)).ToEnumerableAsync();
    }

    public async Task Delete(int id)
    {
        await Users.DeleteAsync(id);
    }
}