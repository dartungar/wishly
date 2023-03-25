using AutoMapper;
using Wishlis.Domain;
using Wishlis.Domain.Repositories;

namespace Wishlis.Application;

public class BaseServiceWithSearch<TEntity, TDto> : BaseService<TEntity, TDto> where TEntity : class, IDomainEntity
{
    protected new readonly ISearchableEntityRepository<TEntity> _repository;
    public BaseServiceWithSearch(ISearchableEntityRepository<TEntity> repo, IMapper mapper) : base(repo, mapper)
    {
        _repository = repo;
    }

    public async Task<IEnumerable<TDto>> Search(string query)
    {
        var entities = await _repository.FindAsync(query);
        return Mapper.Map<IEnumerable<TDto>>(entities);
    }
}