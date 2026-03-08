using Auctera.Identity.Application.Commands;
using Auctera.Identity.Application.Interfaces;

using MediatR;

namespace Auctera.Identity.Application.Handlers;

/// <summary>
/// Represents the login command handler class.
/// </summary>
public class LoginCommandHandler(
    ITokenProvider tokenProvider,
    IUserRepository userRepository,
    IPasswordHasher passwordHasher) : IRequestHandler<LoginCommand, string>
{
    private readonly ITokenProvider _tokenProvider = tokenProvider;
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IPasswordHasher _passwordHasher = passwordHasher;

    /// <summary>
    /// Handles the operation.
    /// </summary>
    /// <param name="request">Input data for the operation.</param>
    /// <param name="cancellationToken">Cancellation token for the operation.</param>
    /// <returns>A task that returns the operation result.</returns>
    public async Task<string> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetUserByEmailAsync(request.email);

        if (user is null)
        {
            throw new UnauthorizedAccessException("User is not registred");
        }

        if (!_passwordHasher.VerifyPassword(user.PasswordHash, request.password))
        {
            throw new UnauthorizedAccessException("Invalid password");
        }

        return _tokenProvider.Generate(user);
    }
}
