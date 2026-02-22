using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auctera.Identity.Application.Interfaces;

public interface ITokenProvider
{
    string Generate(Domain.User user);
}
