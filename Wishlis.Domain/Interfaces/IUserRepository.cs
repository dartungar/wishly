using Wishlis.Domain.Entities;

namespace Wishlis.Domain.Repositories;

public interface IUserRepository
{
    public Task<Guid> Create(User user);
    
    public Task Update(User user);

    public Task<User> GetById(Guid id);

    public Task AddUserToFavorites(Guid favoriteUserId, Guid ownerUserId);
    
    public Task<IEnumerable<User>> GetFavoriteUsers(Guid userId);
    
    Task Delete(Guid id);
}