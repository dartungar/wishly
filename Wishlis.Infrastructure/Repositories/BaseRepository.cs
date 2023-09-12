using Wishlis.Domain;
using Wishlis.Domain.Repositories;

namespace Wishlis.Infrastructure.Repositories;

public class BaseRepository<T> : IEntityRepository<T> where T: class, IDomainEntity
{
    protected readonly WishlistContext _context;

    protected BaseRepository(WishlistContext context)
    {
        _context = context;
    }

    public virtual async Task<T> GetAsync(int id)
    {
        return await _context.FindAsync<T>(id);
    }

    public virtual async Task AddAsync(T entity)
    {
        await _context.AddAsync<T>(entity);
        await _context.SaveChangesAsync();
    }

    public virtual async Task UpdateAsync(T entity)
    {
        var e = await _context.FindAsync<T>(entity);
        if (e == null)
            throw new InvalidOperationException($"Entity {typeof(T).Name} with ID {entity.Id} not found");
        _context.Entry(e).CurrentValues.SetValues(entity);
        await _context.SaveChangesAsync();
    }

    public virtual async Task DeleteAsync(T entity)
    {
        var e = await _context.FindAsync<T>(entity);
        if (e == null)
            throw new InvalidOperationException($"Entity {typeof(T).Name} with ID {entity.Id} not found");
        _context.Remove(e);
        await _context.SaveChangesAsync();
    }
}