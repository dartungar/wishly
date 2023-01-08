using Wishlis.Domain;

namespace Wishlis.Application.Services;

public interface IDomainEntityService<TEntity, TDto> where TEntity: IDomainEntity
{
    public Task<TDto> Get(int id);
    public Task<IEnumerable<TDto>> Get();
    public Task Insert(TDto dto);
    public Task Delete(int id);
}