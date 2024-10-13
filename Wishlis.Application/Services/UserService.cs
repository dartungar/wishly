﻿using AutoMapper;
using Wishlis.Application.DTO;
using Wishlis.Application.Interfaces;
using Wishlis.Domain.Entities;
using Wishlis.Domain.Repositories;

namespace Wishlis.Application.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public UserService(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<Guid> CreateUser(UserDto model)
    {
        return await _userRepository.Create(_mapper.Map<User>(model));
    }

    public async Task DeleteUser(Guid id)
    {
        await _userRepository.Delete(id);
    }

    public async Task UpdateUser(UserDto model)
    {
        await _userRepository.Update(_mapper.Map<User>(model));
    }

    public async Task<UserDto> GetById(Guid id)
    {
        var user = await _userRepository.GetById(id);
        return _mapper.Map<UserDto>(user);
    }

    public async Task<IEnumerable<UserDto>> GetFavoriteUsers(Guid ownerId)
    {
        var favoriteUsers = await _userRepository.GetFavoriteUsers(ownerId);
        return _mapper.Map<IEnumerable<UserDto>>(favoriteUsers);
    }

    public async Task AddUserToFavorites(Guid favoriteUserId, Guid ownerId)
    {
        await _userRepository.AddUserToFavorites(favoriteUserId, ownerId);
    }
}