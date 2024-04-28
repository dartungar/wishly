using Microsoft.AspNetCore.Mvc;
using Wishlis.Application.DTO;
using Wishlis.Application.Services;
using Wishlis.Domain.Entities;

namespace Wishlis.Api.Controllers;

[ApiController]
[Route("items")]
public class WishlistItemController : ControllerBase
{
    private readonly ILogger<WishlistItemController> _logger;
    private readonly IWishlistItemService _wishlistItemService;

    public WishlistItemController(ILogger<WishlistItemController> logger, IWishlistItemService wishlistItemService)
    {
        _logger = logger;
        _wishlistItemService = wishlistItemService;
    }

    [HttpPost]
    public async Task Create([FromBody] WishlistItemDto model)
    {
        await _wishlistItemService.Create(model);
    }
    
    [HttpPut]
    public async Task Update([FromBody] WishlistItemDto model)
    {
        await _wishlistItemService.Update(model);
    }
    
    [HttpDelete]
    public async Task Delete(int id)
    {
        await _wishlistItemService.Delete(id);
    }
}