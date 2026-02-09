using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auctera.Shared.Domain.Exceptions;

public class Error : System.Exception
{
    public int StatusCode { get; }

    public Error(string message, int statusCode = 400) : base(message)
    {
        StatusCode = statusCode;
    }
}
