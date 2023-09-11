using Common.DTO;
using Wishlis.Domain;
using Wishlis.Domain.Repositories;
using Wishlis.Services.Users;

namespace Wishlis.Services;

public class AuthService
{
    private readonly IUserRepository _userRepository;

    private readonly UserService _userService;

    //private readonly
    public AuthService(IUserRepository userRepository, UserService userService)
    {
        _userRepository = userRepository;
        _userService = userService;
    }
    
    
    // 
    public async Task<User> GetUserByExternalId(string externalId)
    {
        return await _userRepository.GetByExternalId(externalId);
    }
    
    // TODO: methods for Google, Facebook, Vk, and Telegram
    public async Task<int> CreateWithExternalId(UserDto dto, string externalId)
    {
        var userId = await _userService.Insert(dto);
        await _userRepository.CreateExternalId(userId, externalId);
        return userId;
    }
}