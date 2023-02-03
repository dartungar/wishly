using Dapper;
using Microsoft.Extensions.Options;
using Wishlis.Domain;
using Wishlis.Domain.Repositories;

namespace Wishlis.Infrastructure.Repositories;

public class UserRepository : BaseRepository<User>, IUserRepository
{
    public UserRepository(IOptions<DbOptions> options) : base(options)
    {
    }

    public override async Task<IEnumerable<User>> GetAsync()
    {
        return await Connection.QueryAsync<User>(@$"
            SELECT 
                id as Id,
                name as Name,
                publicid as PublicId,
                dateofbirth as DateOfBirth
            FROM users
            ");
    }
    
    public override async Task<User> GetAsync(int id)
    {
        return await Connection.QueryFirstAsync<User>(@$"
            SELECT 
                id as Id,
                name as Name,
                publicid as PublicId,
                dateofbirth as DateOfBirth
            FROM users
            WHERE id = {id}
            ");
    }
    
    public override async Task<int> AddAsync(User entity)
    {
        return await Connection.ExecuteAsync(@$"
            INSERT INTO users
            (name, publicid, dateofbirth)
            VALUES ('{entity.Name}', '{entity.PublicId}', '{entity.DateOfBirth:yyyy-MM-dd}')
            ");
    }
    
    public override async Task<int> DeleteAsync(User entity)
    {
        return await Connection.ExecuteAsync(@$"
            DELETE FROM users
            WHERE id = {entity.Id}
            ");
    }
    
    public override async Task<int> DeleteAsync(int id)
    {
        return await Connection.ExecuteAsync(@$"
            DELETE FROM users
            WHERE id = {id}
            ");
    }

    public async Task<User> GetUserByExternalId(string externalId)
    {
        return await Connection.QueryFirstAsync<User>(@$"
            SELECT
                id as Id,
                name as Name,
                publicid as PublicId,
                dateofbirth as DateOfBirth
            FROM users u 
            JOIN {nameof(UserExternalId)} ueid 
                       ON u.id = ueid.{nameof(UserExternalId.ExternalId)}");
    }

    // TODO: check if LIKE works
    public async Task<IEnumerable<User>> FindAsync(string searchQuery)
    {
        return await Connection.QueryAsync<User>(@$"
            SELECT
                id as Id,
                name as Name,
                publicid as PublicId,
                dateofbirth as DateOfBirth
            FROM users
                     WHERE name LIKE '%{searchQuery}%'
            UNION
            SELECT
                id as Id,
                name as Name,
                publicid as PublicId,
                dateofbirth as DateOfBirth
            FROM users
                     WHERE publicid LIKE '%{searchQuery}%'");
    }
}