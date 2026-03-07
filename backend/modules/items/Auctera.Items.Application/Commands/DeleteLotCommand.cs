using MediatR;

namespace Auctera.Items.Application.Commands;

public record DeleteLotCommand (Guid lotId, Guid sellerId) : IRequest;
