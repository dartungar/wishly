using Wishlis.Domain;
using Dapper;
using Microsoft.Extensions.Options;

namespace Wishlis.Infrastructure;

public class WishlistItemRepository : BaseRepository<WishlistItem>
{
    public WishlistItemRepository(IOptions<DbOptions> options) : base(options)
    {
    }

    public async Task<IEnumerable<WishlistItem>> GetWishlistItemsByUserId(int userId)
    {
        return await Connection.QueryAsync<WishlistItem>(@$"
                SELECT * FROM {nameof(WishlistItem)} 
                WHERE {nameof(WishlistItem.UserId)} = {userId}");
    }
}