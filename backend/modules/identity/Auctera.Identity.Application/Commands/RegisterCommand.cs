using System.ComponentModel.DataAnnotations;
using MediatR;

namespace Auctera.Identity.Application.Commands;

public record RegisterCommand
(
    [property: Required]
    [property: MinLength(3)]
    [property: MaxLength(50)]
    string username,

    [property: Required]
    [property: EmailAddress]
    [property: MaxLength(256)]
    string email,

    [property: Required]
    [property: MinLength(8)]
    [property: MaxLength(128)]
    string password,

    [property: Required]
    [property: MinLength(8)]
    [property: MaxLength(128)]
    string confirmPassword
) : IRequest<string>;
