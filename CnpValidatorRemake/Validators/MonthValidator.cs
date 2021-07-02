using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using CnpValidatorRemake;

namespace CnpValidatorRemake
{
    class MonthValidator : ICnpValidator
    {
        private readonly Dictionary<int, (string name, int noOfDays)> monthInfo;
        public MonthValidator()
        {
            this.monthInfo = ICnpValidator.GetMonthInfo();
        }

        public bool IsValid(string inputCnp)
        {
            int monthDigits = int.Parse(inputCnp.Substring(3, 2));
            return monthInfo.ContainsKey(monthDigits);
        }

        public string GetMonth(string inputCnp)
        {
            int monthDigits = int.Parse(inputCnp.Substring(3, 2));
            return this.monthInfo[monthDigits].name;
        }
    }
}
