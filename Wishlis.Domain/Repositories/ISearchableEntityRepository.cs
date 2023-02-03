namespace Wishlis.Domain.Repositories;

public interface ISearchableEntityRepository<T> : IEntityRepository<T> where T: class
{
    Task<IEnumerable<T>> FindAsync(string searchQuery);
}