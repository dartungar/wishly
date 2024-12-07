using Wishlis.Domain.Entities;

namespace Wishlis.Domain.Interfaces;

public interface IUserRepository
{
    public Task<Guid> Create(User user);
    
    public Task Update(User user);

    public Task<User> GetById(Guid id);

    public Task<IEnumerable<User>> GetFavoriteUsers(Guid userId);
    
    public Task AddUserToFavorites(Guid favoriteUserId, Guid ownerUserId);
    
    public Task RemoveUserFromFavorites(Guid favoriteUserId, Guid ownerUserId);


    Task Delete(Guid id);
}