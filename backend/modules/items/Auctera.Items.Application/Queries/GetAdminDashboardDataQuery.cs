using Auctera.Items.Application.Models;
using MediatR;

namespace Auctera.Items.Application.Queries;

public sealed record GetAdminDashboardDataQuery : IRequest<AdminDashboardDto>;
