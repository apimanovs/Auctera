using Auctera.Identity.Application.Commands;
using Auctera.Identity.Application.Interfaces;
using Auctera.Identity.Application.Models;

using MediatR;

namespace Auctera.Identity.Application.Handlers;

public sealed class UpdateProfileSettingsCommandHandler
    : IRequestHandler<UpdateProfileSettingsCommand, ProfileSettingsDto>
{
    private readonly IUserRepository _userRepository;

    public UpdateProfileSettingsCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<ProfileSettingsDto> Handle(UpdateProfileSettingsCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetUserByIdAsync(request.UserId);

        if (user is null)
        {
            throw new UnauthorizedAccessException("No user is currently authenticated.");
        }

        var existingUserWithUsername = await _userRepository.GetUserByUsernameAsync(request.Username.Trim());

        if (existingUserWithUsername is not null && existingUserWithUsername.Id != user.Id)
        {
            throw new ArgumentException("Username is already taken.");
        }

        user.UpdateProfile(request.Name, request.Username, request.City, request.Country);
        await _userRepository.UpdateUserAsync(user);

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
