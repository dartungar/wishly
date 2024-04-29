using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Wishlis.Application.DTO;
using Wishlis.Application.Services;
using Wishlis.Domain.Entities;

namespace Wishlis.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class WishlistItemsController : ControllerBase
{
    private readonly ILogger<WishlistItemsController> _logger;
    private readonly IWishlistItemService _wishlistItemService;

    public WishlistItemsController(ILogger<WishlistItemsController> logger, IWishlistItemService wishlistItemService)
    {
        _logger = logger;
        _wishlistItemService = wishlistItemService;
    }

    [HttpPost]
    public async Task<IActionResult> Create(WishlistItemDto model)
    {
        await _wishlistItemService.Create(model);
        return Created();
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, WishlistItemDto model)
    {
        if (id != model.Id)
            return BadRequest();
        await _wishlistItemService.Update(model);
        return Ok();
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _wishlistItemService.Delete(id);
        return Ok();
    }
}