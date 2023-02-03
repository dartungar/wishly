using AutoMapper;
using Common.DTO;
using Microsoft.AspNetCore.Mvc;
using Wishlis.Application.Services;

namespace Wishlis.WebApp.Controllers;

[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{
    private readonly ILogger<UsersController> _logger;
    private readonly UserService _service;
    private readonly IMapper _mapper;

    public UsersController(ILogger<UsersController> logger, UserService service, IMapper mapper)
    {
        _logger = logger;
        _service = service;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IEnumerable<UserDto>> Get()
    {
        var entities = await _service.Get();
        return _mapper.Map<IEnumerable<UserDto>>(entities);
    }
}