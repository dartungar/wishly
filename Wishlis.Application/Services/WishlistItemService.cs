using AutoMapper;
using Common.DTO;
using Wishlis.Domain;
using Wishlis.Domain.Repositories;

namespace Wishlis.Application.Services;

public class WishlistItemService : BaseService<WishlistItem, WishlistItemDto>
{
    public WishlistItemService(IWishlistItemRepository repo, IMapper mapper) : base(repo, mapper)
    {
    }
}