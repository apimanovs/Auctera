using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Auctera.Shared.Domain.ValueObjects;
public sealed class Money : IEquatable<Money>
{
    public decimal Amount { get; }
    public string Currency { get; }

    public Money(decimal amount, string currency)
    {
        if (amount < 0)
        {
            throw new ArgumentException("Amount cannot be negative.", nameof(amount));
        }

        if (string.IsNullOrWhiteSpace(currency))
        {
            throw new ArgumentException("Currency cannot be null or empty.", nameof(currency));
        }

        Amount = amount;
        Currency = currency;
    }

    public Money Add(Money moneyToAdd)
    {
        if (moneyToAdd.Currency != Currency)
        {
            throw new ArgumentException("Cannot add amounts with different currencies.", nameof(moneyToAdd));
        }
        return new Money(Amount + moneyToAdd.Amount, Currency);
    }

    public Money Subtract(Money moneyToSusbstract)
    {
        if (moneyToSusbstract.Currency != Currency)
        {
            throw new ArgumentException("Cannot subtract amounts with different currencies.", nameof(moneyToSusbstract));
        }

        if (Amount < moneyToSusbstract.Amount)
        {
            throw new ArgumentException("Resulting amount cannot be negative.", nameof(moneyToSusbstract));
        }

        return new Money(Amount - moneyToSusbstract.Amount, Currency);
    }

    public bool Equals(Money? other)
    {
        if (other is null)
        {
            return false;
        }

        return Amount == other.Amount && Currency == other.Currency;
    }

    public bool GreatherThan(Money amount)
    {
        if (Amount < amount.Amount)
        {
            return false;
        }

        return true;
    }
}
