using Auctera.Shared.Domain.Abstractions;

namespace Auctera.Auctions.Application.Interfaces;
public interface IDomainEventHandler<in TEvent> where TEvent : IDomainEvent
{
    Task Handle(TEvent domainEvent, CancellationToken ct);
}
