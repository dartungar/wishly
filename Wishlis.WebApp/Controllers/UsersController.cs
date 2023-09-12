using AutoMapper;
using Common.DTO;
using Microsoft.AspNetCore.Mvc;
using Wishlis.Services.Users;

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
    public async Task<ActionResult<IEnumerable<UserDto>>> Get(int id)
    {
        var entities = await _service.Get(id);
        return Ok(_mapper.Map<IEnumerable<UserDto>>(entities));
    }
}