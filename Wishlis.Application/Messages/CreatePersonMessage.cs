namespace Wishlis.Application.Messages;

public record CreatePersonMessage(string Name, DateOnly? Birthday);