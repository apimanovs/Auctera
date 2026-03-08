using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Auctera.Orders.Domain.Enums;

namespace Auctera.Orders.Application.Models;

/// <summary>
/// Represents the order list item dto class.
/// </summary>
public sealed class OrderListItemDto
{
    /// <summary>
    /// Gets or sets the id used by this type.
    /// </summary>
    public Guid Id { get; init; }
    /// <summary>
    /// Gets or sets the auction id used by this type.
    /// </summary>
    public Guid AuctionId { get; init; }
    /// <summary>
    /// Gets or sets the seller id used by this type.
    /// </summary>
    public Guid SellerId { get; init; }
    /// <summary>
    /// Gets or sets the buyer id used by this type.
    /// </summary>
    public Guid BuyerId { get; init; }
    /// <summary>
    /// Gets or sets the status used by this type.
    /// </summary>
    public OrderStatus Status { get; init; }
    /// <summary>
    /// Gets or sets the price used by this type.
    /// </summary>
    public decimal Price { get; init; }
    /// <summary>
    /// Gets or sets the currency used by this type.
    /// </summary>
    public string Currency { get; init; } = default!;
    /// <summary>
    /// Gets or sets the payment deadline utc used by this type.
    /// </summary>
    public DateTime PaymentDeadlineUtc { get; init; }
    /// <summary>
    /// Gets or sets the paid at utc used by this type.
    /// </summary>
    public DateTime? PaidAtUtc { get; init; }
}
