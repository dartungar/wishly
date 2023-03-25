using AutoMapper;
using Wishlis.Domain;
using Wishlis.Domain.Repositories;

namespace Wishlis.Application;

public class BaseService<TEntity, TDto> : IDomainEntityService<TEntity> where TEntity: class, IDomainEntity
{
    protected readonly IEntityRepository<TEntity> Repository;
    protected readonly IMapper Mapper;

    public BaseService(IEntityRepository<TEntity> repo, IMapper mapper)
    {
        Repository = repo;
        Mapper = mapper;
    }

    public virtual async Task<TEntity> Get(int id)
    {
        return await Repository.GetAsync(id);
    }

    public virtual async Task<IEnumerable<TEntity>> Get()
    {
        return await Repository.GetAsync();
    }
    
    public virtual async Task<int> Insert(TDto dto)
    {
        var entity = Mapper.Map<TDto, TEntity>(dto);
        return await Insert(entity);
    }
    
    public virtual async Task<int> Insert(TEntity entity)
    {
        return await Repository.AddAsync(entity);
    }
    
    public virtual async Task Update(TDto dto)
    {
        var entity = Mapper.Map<TDto, TEntity>(dto);
        await Repository.UpdateAsync(entity);
    }

    public virtual async Task Update(TEntity entity)
    {
        await Repository.UpdateAsync(entity);
    }

    public virtual async Task Delete(int id)
    {
        await Repository.DeleteAsync(id);
    }
}