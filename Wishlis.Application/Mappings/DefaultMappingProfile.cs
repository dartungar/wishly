using AutoMapper;
using Wishlis.Application.DTO;
using Wishlis.Domain;

namespace Wishlis.Application.Mappings;

public class DefaultMappingProfile : Profile
{
    public DefaultMappingProfile()
    {
        // Entity -> Dto mapping
        CreateMap<User, UserDto>();
        CreateMap<WishlistItem, WishlistItemDto>();
        
        // Dto -> Entity mapping
    }
}