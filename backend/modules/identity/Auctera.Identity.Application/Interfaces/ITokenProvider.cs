using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auctera.Identity.Application.Interfaces;

/// <summary>
/// Represents the i token provider interface.
/// </summary>
public interface ITokenProvider
{
    string Generate(Domain.User user);
}
