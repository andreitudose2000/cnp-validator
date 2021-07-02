using System;
using System.Collections.Generic;
using System.Text;
using CnpValidatorRemake;

namespace CnpValidatorRemake
{
    public enum Gender
    {
        Male,
        Female
    }
    class GenderValidator : ICnpValidator
    {
        //todo
        public bool IsValid(string inputCnp)
        {
            int genderDigit = int.Parse(inputCnp[0].ToString());
            return genderDigit != 0 && genderDigit != 9;
        }

        public Gender? GetGender(string inputCnp)
        {
            if (!IsValid(inputCnp))
            {
                return null;
            }
            if (int.Parse(inputCnp[0].ToString()) % 2 == 0)
            {
                return Gender.Female;
            }
            return Gender.Male;
        }
    }
}
