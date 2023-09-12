using Wishlis.Domain;

namespace Wishlis.Services;

public interface IDomainEntityService<TEntity> where TEntity: IDomainEntity
{
    public Task<TEntity> Get(int id);
    protected Task Insert(TEntity entity);
    protected Task Update(TEntity entity);
    protected Task Delete(int id);
}