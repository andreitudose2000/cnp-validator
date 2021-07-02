using System;
using System.Collections.Generic;
using System.Text;
using CnpValidatorRemake;

namespace CnpValidatorRemake
{
    class SerialValidator : ICnpValidator
    {
        public bool IsValid(string inputCnp)
        {
            int serialDigits = int.Parse(inputCnp.Substring(9, 3));
            return serialDigits != 0;
        }
    }
}
