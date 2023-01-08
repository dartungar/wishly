using Dapper;
using Microsoft.Extensions.Options;
using Wishlis.Domain;
using Wishlis.Domain.Repositories;

namespace Wishlis.Infrastructure.Repositories;

public class UserRepository : BaseRepository<User>, ISearchableEntityRepository<User>
{
    public UserRepository(IOptions<DbOptions> options) : base(options)
    {
    }

    public async Task<User> GetUserByExternalId(string externalId)
    {
        return await Connection.QueryFirstAsync<User>(@$"
            SELECT u.* FROM {nameof(User)} u 
            JOIN {nameof(UserExternalId)} ueid 
                       ON u.{nameof(User.Id)} = ueid.{nameof(UserExternalId.ExternalId)}");
    }

    // TODO: check if LIKE works
    public async Task<IEnumerable<User>> FindAsync(string searchQuery)
    {
        return await Connection.QueryAsync<User>(@$"
            SELECT * FROM {nameof(User)} 
                     WHERE {nameof(User.Name)} LIKE {searchQuery}
            UNION
            SELECT * FROM {nameof(User)} 
                     WHERE {nameof(User.PublicId)} LIKE {searchQuery}");
    }
}