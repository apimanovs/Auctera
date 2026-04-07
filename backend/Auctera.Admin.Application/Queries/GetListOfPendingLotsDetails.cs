using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Auctera.Admin.Domain.Models;

using MediatR;

namespace Auctera.Admin.Application.Queries;

public sealed record GetListOfPendingLotsDetails(Guid adminId) : IRequest<IReadOnlyList<PendingLotPreviewDetailsDto>>;
