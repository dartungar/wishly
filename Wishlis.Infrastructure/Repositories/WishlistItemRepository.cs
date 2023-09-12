using Microsoft.EntityFrameworkCore;
using Wishlis.Domain;
using Wishlis.Domain.Repositories;

namespace Wishlis.Infrastructure.Repositories;

public class WishlistItemRepository : BaseRepository<WishlistItem>, IWishlistItemRepository
{
    public WishlistItemRepository(WishlistContext context) : base(context)
    {
    }

    public async Task<IEnumerable<WishlistItem>> GetByUserId(int userId)
    {
        return await _context.Items.Where(i => i.UserId == userId).ToListAsync();
    }
}