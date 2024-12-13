using Wishlis.Application.DTO;
using Wishlis.Application.Interfaces;
using Wishlis.Application.Mappers;
using Wishlis.Domain.Interfaces;

namespace Wishlis.Application.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly ISearchCache _searchCache;

    public UserService(IUserRepository userRepository, ISearchCache searchCache)
    {
        _userRepository = userRepository;
        _searchCache = searchCache;
    }

    public async Task<UserDto> CreateUser(UserDto model)
    {
        var createdUserId = await _userRepository.Create(model.ToUser());
        var createdUser = await _userRepository.GetById(createdUserId);
        return createdUser.ToUserDto();
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
    
    public async Task RemoveUserFromFavorites(Guid favoriteUserId, Guid ownerId)
    {
        await _userRepository.RemoveUserFromFavorites(favoriteUserId, ownerId);
    }

    public async Task<IEnumerable<UserDto>> SearchUsers(string query)
    {
        if (string.IsNullOrWhiteSpace(query))
            return new List<UserDto>();
        
        var results = await _searchCache.SearchUsers(query);
        if (!results.Any())
            return new List<UserDto>();

        return results.Select(x => x.ToUserDto());
    }
}