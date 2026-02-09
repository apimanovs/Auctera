using Auctera.Shared.Infrastructure.Interfaces;
using Auctera.Shared.Domain.Abstractions;
using MediatR;

namespace Auctera.Shared.Infrastructure.Dispatcher;

public sealed class DomainEventDispatcher : IDomainEventDispatcher
{
    private readonly IMediator _mediator;

    public DomainEventDispatcher(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task DispatchAsync(IEnumerable<IDomainEvent> events, CancellationToken ct)
    {
        foreach (var domainEvent in events)
        {
            await _mediator.Publish(domainEvent, ct);
        }
    }
}
