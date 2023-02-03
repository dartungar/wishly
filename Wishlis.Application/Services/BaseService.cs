using AutoMapper;
using Wishlis.Domain;
using Wishlis.Domain.Repositories;

namespace Wishlis.Application.Services;

public class BaseService<TEntity, TDto> : IDomainEntityService<TEntity> where TEntity: class, IDomainEntity
{
    protected readonly IEntityRepository<TEntity> _repository;
    protected readonly IMapper _mapper;

    public BaseService(IEntityRepository<TEntity> repo, IMapper mapper)
    {
        _repository = repo;
        _mapper = mapper;
    }

    public async Task<TEntity> Get(int id)
    {
        return await _repository.GetAsync(id);
    }

    public async Task<IEnumerable<TEntity>> Get()
    {
        return await _repository.GetAsync();
    }
    
    public async Task Insert(TDto dto)
    {
        var entity = _mapper.Map<TDto, TEntity>(dto);
        await Insert(entity);
    }
    
    public async Task Insert(TEntity entity)
    {
        await _repository.AddAsync(entity);
    }

    public async Task Delete(int id)
    {
        await _repository.DeleteAsync(id);
    }
}