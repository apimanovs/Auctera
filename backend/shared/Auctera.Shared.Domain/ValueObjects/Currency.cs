using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Auctera.Shared.Domain.Enums;

namespace Auctera.Shared.Domain.ValueObjects;

public sealed class Currency
{
    public CurrencyType Code { get; }

    public Currency(CurrencyType code)
    {
        if (!Enum.IsDefined(typeof(CurrencyType), code))
        {
            throw new ArgumentException("Invalid currency code.", nameof(code));
        }

        Code = code;
    }
}
