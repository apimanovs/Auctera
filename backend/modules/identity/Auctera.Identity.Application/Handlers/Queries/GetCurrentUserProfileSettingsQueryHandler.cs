using Auctera.Identity.Application.Interfaces;
using Auctera.Identity.Application.Models;
using Auctera.Identity.Application.Queries;

using MediatR;

namespace Auctera.Identity.Application.Handlers.Queries;

public sealed class GetCurrentUserProfileSettingsQueryHandler
    : IRequestHandler<GetCurrentUserProfileSettingsQuery, ProfileSettingsDto>
{
    private readonly IUserRepository _userRepository;

    public GetCurrentUserProfileSettingsQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<ProfileSettingsDto> Handle(GetCurrentUserProfileSettingsQuery request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetUserByIdAsync(request.UserId);

        if (user is null)
        {
            throw new UnauthorizedAccessException("No user is currently authenticated.");
        }

        return new ProfileSettingsDto
        {
            Id = user.Id,
            Name = user.Name,
            Username = user.UserName,
            Email = user.Email,
            City = user.City,
            Country = user.Country
        };
    }
}
