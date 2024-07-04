using Wishlis.Domain.Enums;

namespace Wishlis.Domain.Entities;

public class Price
{
    public double Amount { get; set; }
    public Currency Currency { get; set; }
    
    private Price() { }

    public Price(double amount, Currency currency)
    {
        if (amount <= 0)
            throw new ArgumentException("Amount should be greater than 0");

        Amount = amount;
        Currency = currency;
    }
}