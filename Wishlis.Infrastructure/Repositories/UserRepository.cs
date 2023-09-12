using Wishlis.Domain;
using Wishlis.Domain.Repositories;

namespace Wishlis.Infrastructure.Repositories;

public class UserRepository : BaseRepository<User>, IUserRepository
{
    public UserRepository(WishlistContext context) : base(context)
    {
    }

    public Task<IEnumerable<User>> FindAsync(string searchQuery)
    {
        throw new NotImplementedException();
    }
}