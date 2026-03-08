using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Auctera.Items.Application.Queries;
using Auctera.Items.Application.Models;
using MediatR;
using System.Runtime.CompilerServices;
using Auctera.Items.Application.Interfaces;

namespace Auctera.Items.Application.Handlers.Queries;
/// <summary>
/// Represents the get lot query handler class.
/// </summary>
public sealed class GetLotQueryHandler(ILotRepository lotRepository) : IRequestHandler<GetLotQuery, LotDto>
{
    private readonly ILotRepository _lotRepository = lotRepository;

    /// <summary>
    /// Handles the operation.
    /// </summary>
    /// <param name="request">Input data for the operation.</param>
    /// <param name="cancellationToken">Cancellation token for the operation.</param>
    /// <returns>A task that returns the operation result.</returns>
    public async Task<LotDto> Handle(GetLotQuery request, CancellationToken cancellationToken)
    {
        var lot = await _lotRepository.GetLotById(request.lotId, cancellationToken);

        if (lot is null)
        {
            throw new KeyNotFoundException($"Lot with id {request.lotId} not found.");
        }

        return new LotDto
        {
            Id = lot.Id,
            SellerId = lot.SellerId,
            Title = lot.Title,
            Description = lot.Description,
            Price = lot.Price.Amount,
            Currency = lot.Price.Currency,
            Category = lot.Category,
            Gender = lot.Gender,
            Size = lot.Size,
            Brand = lot.Brand,
            Condition = lot.Condition,
            Color = lot.Color,
            Media = lot.Media.Select(m => new LotMediaDto
            {
                Key = m.Key, // url
                Type = m.Type
            }).ToList()
        };
    }
}
