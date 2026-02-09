using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Auctera.Items.Application.Commands;

public record PublishLotCommand(Guid LotId, Guid SellerId) : IRequest<Guid>;
