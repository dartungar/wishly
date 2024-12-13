using Amazon.DynamoDBv2.DataModel;
using Wishlis.Domain.Entities;

namespace Wishlis.Infrastructure.DynamoDB;

[DynamoDBTable("WishlistItems")]
public class WishlistItemDynamoDbModel
{
    [DynamoDBHashKey]
    public string UserId { get; set; }

    [DynamoDBRangeKey]
    public string Id { get; set; }

    public string Name { get; set; }

    public double Price { get; set; }

    public string CurrencyCode { get; set; }

    public string Url { get; set; }

    public bool IsGroupGift { get; set; }

    // for DDB SDK
    public WishlistItemDynamoDbModel() { }
    
    public WishlistItemDynamoDbModel(WishlistItem domainModel)
    {
        Id = domainModel.Id.ToString();
        UserId = domainModel.UserId.ToString();
        Name = domainModel.Name;
        Price = domainModel.Price;
        CurrencyCode = domainModel.CurrencyCode;
        Url = domainModel.Url;
        IsGroupGift = domainModel.IsGroupGift;
    }
    
    public WishlistItem ToDomainModel()
    {
        return new WishlistItem
        {
            Id = Guid.TryParse(Id, out var id) ? id : Guid.Empty, // Convert back to Guid
            UserId = Guid.TryParse(UserId, out var userId) ? userId : Guid.Empty, // Convert back to Guid
            Name = Name,
            Price = Price, // Assuming Price is a decimal or double, no conversion necessary
            CurrencyCode = CurrencyCode,
            Url = Url,
            IsGroupGift = IsGroupGift
        };
    }
}