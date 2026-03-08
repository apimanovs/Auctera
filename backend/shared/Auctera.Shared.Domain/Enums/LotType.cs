using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auctera.Shared.Domain.Enums;

/// <summary>
/// Represents the lot status enum.
/// </summary>
public enum LotStatus
{
    Draft,
    Published,
    Listed,
    Sold,
    Removed
}
