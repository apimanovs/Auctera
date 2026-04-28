using Auctera.Identity.Application.Queries;
using Auctera.Identity.Application.Models;
using MediatR;

using Auctera.Identity.Application.Interfaces;
using Auctera.Persistance;
using Auctera.Shared.Infrastructure.Media;

using Microsoft.Extensions.Options;

namespace Auctera.Identity.Application.Handlers.Queries;

public sealed class GetUserProfileQueryHandler : IRequestHandler<GetUserProfileQuery, UserProfileDto>
{
    private readonly IUserRepository _userRepository;
    private readonly AucteraDbContext _aucteraDbContext;
    private readonly string _publicBaseUrl;

    public GetUserProfileQueryHandler(
        IUserRepository userRepository,
        AucteraDbContext aucteraDbContext,
        IOptions<MediaOptions> mediaOptions)
    {
        _userRepository = userRepository;
        _aucteraDbContext = aucteraDbContext;
        _publicBaseUrl = mediaOptions.Value.PublicBaseUrl;
    }

    public async Task<UserProfileDto> Handle(GetUserProfileQuery request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetUserByUsernameAsync(request.username);

        if (user is null)
        {
            throw new KeyNotFoundException($"User with ID {request.username} not found.");
        }

        return new UserProfileDto
        {
            Id = user.Id,
            Username = user.UserName,
            Name = user.Name,
            City = user.City,
            Country = user.Country,
            Stats = new UserProfileStatsDto
            {
                BidsPlaced = await _userRepository.GetUsersBidsPlacedCount(user.Id),
                ActiveListingsCount = await _userRepository.GetUserActiveLotsCountAsync(user.Id),
                SoldItemsCount = await _userRepository.GetUserSoldLotsCountAsync(user.Id)
            },
            ActiveListings = AddPublicUrls(await _userRepository.GetUserActiveLotsAsync(user.Id)),
            SoldListings = AddPublicUrls(await _userRepository.GetUserSoldLotsAsync(user.Id))
        };
    }

    private List<UserProfileListingDto> AddPublicUrls(List<UserProfileListingDto> listings)
    {
        foreach (var listing in listings)
        {
            if (!string.IsNullOrWhiteSpace(listing.ThumbnailUrl) &&
                !listing.ThumbnailUrl.StartsWith("http", StringComparison.OrdinalIgnoreCase))
            {
                listing.ThumbnailUrl = $"{_publicBaseUrl}{listing.ThumbnailUrl}";
            }
        }

        return listings;
    }
}
