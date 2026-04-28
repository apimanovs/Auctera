using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Auctera.Items.Application.Interfaces;
using Auctera.Items.Application.Models;
using Auctera.Items.Application.Queries;
using Auctera.Shared.Domain.Enums;

using Microsoft.EntityFrameworkCore;

using MediatR;
using Auctera.Identity.Application.Interfaces;
using Auctera.Shared.Infrastructure.Media;

using Microsoft.Extensions.Options;

namespace Auctera.Items.Application.Handlers.Queries;

/// <summary>
/// Represents the get lots list query handler class.
/// </summary>
public class GetLotsListQueryHandler : IRequestHandler<GetLotsListQuery, List<LotPreviewDto>>
{
    private readonly ILotRepository _lotRepository;
    private readonly IUserRepository _userRepository;
    private readonly string _publicBaseUrl;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetLotsListQueryHandler"/> class.
    /// </summary>
    /// <param name="lotRepository">Lot repository.</param>
    public GetLotsListQueryHandler(
        ILotRepository lotRepository,
        IUserRepository userRepository,
        IOptions<MediaOptions> mediaOptions)
    {
        _lotRepository = lotRepository;
        _userRepository = userRepository;
        _publicBaseUrl = mediaOptions.Value.PublicBaseUrl;
    }

    /// <summary>
    /// Handles the operation.
    /// </summary>
    /// <param name="request">Input data for the operation.</param>
    /// <param name="cancellationToken">Cancellation token for the operation.</param>
    /// <returns>A task that returns the operation result.</returns>
    public async Task<List<LotPreviewDto>> Handle(GetLotsListQuery request, CancellationToken cancellationToken)
    {
        var query = _lotRepository.GetQueryable();

        query = query.Where(x =>
            x.Status == LotStatus.Published ||
            x.Status == LotStatus.Listed);

        if (request.Status.HasValue)
        {
            if (request.Status.Value is not LotStatus.Published and not LotStatus.Listed)
            {
                return [];
            }

            query = query.Where(x => x.Status == request.Status.Value);
        }

        if (!string.IsNullOrWhiteSpace(request.Search))
        {
            var search = request.Search.Trim().ToLower();
            query = query.Where(x =>
                x.Title.ToLower().Contains(search) ||
                x.Brand.ToLower().Contains(search) ||
                x.Description.ToLower().Contains(search));
        }

        if (request.Category.HasValue)
        {
            query = query.Where(x => x.Category == request.Category.Value);
        }

        if (request.Gender.HasValue)
        {
            query = query.Where(x => x.Gender == request.Gender.Value);
        }

        if (request.Size.HasValue)
        {
            query = query.Where(x => x.Size == request.Size.Value);
        }

        if (!string.IsNullOrWhiteSpace(request.Brand))
        {
            var brand = request.Brand.Trim().ToLower();
            query = query.Where(x => x.Brand.ToLower().Contains(brand));
        }

        if (request.Condition.HasValue)
        {
            query = query.Where(x => x.Condition == request.Condition.Value);
        }

        if (request.MinPrice.HasValue)
        {
            query = query.Where(x => x.Price.Amount >= request.MinPrice.Value);
        }

        if (request.MaxPrice.HasValue)
        {
            query = query.Where(x => x.Price.Amount <= request.MaxPrice.Value);
        }

        if (request.MinYear.HasValue)
        {
            query = query.Where(x => x.Year >= request.MinYear.Value);
        }

        if (request.MaxYear.HasValue)
        {
            query = query.Where(x => x.Year <= request.MaxYear.Value);
        }

        if (!string.IsNullOrWhiteSpace(request.City) ||
            !string.IsNullOrWhiteSpace(request.Country) ||
            !string.IsNullOrWhiteSpace(request.Location))
        {
            var sellerIds = await _userRepository.GetUserIdsByLocationAsync(
                request.City,
                request.Country,
                request.Location,
                cancellationToken);

            if (sellerIds.Count == 0)
            {
                return [];
            }

            query = query.Where(x => sellerIds.Contains(x.SellerId));
        }

        var lots = await query
            .Select(lot => new LotPreviewDto
            {
                Id = lot.Id,
                SellerId = lot.SellerId,
                Title = lot.Title,
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
                Year = lot.Year,
                Status = lot.Status,
                StatusName = lot.Status.ToString(),
                Media = lot.Media.Select(m => new LotMediaDto
                {
                    Key = m.Key,
                    Type = m.Type,
                    Url = $"{_publicBaseUrl}{m.Key}"
                }).ToList()
            }).ToListAsync(cancellationToken);

        var sellers = await _userRepository.GetUsersByIdsAsync(
            lots.Select(l => l.SellerId).Distinct().ToList(),
            cancellationToken);
        var sellersById = sellers.ToDictionary(s => s.Id);

        foreach (var lot in lots)
        {
            if (sellersById.TryGetValue(lot.SellerId, out var seller))
            {
                lot.SellerCity = seller.City;
                lot.SellerCountry = seller.Country;
            }
        }

        return lots;
    }
}
