using MediatR;

using Auctera.Identity.Application.Commands;
using Auctera.Identity.Application.Interfaces;

namespace Auctera.Identity.Application.Handlers;
public class LoginCommandHandler(ITokenProvider tokenProvider, IUserRepository userRepository,
    IPasswordHasher passwordHasher) : IRequestHandler<LoginCommand, string>
{
    private readonly ITokenProvider _tokenProvider = tokenProvider;
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IPasswordHasher _passwordHasher = passwordHasher;
    public Task<string> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var user = _userRepository.GetUserByEmailAsync(request.email).Result;

        if (user == null)
        {
            throw new UnauthorizedAccessException("User is not registred");
        }

        if (!_passwordHasher.VerifyPassword(request.password, user.PasswordHash))
        {
            throw new UnauthorizedAccessException("Invalid password");
        }

        return Task.FromResult(_tokenProvider.Generate(user));
    }
}
