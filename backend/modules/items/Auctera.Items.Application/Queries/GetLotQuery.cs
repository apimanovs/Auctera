using Auctera.Items.Application.Models;
using MediatR;

namespace Auctera.Items.Application.Queries;

/// <summary>
/// Represents the get lot query record.
/// </summary>
public record GetLotQuery(Guid lotId) : IRequest<LotDto>;
