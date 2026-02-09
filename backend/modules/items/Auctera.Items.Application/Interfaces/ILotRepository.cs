using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Auctera.Items.Domain;

namespace Auctera.Items.Application.Interfaces;
public interface ILotRepository
{
    Task<Lot?> GetLotById(Guid id, CancellationToken cancellationToken);
    Task SaveLotAsync(Lot lot, CancellationToken cancellationToken);
    Task AddLotAsync(Lot lot, CancellationToken cancellationToken);
}
