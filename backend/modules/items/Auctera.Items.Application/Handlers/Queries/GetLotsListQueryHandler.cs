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

namespace Auctera.Items.Application.Handlers.Queries;

public class GetLotsListQueryHandler : IRequestHandler<GetLotsListQuery, List<LotDto>>
{
    private readonly ILotRepository _lotRepository;

    public GetLotsListQueryHandler(ILotRepository lotRepository)
    {
        _lotRepository = lotRepository;
    }

    public async Task<List<LotDto>> Handle(GetLotsListQuery request, CancellationToken cancellationToken)
    {
        var query = _lotRepository.GetQueryable();

        query = query.Where(x => x.Status == LotStatus.Listed);

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
            query = query.Where(x => x.Brand.ToLower() == brand);
        }

        return await query
            .Select(lot => new LotDto
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
                    Key = m.Key,
                    Type = m.Type
                }).ToList()
            }).ToListAsync();
    }
}
