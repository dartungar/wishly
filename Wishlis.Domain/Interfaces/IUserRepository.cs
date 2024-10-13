using Wishlis.Domain.Entities;

namespace Wishlis.Domain.Repositories;

public interface IUserRepository
{
    public Task<int> Create(User user);
    
    public Task Update(User user);

    public Task<User> GetById(int id);

    public Task AddUserToFavorites(int favoriteUserId, int ownerUserId);
    
    public Task<IEnumerable<User>> GetFavoriteUsers(int userId);
    
    Task Delete(int id);
}