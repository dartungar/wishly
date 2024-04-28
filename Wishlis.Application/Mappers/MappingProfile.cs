using AutoMapper;
using Wishlis.Application.DTO;
using Wishlis.Domain.Entities;

namespace Wishlis.Application.Mappers;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<PersonDto, PersonDto>().ReverseMap();
        CreateMap<WishlistItemDto, WishlistItem>().ReverseMap();
    }
}