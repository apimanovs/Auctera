using Auctera.Identity.Application.Commands;
using Auctera.Identity.Application.Interfaces;
using Auctera.Identity.Application.Models;

using MediatR;

namespace Auctera.Identity.Application.Handlers;
public sealed class CurrentUserCommandHandler : IRequestHandler<CurrentUserCommand, UserDto>
{
    private readonly IUserRepository _userRepository;

    public CurrentUserCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<UserDto> Handle(CurrentUserCommand command, CancellationToken ct)
    {
        var user = await _userRepository.GetUserByIdAsync(command.userId);

        if (user is null)
        {
            throw new UnauthorizedAccessException("No user is currently authenticated.");
        }

        return new UserDto(user.Id, user.UserName, user.Email);
    }
}
