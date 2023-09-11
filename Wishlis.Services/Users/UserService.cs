using AutoMapper;
using Common.DTO;
using Common.Exceptions;
using Wishlis.Domain;
using Wishlis.Domain.Repositories;

namespace Wishlis.Services.Users;

public class UserService : BaseServiceWithSearch<User, UserDto>
{
    private readonly IUserRepository _repo;
    private readonly UserValidator _validator;
    public UserService(IUserRepository repo, IMapper mapper) : base(repo, mapper)
    {
        _repo = repo;
        _validator = new UserValidator();
    }

    public override Task<int> Insert(UserDto dto)
    {
        var user = Mapper.Map<User>(dto);
        ThrowIfInvalid(user);
        return base.Insert(dto);
    }
    
    public override Task<int> Insert(User entity)
    {
        ThrowIfInvalid(entity);
        return base.Insert(entity);
    }

    public override Task Update(UserDto dto)
    {
        var user = Mapper.Map<User>(dto);
        ThrowIfInvalid(user);
        return base.Update(user);
    }
    
    public override Task Update(User entity)
    {
        ThrowIfInvalid(entity);
        return base.Update(entity);
    }

    private void ThrowIfInvalid(User entity)
    {
        var result = _validator.Validate(entity);
        if (!result.IsValid)
            throw new WishlisLogicException(result.ToString("~"));
    }
}