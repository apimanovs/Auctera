using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Auctera.Identity.Application.Interfaces;
using Auctera.Items.Application.Interfaces;
using Auctera.Items.Application.Models;
using Auctera.Items.Application.Queries;
using Auctera.Shared.Infrastructure.Media;

using MediatR;

using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.Extensions.Options;

namespace Auctera.Items.Application.Handlers.Queries;
/// <summary>
/// Represents the get lot query handler class.
/// </summary>
public sealed class GetLotQueryHandler(ILotRepository lotRepository, IUserRepository userRepository, IOptions<MediaOptions> mediaOptions) : IRequestHandler<GetLotQuery, LotDto>
{
    private readonly ILotRepository _lotRepository = lotRepository;
    private readonly IUserRepository _userRepository = userRepository;
    private readonly string _publicBaseUrl = mediaOptions.Value.PublicBaseUrl;

    /// <summary>
    /// Handles the operation.
    /// </summary>
    /// <param name="request">Input data for the operation.</param>
    /// <param name="cancellationToken">Cancellation token for the operation.</param>
    /// <returns>A task that returns the operation result.</returns>
    public async Task<LotDto> Handle(GetLotQuery request, CancellationToken cancellationToken)
    {
        var lot = await _lotRepository.GetLotById(request.lotId, cancellationToken);

        if (lot == null)
        {
            throw new KeyNotFoundException($"Lot is null");
        }

        var seller = _userRepository.GetUserByIdAsync(lot.SellerId);

        if (lot is null)
        {
            throw new KeyNotFoundException($"Lot with id {request.lotId} not found.");
        }

        return new LotDto
        {
            Id = lot.Id,
            Title = lot.Title,
            Description = lot.Description,
            Price = lot.Price.Amount,
            Currency = lot.Price.Currency,
            Category = lot.Category,
            CategoryName = lot.Category.ToString(),
            Gender = lot.Gender,
            GenderName = lot.Gender.ToString(),
            Size = lot.Size,
            SizeName = lot.Size.ToString(),
            Brand = lot.Brand,
            Condition = lot.Condition,
            ConditionName = lot.Condition.ToString(),
            Color = lot.Color,
            Status = lot.Status,
            StatusName = lot.Status.ToString(),
            Media = lot.Media.Select(m => new LotMediaDto
            {
                Key = m.Key, // url
                Type = m.Type,
                Url = $"{ _publicBaseUrl }{ m.Key }"
            })
            .ToList(),
            Seller = new LotSellerDto
            {
                Id = seller.Result.Id,
                Name = seller.Result.Name,
                Username = seller.Result.UserName,
            }
        };
    }
}
