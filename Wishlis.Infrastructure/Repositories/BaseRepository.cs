using Dapper.Contrib.Extensions;
using Npgsql;
using Wishlis.Domain.Repositories;
using Microsoft.Extensions.Options;

namespace Wishlis.Infrastructure;

public class BaseRepository<T> : IEntityRepository<T> where T: class
{
    protected readonly NpgsqlConnection Connection;
    
    protected BaseRepository(IOptions<DbOptions> options)
    {
        Connection = new NpgsqlConnection(options.Value.ConnectionString);
    }

    public async Task<T> GetAsync(int id)
    {
        return await Connection.GetAsync<T>(id);
    }

    public async Task<IEnumerable<T>> GetAsync()
    {
        return await Connection.GetAllAsync<T>();
    }

    public async Task<int> AddAsync(T entity)
    {
        return await Connection.InsertAsync(entity);
    }

    public async Task<bool> DeleteAsync(T entity)
    {
        return await Connection.DeleteAsync(entity);
    }
}