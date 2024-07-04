using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Wishlis.Domain.Entities;
using Wishlis.Domain.Services;

namespace Wishlis.Api.Controllers;

[ApiVersion(1)]
[Route("v{version:apiVersion}/[controller]")]
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
    public async Task<IActionResult> Create(WishlistItem model)
    {
        await model.Save();
        return Created();
    }
    
    /// <summary>
    /// Update an existing wishlist item.
    /// </summary>
    /// <param name="id">Item ID.</param>
    /// <param name="model">Item's updated data.</param>
    /// <returns></returns>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> Update(int id, WishlistItem model)
    {
        if (id != model.Id)
            return BadRequest();
        await model.Save();
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