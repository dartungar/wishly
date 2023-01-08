namespace Wishlis.Domain.Repositories;

public interface ISearchableEntityRepository<T> : IEntityRepository<T>
{
    Task<IEnumerable<T>> FindAsync(string searchQuery);
}