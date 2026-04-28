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

namespace Auctera.Items.Application.Handlers.Queries;

/// <summary>
/// Represents the get lots list query handler class.
/// </summary>
public class GetLotsListQueryHandler : IRequestHandler<GetLotsListQuery, List<LotPreviewDto>>
{
    private readonly ILotRepository _lotRepository;
    private readonly IUserRepository _userRepository;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetLotsListQueryHandler"/> class.
    /// </summary>
    /// <param name="lotRepository">Lot repository.</param>
    public GetLotsListQueryHandler(ILotRepository lotRepository, IUserRepository userRepository)
    {
        _lotRepository = lotRepository;
        _userRepository = userRepository;
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

        query = query.Where(x => x.Status == LotStatus.Published || x.Status == LotStatus.Sold);

        if (request.Category.HasValue)
        {
            query = query.Where(x => x.Category == request.Category.Value);
        }

        if (request.Status.HasValue)
        {
            query = query.Where(x => x.Status == request.Status.Value);
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
            query = query.Where(x => x.Brand.ToLower() == brand);
        }

        return await query
            .Select(lot => new LotPreviewDto
            {
                Id = lot.Id,
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
                Media = lot.Media.Select(m => new LotMediaDto
                {
                    Key = m.Key,
                    Type = m.Type
                }).ToList()
            }).ToListAsync();
    }
}
