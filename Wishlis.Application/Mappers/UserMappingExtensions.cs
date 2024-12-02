using Wishlis.Application.DTO;
using Wishlis.Domain.Entities;

namespace Wishlis.Application.Mappers;

public static class UserMappingExtensions
{
    public static UserDto ToUserDto(this User user)
        => new UserDto(user.Id, user.Name, user.Birthday, user.CurrencyCode, user.IsProfileSearchable);

    public static User ToUser(this UserDto userDto) => new User()
    {
        Id = userDto.Id ?? Guid.NewGuid(),
        Name = userDto.Name,
        Birthday = userDto.Birthday,
        CurrencyCode = userDto.CurrencyCode,
        IsProfileSearchable = userDto.IsProfileSearchable
    };
}