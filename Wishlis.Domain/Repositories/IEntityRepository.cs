namespace Wishlis.Domain.Repositories;

public interface IEntityRepository<T> where T: class
{
    Task<T> GetAsync(int id);
    Task AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(T entity);
}