using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Wishlis.Application.DTO;
using Wishlis.Application.Services;

namespace Wishlis.Api.Controllers;

[ApiVersion(2)]
[Route("v{version:apiVersion}/wishlist-items")]
[ApiController]
public class WishlistItemsODataController : ODataController
{
    private readonly IWishlistItemService _wishlistItemService;

    public WishlistItemsODataController(IWishlistItemService wishlistItemService)
    {
        _wishlistItemService = wishlistItemService;
    }

    [EnableQuery]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<WishlistItemDto>>> Get()
    {
        var items = await _wishlistItemService.Get();
        return Ok(items);
    }
}