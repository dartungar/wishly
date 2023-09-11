
using AutoMapper;
using Common.DTO;
using Wishlis.Infrastructure;
using Wishlis.Infrastructure.Repositories;
using Wishlis.Services.WishlistItems;

namespace Wishlis.Tests.Integration;

public class WishlistItemServiceIntegrationTests
{
    
    [Fact]
    public async Task WishlistItemService_Adds_WishlistItem_To_Db()
    {
        var dto = WishlistItemServiceFixture.GetWishlistItemDto();
        var sut = CreateService();
        
        var insertedId = await sut.Insert(dto);
        var insertedWishlistItems = await sut.Get(insertedId);

        insertedWishlistItems.Should().NotBeNull();
        insertedWishlistItems.Should().BeOfType<WishlistItemDto>();
        await sut.Delete(dto.Id);
    }

    private WishlistItemService CreateService()
    {
        var dbFixture = new DbFixture();
        var mapper = MappingFixtures.GetMapper();
        var repo = new WishlistItemRepository(dbFixture.DbOptions);
        return new WishlistItemService(repo, mapper);
    }
    
}