namespace Wishlis.Domain.Repositories;

public interface IEntityRepository<T> where T: class
{
    Task<T> GetAsync(int id);
    Task<IEnumerable<T>> GetAsync();
    Task<int> AddAsync(T entity);
    Task<bool> UpdateAsync(T entity);
    Task<int> DeleteAsync(T entity);
    Task<int> DeleteAsync(int id);
}