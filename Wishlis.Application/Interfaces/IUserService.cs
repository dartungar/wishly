using Wishlis.Application.DTO;

namespace Wishlis.Application.Interfaces;

public interface IUserService
{
    Task<UserDto> CreateUser(UserDto model);
    Task DeleteUser(Guid id);
    Task UpdateUser(UserDto model);
    Task<UserDto?> GetById(Guid id);
    Task<IEnumerable<UserDto>> GetFavoriteUsers(Guid ownerId);
    Task AddUserToFavorites(Guid favoriteUserId, Guid ownerId);
}