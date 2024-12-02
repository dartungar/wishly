namespace Wishlis.Application.DTO;

public record UserDto(Guid? Id, string Name, DateOnly? Birthday, string? CurrencyCode, bool? IsProfileSearchable);