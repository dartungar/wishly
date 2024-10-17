using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Wishlis.Application.DTO;
using Wishlis.Application.Interfaces;
using Wishlis.Application.Services;

namespace Wishlis.Api.Controllers;

[ApiVersion(1)]
[Route("/api/v{version:apiVersion}/[controller]")]
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

    /// <summary>
    /// Create a new wishlist item.
    /// </summary>
    /// <param name="model">Wishlist item's data.</param>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> Create(WishlistItemDto model)
    {
        await _wishlistItemService.Create(model);
        return Created();
    }
    
    /// <summary>
    /// Create or update an existing wishlist item.
    /// </summary>
    /// <param name="id">Item ID.</param>
    /// <param name="model">Item's updated data.</param>
    /// <returns></returns>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> Save(Guid id, WishlistItemDto model)
    {
        if (id != model.Id)
            return BadRequest();
        await _wishlistItemService.Save(model);
        return Ok();
    }
    
    /// <summary>
    /// Delete a wishlist item.
    /// </summary>
    /// <param name="id">Item's ID.</param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _wishlistItemService.Delete(id);
        return Ok();
    }
}