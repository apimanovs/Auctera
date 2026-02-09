using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MediatR;
using Auctera.Auctions.Application.Models;

namespace Auctera.Auctions.Application.Queries;
public sealed record GetCurrentPriceQuery(Guid AuctionId) : IRequest<CurrentPriceDto>;
