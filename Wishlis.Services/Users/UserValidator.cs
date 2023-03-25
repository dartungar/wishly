using FluentValidation;
using Wishlis.Domain;

namespace Wishlis.Application.Users;

public class UserValidator : AbstractValidator<User>
{
    public UserValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Name must be not empty");
        
        RuleFor(x => x.DateOfBirth)
            .NotEmpty()
            .Must(dob => dob > new DateTime(1900, 01, 01))
            .WithMessage("Date of birth must be after 01.01.1900");
    }
}