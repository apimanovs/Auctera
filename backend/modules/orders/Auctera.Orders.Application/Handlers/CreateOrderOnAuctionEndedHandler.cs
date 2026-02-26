using Auctera.Auctions.Domain.Events;
using Auctera.Orders.Application.Interfaces;
using Auctera.Orders.Domain;
using Auctera.Shared.Domain.Time;
using Auctera.Items.Application.Interfaces;

using MediatR;

namespace Auctera.Orders.Application.Handlers;

public sealed class CreateOrderOnAuctionEndedHandler : INotificationHandler<AuctionEndedDomainEvent>
{
    private readonly IOrderRepository _orderRepository;
    private readonly ILotRepository _lotRepository;
    private readonly IClock _clock;

    public CreateOrderOnAuctionEndedHandler(IOrderRepository orderRepository, IClock clock, ILotRepository lotRepository)
    {
        _orderRepository = orderRepository;
        _clock = clock;
        _lotRepository = lotRepository;
    }

    public async Task Handle(AuctionEndedDomainEvent endedDomainEvent, CancellationToken cancellationToken)
    {
        if (endedDomainEvent.WinnerId == null || endedDomainEvent.WinningMoney is null)
        {
            throw new InvalidOperationException("No winner for this auction.");
        }

        if (await _orderRepository.ExistsForAuctionAsync(endedDomainEvent.AuctionId, cancellationToken))
        {
            throw new InvalidOperationException("Order for this auction already exists.");
        }

        var lot = await _lotRepository.GetLotById(endedDomainEvent.LotId, cancellationToken);

        if (lot == null)
        {
            throw new InvalidOperationException("Lot not found.");
        }

        var order = Order.Create
        (
            endedDomainEvent.AuctionId, lot.SellerId,
            endedDomainEvent.WinnerId, endedDomainEvent.WinningMoney.Amount,
            endedDomainEvent.WinningMoney.Currency, _clock.UtcNow.AddHours(48)
        );

        await _orderRepository.AddOrderAsync(order, cancellationToken);
        await _orderRepository.UpdateOrderAsync(order, cancellationToken);
    }
}
