using Dapper;
using Microsoft.Extensions.Options;
using Wishlis.Domain;
using Wishlis.Domain.Repositories;

namespace Wishlis.Infrastructure.Repositories;

public class WishlistItemRepository : BaseRepository<WishlistItem>, IWishlistItemRepository
{
    public WishlistItemRepository(IOptions<DbOptions> options) : base(options)
    {
    }

    public async Task<IEnumerable<WishlistItem>> GetByUserId(int userId)
    {
        return await Connection.QueryAsync<WishlistItem>(@$"
                SELECT * FROM wishlist_items 
                WHERE {nameof(WishlistItem.UserId)} = {userId}");
    }

    public override async Task<WishlistItem> GetAsync(int id)
    {
        return await Connection.QueryFirstAsync<WishlistItem>(@$"
            SELECT 
                id as Id,
                user_id as UserId,
                name as Name,
                external_url as ExternalUrl,
                cost as Cost,
                currency as Currency,
                is_joint_purchase as IsJointPurchase
            FROM wishlist_items 
            WHERE {nameof(WishlistItem.Id)} = {id}");
    }

    public override async Task<int> DeleteAsync(int id)
    {
        return await Connection.ExecuteAsync(@$"
            DELETE FROM wishlist_items 
            WHERE {nameof(WishlistItem.Id)} = {id}");
    }
    
    public override async Task<int> AddAsync(WishlistItem entity)
    {
        return await Connection.ExecuteAsync(@$"
            INSERT INTO wishlist_items
            (user_id, name, external_url, cost, currency, is_joint_purchase)
            VALUES (
                    {entity.UserId}, 
                    '{entity.Name}', 
                    '{entity.ExternalUrl}', 
                    {entity.Cost}, 
                    '{entity.Currency}', 
                    {entity.IsJointPurchase})");
    }
    
    public override async Task<bool> UpdateAsync(WishlistItem entity)
    {
        await Connection.ExecuteAsync(@$"
            UPDATE wishlist_items
            SET 
                user_id = {entity.UserId}, 
                name = '{entity.Name}', 
                external_url = '{entity.ExternalUrl}', 
                cost = {entity.Cost}, 
                currency = {entity.Currency}, 
                is_joint_purchase = {entity.IsJointPurchase}
            WHERE {nameof(WishlistItem.Id)} = {entity.Id}");

        return true;
    }
    
    
}