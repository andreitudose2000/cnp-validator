using System;
using System.Collections.Generic;
using System.Text;
using CnpValidatorRemake;

namespace CnpValidatorRemake
{
    class YearValidator : ICnpValidator
    {
        public bool IsValid(string inputCnp)
        {
            int genderDigit = int.Parse(inputCnp.Substring(0, 1));
            int yearDigits = int.Parse(inputCnp.Substring(1, 2));

            if (genderDigit == 5 || genderDigit == 6)
            {
                return 2000 + yearDigits <= DateTime.Now.Year;
            }
            return true;
        }

        public int? GetYear(string inputCnp)
        {
            if (!IsValid(inputCnp))
            {
                return null;
            }

            int genderDigit = int.Parse(inputCnp.Substring(0, 1));
            int yearDigits = int.Parse(inputCnp.Substring(1, 2));
            int birthYear;

            if (genderDigit == 3 || genderDigit == 4)
            {
                birthYear = 1800 + yearDigits;
            }
            else if (genderDigit == 1 || genderDigit == 2)
            {
                birthYear = 1900 + yearDigits;
            }
            else if (genderDigit == 5 || genderDigit == 6)
            {
                birthYear = 2000 + yearDigits;
            }
            else
            {
                birthYear = yearDigits;
            }

            return birthYear;
        }
    }
}
