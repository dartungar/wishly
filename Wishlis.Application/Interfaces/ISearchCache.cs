using Wishlis.Application.DTO;
using Wishlis.Domain.Entities;

namespace Wishlis.Application.Interfaces;

public interface ISearchCache
{
    Task<IEnumerable<User>> SearchUsers(string query);

    void Invalidate();
}