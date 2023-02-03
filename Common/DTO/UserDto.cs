namespace Common.DTO;

public record UserDto(
    int Id, 
    string Name, 
    string PublicId,
    DateTime DateOfBirth
    ) : IDto;