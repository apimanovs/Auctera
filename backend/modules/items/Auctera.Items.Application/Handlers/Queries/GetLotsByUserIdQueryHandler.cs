using Auctera.Items.Application.Interfaces;
using Auctera.Items.Application.Models;
using Auctera.Items.Application.Queries;
using Auctera.Shared.Infrastructure.Media;

using MediatR;

using Microsoft.Extensions.Options;

namespace Auctera.Items.Application.Handlers.Queries;

public sealed class GetLotsByUserIdQueryHandler(ILotRepository lotRepository, IOptions<MediaOptions> mediaOptions)
    : IRequestHandler<GetLotsByUserIdQuery, IReadOnlyList<MyLotListItemDto>>
{
    private readonly ILotRepository _lotRepository = lotRepository;
    private readonly string _publicBaseUrl = mediaOptions.Value.PublicBaseUrl;

    public async Task<IReadOnlyList<MyLotListItemDto>> Handle(GetLotsByUserIdQuery request, CancellationToken cancellationToken)
    {
        var lots = await _lotRepository.GetLotsBySellerIdAsync(request.UserId, cancellationToken);

        return lots
            .Select(lot => new MyLotListItemDto
            {
                Id = lot.Id,
                SellerId = lot.SellerId,
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
                Year = lot.Year,
                Status = lot.Status,
                StatusName = lot.Status.ToString(),
                AuctionId = null,
                CreatedAt = null,
                Media = lot.Media.Select(m => new LotMediaDto
                {
                    Key = m.Key,
                    Type = m.Type,
                    Url = $"{_publicBaseUrl}{m.Key}"
                }).ToList(),
            })
            .ToList();
    }
}
