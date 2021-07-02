using System;
using System.Collections.Generic;
using System.Text;
using CnpValidatorRemake;

namespace CnpValidatorRemake
{
    class LengthValidator : ICnpValidator
    {
        public bool IsValid(string inputCnp)
        {
            return inputCnp.Length == 13;
        }
    }
}
