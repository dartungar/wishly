using Wishlis.Application.DTO;

namespace Wishlis.Application.Interfaces;

public interface IUserService
{
    Task<int> CreateUser(UserDto model);
    Task DeleteUser(int id);
    Task UpdateUser(UserDto model);
    Task<UserDto> GetById(int id);
    Task<IEnumerable<UserDto>> GetFavoriteUsers(int ownerId);
    Task AddUserToFavorites(int favoriteUserId, int ownerId);
}