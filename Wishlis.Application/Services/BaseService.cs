using AutoMapper;
using Wishlis.Domain;
using Wishlis.Domain.Repositories;

namespace Wishlis.Application.Services;

public class BaseService<TEntity, TDto> : IDomainEntityService<TEntity, TDto> where TEntity: IDomainEntity
{
    private readonly IEntityRepository<TEntity> _repository;
    private readonly IMapper _mapper;

    public BaseService(IEntityRepository<TEntity> repo, IMapper mapper)
    {
        _repository = repo;
    }

    public async Task<TDto> Get(int id)
    {
        var entity = await _repository.GetAsync(id);
        return _mapper.Map<TDto>(entity);
    }

    public async Task<IEnumerable<TDto>> Get()
    {
        var entities = await _repository.GetAsync();
        return _mapper.Map<IEnumerable<TDto>>(entities);
    }

    public async Task Insert(TDto dto)
    {
        // TODO: implement mapping DTO->Entity
        var entity = _mapper.Map<TEntity>(dto);
        await _repository.AddAsync(entity);
    }

    public async Task Delete(int id)
    {
        var entity = await _repository.GetAsync(id);
        await _repository.DeleteAsync(entity);
    }
}