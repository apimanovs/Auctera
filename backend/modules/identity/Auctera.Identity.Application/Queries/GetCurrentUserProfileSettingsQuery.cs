using Auctera.Identity.Application.Models;

using MediatR;

namespace Auctera.Identity.Application.Queries;

public sealed record GetCurrentUserProfileSettingsQuery(Guid UserId) : IRequest<ProfileSettingsDto>;
