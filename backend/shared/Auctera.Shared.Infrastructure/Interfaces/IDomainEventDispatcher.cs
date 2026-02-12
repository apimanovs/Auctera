using Auctera.Shared.Domain.Abstractions;

namespace Auctera.Shared.Infrastructure.Interfaces;

public interface IDomainEventDispatcher
{
    Task DispatchAsync(IEnumerable<IDomainEvent> events, CancellationToken ct);
}
