using AutoMapper;
using Wishlis.Application.DTO;
using Wishlis.Application.Interfaces;
using Wishlis.Domain.Entities;
using Wishlis.Domain.Repositories;

namespace Wishlis.Application.Services;

public class WishlistItemService : IWishlistItemService
{
    private readonly IWishlistItemRepository _wishlistItemRepository;
    private readonly IMapper _mapper;

    public WishlistItemService(IWishlistItemRepository wishlistItemRepository, IMapper mapper)
    {
        _wishlistItemRepository = wishlistItemRepository;
        _mapper = mapper;
    }

    public async Task Create(WishlistItemDto model)
    {
        await _wishlistItemRepository.Create(_mapper.Map<WishlistItem>(model));
    }
    
    public async Task Save(WishlistItemDto model)
    {
        await _wishlistItemRepository.Save(_mapper.Map<WishlistItem>(model));
    }
    
    public async Task Delete(int id)
    {
        await _wishlistItemRepository.Delete(id);
    }

    public async Task<IEnumerable<WishlistItemDto>> Get()
    {
        var items = await _wishlistItemRepository.Get();
        return _mapper.Map<IEnumerable<WishlistItemDto>>(items);
    }

    public async Task<IEnumerable<WishlistItemDto>> GetByUserId(Guid userId)
    {
        var items = await _wishlistItemRepository.GetByUserId(userId);
        return _mapper.Map<IEnumerable<WishlistItemDto>>(items);
    }
}