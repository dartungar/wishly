using Wishlis.Domain;

namespace Wishlis.Application.Services;

public interface IDomainEntityService<TEntity> where TEntity: IDomainEntity
{
    public Task<TEntity> Get(int id);
    public Task<IEnumerable<TEntity>> Get();
    public Task Insert(TEntity entity);
    public Task Delete(int id);
}