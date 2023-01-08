namespace Wishlis.Domain.Repositories;

public interface IEntityRepository<T>
{
    Task<T> GetAsync(int id);
    Task<IEnumerable<T>> GetAsync();
    Task<int> AddAsync(T entity);
    Task<bool> DeleteAsync(T entity);
}