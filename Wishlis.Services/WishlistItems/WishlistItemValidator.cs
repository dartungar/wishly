using FluentValidation;
using Wishlis.Domain;

namespace Wishlis.Services.WishlistItems;

public class WishlistItemValidator : AbstractValidator<WishlistItem>
{
    public WishlistItemValidator()
    {
        // RuleFor(x => x.Currency)
        //     .NotEmpty()
        //     .Must(c => c == Currency.Usd || c == Currency.Eur || c == Currency.Rub)
        //     .WithMessage("Currency must be USD, EUR or RUB");
    }
}