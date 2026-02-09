using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Auctera.Shared.Domain.Time;

namespace Auctera.Shared.Infrastructure.Time;
public class SystemClock : IClock
{
    public DateTime UtcNow => DateTime.UtcNow;
}
