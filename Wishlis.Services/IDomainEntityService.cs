using Wishlis.Domain;

namespace Wishlis.Application;

public interface IDomainEntityService<TEntity> where TEntity: IDomainEntity
{
    public Task<TEntity> Get(int id);
    public Task<IEnumerable<TEntity>> Get();
    protected Task<int> Insert(TEntity entity);
    protected Task Update(TEntity entity);
    protected Task Delete(int id);
}