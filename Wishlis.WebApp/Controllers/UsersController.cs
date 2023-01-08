using Microsoft.AspNetCore.Mvc;
using Wishlis.Application.DTO;
using Wishlis.Application.Services;
using Wishlis.Domain;

namespace Wishlis.WebApp.Controllers;

[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{
    private readonly ILogger<UsersController> _logger;
    private readonly BaseService<User, UserDto> _service;

    public UsersController(ILogger<UsersController> logger, BaseService<User, UserDto> service)
    {
        _logger = logger;
        _service = service;
    }

    [HttpGet]
    public async Task<IEnumerable<UserDto>> Get()
    {
        return await _service.Get();
    }
}