using AutoMapper;
using Common.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Wishlis.Services.WishlistItems;

namespace Wishlis.WebApp.Controllers;

[ApiController]
[AllowAnonymous]
[Route("api/items")]
public class ItemsController : ControllerBase
{
    private readonly ILogger<ItemsController> _logger;
    private readonly WishlistItemService _service;
    private readonly IMapper _mapper;

    public ItemsController(ILogger<ItemsController> logger, WishlistItemService service, IMapper mapper)
    {
        _logger = logger;
        _service = service;
        _mapper = mapper;
    }

    [HttpGet("")]
    public async Task<ActionResult<IEnumerable<WishlistItemDto>>> Get()
    {
        var newItems = new List<WishlistItemDto>()
        {
            new(0, 1, "Item1", 100.00m, 840, "https://google.com", false),
            new(1, 1, "Item2", 200.00m, 840, "https://yandex.com", false),
            new(2, 2, "Item3", 300.00m, 840, "https://github.com", true),
        };
        var dtos = _mapper.Map<IEnumerable<WishlistItemDto>>(newItems);
        return Ok(dtos);
    }
}