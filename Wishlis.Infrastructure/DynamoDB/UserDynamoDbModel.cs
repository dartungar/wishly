using Amazon.DynamoDBv2.DataModel;
using Wishlis.Domain.Entities;

namespace Wishlis.Infrastructure.DynamoDB;

[DynamoDBTable("Users")]
public class UserDynamoDbModel
{
    [DynamoDBHashKey]
    public string Id { get; set; }

    public string Name { get; set; }

    public string? Birthday { get; set; } // Store as ISO 8601 string

    public string? CurrencyCode { get; set; }

    public bool? IsProfileSearchable { get; set; }

    public List<string> FavoriteUserIds { get; set; } = new();

    // for DDB SDK
    public UserDynamoDbModel() { }
    
    public UserDynamoDbModel(User domainModel)
    {
        Id = domainModel.Id.ToString();
        Name = domainModel.Name;
        Birthday = domainModel.Birthday.ToString();
        CurrencyCode = domainModel.CurrencyCode;
        IsProfileSearchable = domainModel.IsProfileSearchable;
        FavoriteUserIds = domainModel.FavoriteUserIds.Select(x => x.ToString()).ToList();
    }
    
    public User ToDomainModel()
    {
        return new User
        {
            Id = Guid.TryParse(Id, out var id) ? id : Guid.Empty, // Convert back to Guid
            Name = Name,
            Birthday = DateOnly.TryParse(Birthday, out var birthday) ? birthday : DateOnly.MinValue, // Convert back to DateTime
            CurrencyCode = CurrencyCode,
            IsProfileSearchable = IsProfileSearchable,
            FavoriteUserIds = FavoriteUserIds?.Select(x => Guid.TryParse(x, out var guid) ? guid : Guid.Empty).ToHashSet() ??
                              [] 
        };
    }
}