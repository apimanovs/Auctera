using System.ComponentModel.DataAnnotations;
using MediatR;

namespace Auctera.Identity.Application.Commands;

public record LoginCommand(
    [property: Required]
    [property: EmailAddress]
    [property: MaxLength(256)]
    string email,

    [property: Required]
    [property: MinLength(8)]
    [property: MaxLength(128)]
    string password
) : IRequest<string>;
