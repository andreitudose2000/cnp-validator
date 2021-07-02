using System;
using System.Collections.Generic;
using System.Text;

namespace CnpValidator
{
    class CnpException : Exception
    {
        public CnpException(string message) : base(message) { }
    }
}
