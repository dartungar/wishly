using AutoMapper;
using Common.DTO;
using Common.Mappings;
using Wishlis.Infrastructure;
using Wishlis.Infrastructure.Repositories;
using Wishlis.Services.WishlistItems;

namespace Wishlis.Tests.Fixtures;

public class WishlistItemServiceFixture
{
    private WishlistItemService WishlistItemService { get; }
    public WishlistItemServiceFixture()
    {
        var dbFixture = new DbFixture();
        var mapper = MappingFixtures.GetMapper();
        //var repo = new WishlistItemRepository(dbFixture.DbOptions);
        //WishlistItemService = new WishlistItemService(repo, mapper);
    }
    
    public static WishlistItemDto GetWishlistItemDto()
    {
        return new WishlistItemDto(9999, 1, "Test Item", 500, 643, "https://google.com", false);
    }
}