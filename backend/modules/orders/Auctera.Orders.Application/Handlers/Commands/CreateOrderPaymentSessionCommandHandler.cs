using Auctera.Orders.Application.Commands;
using Auctera.Orders.Application.Interfaces;
using Auctera.Orders.Domain.Enums;
using Auctera.Orders.Infrastructure.Options;

using MediatR;

using Stripe.Checkout;

public sealed class CreateOrderPaymentSessionCommandHandler
    : IRequestHandler<CreateOrderPaymentSessionCommand, CreateOrderPaymentSessionResult>
{
    private readonly StripeOptions _stripeOptions;
    private readonly IOrderRepository _orders;

    public CreateOrderPaymentSessionCommandHandler(
        StripeOptions stripeOptions,
        IOrderRepository orders)
    {
        _stripeOptions = stripeOptions;
        _orders = orders;
    }

    public async Task<CreateOrderPaymentSessionResult> Handle(
        CreateOrderPaymentSessionCommand command,
        CancellationToken ct)
    {
        var order = await _orders.GetOrderByIdAsync(command.OrderId, ct);

        if (order is null)
        {
            throw new KeyNotFoundException($"Order {command.OrderId} not found.");
        }

        if (order.BuyerId != command.UserId)
        {
            throw new UnauthorizedAccessException("This order does not belong to the user.");
        }

        if (order.Status != OrderStatus.PendingPayment)
        {
            throw new InvalidOperationException("Order is not available for payment.");
        }

        var amountInCents = (long)Math.Round(order.Price * 100m, MidpointRounding.AwayFromZero);

        var options = new SessionCreateOptions
        {
            Mode = "payment",
            SuccessUrl = _stripeOptions.SuccessUrl + "?session_id={CHECKOUT_SESSION_ID}",
            CancelUrl = _stripeOptions.CancelUrl,
            ClientReferenceId = order.Id.ToString(),

            Metadata = new Dictionary<string, string>
            {
                ["orderId"] = order.Id.ToString(),
                ["buyerId"] = order.BuyerId.ToString()
            },

            LineItems = new List<SessionLineItemOptions>
            {
                new()
                {
                    Quantity = 1,
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        Currency = order.Currency.ToLowerInvariant(),
                        UnitAmount = amountInCents,
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = $"Auction item {order.AuctionId}"
                        }
                    }
                }
            }
        };

        var service = new SessionService();
        var session = await service.CreateAsync(options, cancellationToken: ct);

        if (string.IsNullOrWhiteSpace(session.Url))
        {
            throw new InvalidOperationException("Stripe checkout session URL was not returned.");
        }

        order.SetStripeSession(session.Id);

        await _orders.UpdateOrderAsync(order, ct);

        return new CreateOrderPaymentSessionResult(session.Url);
    }
}
