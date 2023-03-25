using Dapper.Contrib.Extensions;
using Microsoft.Extensions.Options;
using Npgsql;
using Wishlis.Domain;
using Wishlis.Domain.Repositories;

namespace Wishlis.Infrastructure.Repositories;

public class BaseRepository<T> : IEntityRepository<T> where T: class, IDomainEntity
{
    protected readonly NpgsqlConnection Connection;

    protected BaseRepository(IOptions<DbOptions> options)
    {
        Connection = new NpgsqlConnection(options.Value.ConnectionString);
    }

    public virtual async Task<T> GetAsync(int id)
    {
        return await Connection.GetAsync<T>(id);
    }

    public virtual async Task<IEnumerable<T>> GetAsync()
    {
        return await Connection.GetAllAsync<T>();
    }

    public virtual async Task<int> AddAsync(T entity)
    {
        return await Connection.InsertAsync(entity);
    }

    public virtual async Task<bool> UpdateAsync(T entity)
    {
        return await Connection.UpdateAsync(entity);
    }

    public virtual async Task<int> DeleteAsync(T entity)
    {
        var success = await Connection.DeleteAsync(entity);
        return success ? 1 : 0;
    }

    public virtual Task<int> DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }
}