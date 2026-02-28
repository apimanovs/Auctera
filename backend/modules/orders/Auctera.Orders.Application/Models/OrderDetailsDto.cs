using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Auctera.Orders.Domain.Enums;

namespace Auctera.Orders.Application.Models;

public sealed class OrderDetailsDto
{
    public Guid Id { get; init; }
    public Guid AuctionId { get; init; }
    public Guid SellerId { get; init; }
    public Guid BuyerId { get; init; }
    public OrderStatus Status { get; init; }
    public decimal Price { get; init; }
    public string Currency { get; init; } = default!;
    public DateTime PaymentDeadlineUtc { get; init; }
    public string? StripeCheckoutSessionId { get; init; }
    public DateTime? PaidAtUtc { get; init; }
}
