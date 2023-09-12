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

}