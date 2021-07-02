using System;
using System.Collections.Generic;
using System.Text;
using CnpValidatorRemake;

namespace CnpValidatorRemake
{
    class AllDigitsValidator : ICnpValidator
    {
        public bool IsValid(string inputCnp)
        {
            return long.TryParse(inputCnp, out _);
        }
    }
}
