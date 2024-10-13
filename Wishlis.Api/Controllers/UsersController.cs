using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Wishlis.Application.DTO;
using Wishlis.Application.Services;
using Wishlis.Domain;
using Microsoft.AspNetCore.Http;
using Wishlis.Application.Interfaces;

namespace Wishlis.Api.Controllers;

/// <summary>
/// 
/// </summary>
[ApiVersion(1)]
[Route("/api/v{version:apiVersion}/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly ILogger<UsersController> _logger;
    private readonly IUserService _userService;
    private readonly IWishlistItemService _wishlistItemService;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="logger"></param>
    /// <param name="userService"></param>
    /// <param name="wishlistItemService"></param>
    public UsersController(ILogger<UsersController> logger, IUserService userService,
        IWishlistItemService wishlistItemService)
    {
        _logger = logger;
        _userService = userService;
        _wishlistItemService = wishlistItemService;
    }

    /// <summary>
    /// Get a user by ID.
    /// </summary>
    /// <param name="id">user's ID.</param>
    /// <returns>A user object.</returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<UserDto>> GetById(Guid id)
    {
        return Ok(await _userService.GetById(id));
    }

    /// <summary>
    /// Create a new user.
    /// </summary>
    /// <param name="model">The user's data.</param>
    /// <returns>Created user's ID.</returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [Authorize]
    public async Task<ActionResult<Guid>> CreateUser(UserDto model)
    {
        return Created("", await _userService.CreateUser(model));
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete]
    [Authorize(Roles = UserGroups.Admins)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> DeleteUser(Guid id)
    {
        await _userService.DeleteUser(id);
        return NoContent();
    }

    /// <summary>
    /// Update an existing user.
    /// </summary>
    /// <param name="model">Updated data.</param>
    /// <returns></returns>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> UpdateUser(UserDto model)
    {
        await _userService.UpdateUser(model);
        return NoContent();
    }

    /// <summary>
    /// Get a list of a user's favorite people.
    /// </summary>
    /// <param name="ownerId">ID of the user.</param>
    /// <returns></returns>
    [HttpGet("{ownerId}/favorite-users")]
    [Authorize]
    public async Task<ActionResult<IEnumerable<UserDto>>> GetFavoriteUsers(Guid ownerId)
    {
        var users = await _userService.GetFavoriteUsers(ownerId);
        return Ok(users);
    }

    /// <summary>
    /// Add a new user as a favorite to another ('owner') user.
    /// </summary>
    /// <param name="favoriteuserId">ID of the user that will be added as a favorite.</param>
    /// <param name="owneruserId">ID of the user, to whom a favorite user will be added.</param>
    /// <returns></returns>
    [HttpPost("{owneruserId}/favorite-users")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> AddUserToFavorites(Guid favoriteuserId, Guid owneruserId)
    {
        await _userService.AddUserToFavorites(favoriteuserId, owneruserId);
        return NoContent();
    }

    /// <summary>
    /// Get a list of user's wishlist items.
    /// </summary>
    /// <param name="userId">ID of the user.</param>
    /// <returns>List of items.</returns>
    [HttpGet("{userId}/items")]
    public async Task<ActionResult<IEnumerable<WishlistItemDto>>> GetuserWishlist(int userId)
    {
        var result = await _wishlistItemService.GetByUserId(userId);
        return Ok(result);
    }
}