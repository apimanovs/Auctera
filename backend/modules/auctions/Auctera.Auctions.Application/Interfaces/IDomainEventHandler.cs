using Auctera.Shared.Domain.Abstractions;

namespace Auctera.Auctions.Application.Interfaces;
/// <summary>
/// Represents the i domain event handler interface.
/// </summary>
public interface IDomainEventHandler<in TEvent> where TEvent : IDomainEvent
{
    Task Handle(TEvent domainEvent, CancellationToken ct);
}
