using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MediatR;

using Auctera.Identity.Application.Commands;
using Auctera.Identity.Domain;
using Auctera.Identity.Application.Interfaces;

namespace Auctera.Identity.Application.Handlers;
/// <summary>
/// Represents the register command handler class.
/// </summary>
public sealed class RegisterCommandHandler(ITokenProvider tokenProvider,
    IPasswordHasher passwordHasher, IUserRepository userRepository) : IRequestHandler<RegisterCommand, string>
{
    private readonly ITokenProvider _tokenProvider = tokenProvider;
    private readonly IPasswordHasher _passwordHasher = passwordHasher;
    private readonly IUserRepository _userRepository = userRepository;

    /// <summary>
    /// Handles the operation.
    /// </summary>
    /// <param name="request">Input data for the operation.</param>
    /// <param name="cancellationToken">Cancellation token for the operation.</param>
    /// <returns>A task that returns the operation result.</returns>
    public async Task<string> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(request.email))
        {
            throw new ArgumentException("Email is required");
        }

        if (string.IsNullOrEmpty(request.username))
        {
            throw new ArgumentException("Email is required");
        }

        if (string.IsNullOrEmpty(request.password))
        {
            throw new ArgumentException("Email is required");
        }

        if (string.IsNullOrEmpty(request.confirmPassword))
        {
            throw new ArgumentException("Email is required");
        }


        if (request.password != request.confirmPassword)
        {
            throw new ArgumentException("Passwords do not match");
        }

        var hashedPassword = _passwordHasher.HashPassword(request.password);

        var user = User.Create(request.username, request.email, hashedPassword, request.username);

        await _userRepository.AddUserAsync(user);

        return _tokenProvider.Generate(user);
    }
}
