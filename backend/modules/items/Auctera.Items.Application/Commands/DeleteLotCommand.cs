using MediatR;

namespace Auctera.Items.Application.Commands;

/// <summary>
/// Represents the delete lot command record.
/// </summary>
public record DeleteLotCommand (Guid lotId, Guid sellerId) : IRequest;
