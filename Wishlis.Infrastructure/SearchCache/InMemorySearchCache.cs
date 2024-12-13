using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using Wishlis.Application.Interfaces;
using Wishlis.Domain.Entities;
using Wishlis.Infrastructure.DynamoDB;

namespace Wishlis.Infrastructure.SearchCache;

public class InMemorySearchCache : ISearchCache
{
    private readonly IDynamoDBContext _context;
    private User[] _cache = [];
    private static readonly int CacheTtlMinutes = 15;
    private  DateTime _cacheExpirationTime = DateTime.UtcNow.AddMinutes(CacheTtlMinutes);

    public InMemorySearchCache(IDynamoDBContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<User>> SearchUsers(string query)
    {
        await EnsureUserCacheIsRelevant();
        return _cache!.Where(x => x.Name.Contains(query, StringComparison.InvariantCultureIgnoreCase));
    }

    private async Task EnsureUserCacheIsRelevant()
    {
        if (_cache.Any() && DateTimeOffset.Now < _cacheExpirationTime)
            return;
        
        // TODO: scan may be inefficient & expensive if we have a lot of users; use GSIs?
        var scanConditions = new List<ScanCondition>
        {
            new(nameof(UserDynamoDbModel.IsProfileSearchable), ScanOperator.Equal, true)
        };

        var search = _context.ScanAsync<UserDynamoDbModel>(scanConditions);
        var results = await search.GetRemainingAsync();
        _cache = results.Select(x => x.ToDomainModel()).ToArray();
        _cacheExpirationTime = DateTime.UtcNow.AddMinutes(CacheTtlMinutes);
    }
}