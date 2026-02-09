using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MediatR;

namespace Auctera.Auctions.Application.Commands;

public sealed record CreateAuctionCommand (Guid LotId, DateTime EndsAt) : IRequest<Guid>;
