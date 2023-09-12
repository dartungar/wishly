﻿namespace Common.DTO;

public record WishlistItemDto(
    int Id, 
    int UserId, 
    string Name,
    decimal Cost,
    int Currency, 
    string Url,
    bool IsJointPurchase) 
    : IDto;