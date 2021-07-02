using System;
using System.Collections.Generic;
using System.Text;
using CnpValidatorRemake;

namespace CnpValidatorRemake
{
    class ControlDigitValidator : ICnpValidator
    {
        private static readonly string controlConstant = "279146358279";
        public bool IsValid(string inputCnp)
        {
            int controlDigit = int.Parse(inputCnp[12].ToString());
            int trueControlDigit = GetControlDigit(inputCnp);

            return controlDigit == trueControlDigit;
        }

        public int GetControlDigit(string inputCnp)
        {
            int trueControlDigit = 0;
            for (int i = 0; i < 12; i++)
            {
                trueControlDigit += int.Parse(controlConstant[i].ToString()) 
                    * int.Parse(inputCnp[i].ToString());
            }
            trueControlDigit %= 11;
            trueControlDigit = trueControlDigit == 10 ? 1 : trueControlDigit;

            return trueControlDigit;
        }
    }
}
