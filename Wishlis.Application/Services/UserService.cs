using AutoMapper;
using Common.DTO;
using Wishlis.Domain;
using Wishlis.Domain.Repositories;

namespace Wishlis.Application.Services;

public class UserService : BaseServiceWithSearch<User, UserDto>
{
    public UserService(IUserRepository repo, IMapper mapper) : base(repo, mapper)
    {
    }
}