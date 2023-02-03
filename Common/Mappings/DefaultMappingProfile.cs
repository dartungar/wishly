using AutoMapper;
using Common.DTO;
using Wishlis.Domain;

namespace Common.Mappings;

public class DefaultMappingProfile : Profile
{
    public DefaultMappingProfile()
    {
        CreateMap<User, UserDto>().ReverseMap();
        CreateMap<WishlistItem, WishlistItemDto>().ReverseMap();
    }
}