namespace Wishlis.Domain.Repositories;

public interface IWishlistItemRepository : IEntityRepository<WishlistItem>
{
    public Task<IEnumerable<WishlistItem>> GetByUserId(int userId);
}