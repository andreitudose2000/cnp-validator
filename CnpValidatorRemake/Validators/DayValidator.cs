using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using CnpValidatorRemake;

namespace CnpValidatorRemake
{
    class DayValidator : ICnpValidator
    {
        Dictionary<int, (string name, int noOfDays)> monthInfo;
        public DayValidator()
        {
            this.monthInfo = ICnpValidator.GetMonthInfo();
        }

        public bool IsValid(string inputCnp)
        {
            int dayDigits = int.Parse(inputCnp.Substring(5, 2));
            int monthDigits = int.Parse(inputCnp.Substring(3, 2));
            int? userBirthYear = new YearValidator().GetYear(inputCnp);

            if (dayDigits < 1 || dayDigits > monthInfo[monthDigits].noOfDays)
            {
                return false;
            }

            if (monthDigits == 02 && dayDigits == 29 
                && userBirthYear.HasValue
                && !DateTime.IsLeapYear(userBirthYear.Value))
            {
                return false;
            }

            return true;
        }

        public int? GetDay(string inputCnp)
        {
            if (!IsValid(inputCnp))
            {
                return null;
            }
            int dayDigits = int.Parse(inputCnp.Substring(5, 2));
            return dayDigits;
        }
    }
}
