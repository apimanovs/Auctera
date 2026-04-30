using Auctera.Admin.Domain.Models;

using MediatR;

namespace Auctera.Admin.Application.Queries;

public sealed record GetAdminDashboardData() : IRequest<AdminDashboardDto>;
