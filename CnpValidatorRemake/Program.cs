using System;
using System.Collections.Generic;

namespace CnpValidatorRemake
{
    class Program
    {
        static void Main(string[] args)
        {
            var validatorList = new List<ICnpValidator>
            {
                new LengthValidator(),
                new AllDigitsValidator(),
                new GenderValidator(),
                new YearValidator(),
                new MonthValidator(),
                new DayValidator(),
                new CountyValidator(),
                new SerialValidator(),
                new ControlDigitValidator()
            };

            string cnpInput = Console.ReadLine().Trim();

            foreach (var validator in validatorList)
            {
                if (!validator.IsValid(cnpInput))
                {
                    Console.WriteLine("Invalid!");
                    //Console.WriteLine(validator.GetType().Name);
                    return;
                }
            }
            Console.WriteLine("Valid!");
        }
    }
}
