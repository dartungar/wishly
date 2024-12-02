using Wishlis.Application.DTO;
using Wishlis.Application.Interfaces;
using Wishlis.Application.Mappers;
using Wishlis.Domain.Repositories;

namespace Wishlis.Application.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<Guid> CreateUser(UserDto model)
    {
        return await _userRepository.Create(model.ToUser());
    }

    public async Task DeleteUser(Guid id)
    {
        await _userRepository.Delete(id);
    }

    public async Task UpdateUser(UserDto model)
    {
        await _userRepository.Update(model.ToUser());
    }

    public async Task<UserDto?> GetById(Guid id)
    {
        var user = await _userRepository.GetById(id);
        return user?.ToUserDto();
    }

    public async Task<IEnumerable<UserDto>> GetFavoriteUsers(Guid ownerId)
    {
        var favoriteUsers = await _userRepository.GetFavoriteUsers(ownerId);
        return favoriteUsers.Select(u => u.ToUserDto()).ToList();
    }

    public async Task AddUserToFavorites(Guid favoriteUserId, Guid ownerId)
    {
        await _userRepository.AddUserToFavorites(favoriteUserId, ownerId);
    }
}