using Auctera.Identity.Application.Queries;
using Auctera.Identity.Application.Models;
using MediatR;

using Auctera.Identity.Application.Interfaces;
using Auctera.Persistance;

namespace Auctera.Identity.Application.Handlers.Queries;

public sealed class GetUserProfileQueryHandler : IRequestHandler<GetUserProfileQuery, UserProfileDto>
{
    private readonly IUserRepository _userRepository;
    private readonly AucteraDbContext _aucteraDbContext;

    public GetUserProfileQueryHandler(IUserRepository userRepository, AucteraDbContext aucteraDbContext)
    {
        _userRepository = userRepository;
        _aucteraDbContext = aucteraDbContext;
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
            Stats = new UserProfileStatsDto
            {
                BidsPlaced = await _userRepository.GetUsersBidsPlacedCount(user.Id),
                ActiveListingsCount = await _userRepository.GetUserActiveLotsCountAsync(user.Id),
                SoldItemsCount = await _userRepository.GetUserSoldLotsCountAsync(user.Id)
            },
            ActiveListings = await _userRepository.GetUserActiveLotsAsync(user.Id),
            SoldListings = await _userRepository.GetUserSoldLotsAsync(user.Id)
        };
    }
}
