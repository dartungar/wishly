using Wishlis.Domain;
using Dapper;
using Microsoft.Extensions.Options;
using Wishlis.Domain.Repositories;
using Wishlis.Infrastructure.Repositories;

namespace Wishlis.Infrastructure;

public class WishlistItemRepository : BaseRepository<WishlistItem>, IWishlistItemRepository
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