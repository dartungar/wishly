namespace Wishlis.Domain.Repositories;

public interface IUserRepository : ISearchableEntityRepository<User>
{
    Task<User> GetByExternalId(string externalId);
    Task CreateExternalId(int userId, string externalId);
}