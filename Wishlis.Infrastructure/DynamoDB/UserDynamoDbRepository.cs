using Amazon.DynamoDBv2.DataModel;
using Wishlis.Domain.Entities;
using Wishlis.Domain.Interfaces;

namespace Wishlis.Infrastructure.DynamoDB;

public class UserDynamoDbRepository : IUserRepository
{
    private readonly IDynamoDBContext _context;

    public UserDynamoDbRepository(IDynamoDBContext context)
    {
        _context = context;
    }
    
    public async Task<Guid> Create(User user)
    {
        await _context.SaveAsync(new UserDynamoDbModel(user));
        return user.Id;
    }

    public async Task Update(User user)
    {
        await _context.SaveAsync(new UserDynamoDbModel(user));
    }

    public async Task<User?> GetById(Guid id)
    {
        var ddbModel = await _context.LoadAsync<UserDynamoDbModel>(id.ToString());
        return ddbModel?.ToDomainModel();
    }

    public async Task<IEnumerable<User>> GetFavoriteUsers(Guid userId)
    {
        var ddbModel = await _context.LoadAsync<UserDynamoDbModel>(userId.ToString());
        if (!ddbModel.FavoriteUserIds.Any())
            return new List<User>();
        
        var getUsersRequest = _context.CreateBatchGet<UserDynamoDbModel>();
        ddbModel.FavoriteUserIds.ForEach(getUsersRequest.AddKey);
        await getUsersRequest.ExecuteAsync();
        return getUsersRequest.Results.Select(x => x.ToDomainModel());
    }

    public async Task AddUserToFavorites(Guid favoriteUserId, Guid ownerUserId)
    {
        var ddbModel = await _context.LoadAsync<UserDynamoDbModel>(ownerUserId.ToString());
        if (!ddbModel.FavoriteUserIds.Contains(favoriteUserId.ToString()))
        {
            ddbModel.FavoriteUserIds.Add(favoriteUserId.ToString());
            await _context.SaveAsync(ddbModel);
        }
    }

    public async Task RemoveUserFromFavorites(Guid favoriteUserId, Guid ownerUserId)
    {
        var ddbModel = await _context.LoadAsync<UserDynamoDbModel>(ownerUserId.ToString());
        if (ddbModel.FavoriteUserIds.Contains(favoriteUserId.ToString()))
        {
            ddbModel.FavoriteUserIds.Remove(favoriteUserId.ToString());
            await _context.SaveAsync(ddbModel);
        }
    }

    public async Task Delete(Guid id)
    {
        await _context.DeleteAsync<UserDynamoDbModel>(id.ToString());
    }
}