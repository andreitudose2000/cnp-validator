using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace CnpValidator
{
    public sealed class CnpValidator
    {
        private static Dictionary<int, (string name, int noOfDays)> monthInfo;

        private readonly static string controlConstant;

        public static void Validate(string inputCnp)
        {
            monthInfo = GetMonthInfo();

            ValidateLength(inputCnp);

            ValidateDigitsOnly(inputCnp);

            int genderDigit = int.Parse(inputCnp.Substring(0, 1));
            string userGender = ValidateGender(genderDigit);

            int yearDigits = int.Parse(inputCnp.Substring(1, 2));
            int userBirthYear = validateYear(yearDigits, genderDigit);

            int monthDigits = int.Parse(inputCnp.Substring(3, 2));
            string userBirthMonth = ValidateMonth(monthDigits);

            int dayDigits = int.Parse(inputCnp.Substring(5, 2));
            int userBirthDay = ValidateDay(dayDigits, monthDigits, userBirthYear);

            int countyDigits = int.Parse(inputCnp.Substring(7, 2));
            string userCounty = ValidateCounty(countyDigits);

            int serialDigits = int.Parse(inputCnp.Substring(9, 3));
            ValidateSerial(serialDigits);

            int controlDigit = int.Parse(inputCnp.Substring(12, 1));
            ValidateControl(inputCnp, controlDigit);

            WriteOutput(userGender, userBirthDay, userBirthMonth, userBirthYear, userCounty);
        }

        private static void ValidateLength(string inputCnp)
        {
            if (inputCnp.Length != 13)
            {
                throw new CnpException("Incorrect length");
            }
        }

        private static void ValidateDigitsOnly(string inputCnp)
        {
            foreach (char digit in inputCnp)
            {
                if (!int.TryParse(digit.ToString(), out int temp))
                {
                    throw new CnpException("Can't contain non-digit characters");
                }
            }
        }

        private static string ValidateGender(int genderDigit)
        {
            if (genderDigit == 0 || genderDigit == 9)
            {
                throw new CnpException("First digit of CNP is wrong");
            }
            return genderDigit % 2 == 0 ? "Female" : "Male";
        }

        private static int validateYear(int yearDigits, int genderDigit)
        {
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

            if (birthYear > DateTime.Now.Year)
            {
                throw new CnpException("The person can't be born in the future");
            }

            return birthYear;
            
        }

        public static string ValidateMonth(int monthDigits)
        {
            if (!monthInfo.ContainsKey(monthDigits))
            {
                throw new CnpException("Month incorrect");
            }
            return monthInfo[monthDigits].name;
        }

        public static int ValidateDay(int dayDigits, int monthDigits, int userBirthYear)
        {
            if (monthDigits == 2 && dayDigits == 29 && !DateTime.IsLeapYear(userBirthYear))
            {
                throw new CnpException("Can't have February 29 in non-leap year");
            }

            if (dayDigits < 1 || dayDigits > monthInfo[monthDigits].noOfDays)
            {
                throw new CnpException("Day of month incorrect");
            }

            return dayDigits;
        }

        private static string ValidateCounty(int countyDigits)
        {
            var countyCodes = GetCountyCodes();
            if (!countyCodes.ContainsKey(countyDigits))
            {
                throw new CnpException("County code incorrect");
            }
            return countyCodes[countyDigits];
        }

        private static void ValidateSerial(int serialDigits)
        {
            if (serialDigits == 0)
            {
                throw new CnpException("Serial number has to be in range 001..999");
            }
        }

        private static void ValidateControl(string inputCnp, int controlDigit)
        {
            int trueControlDigit = 0;
            for (int i = 0; i < 12; i++)
            {
                trueControlDigit += int.Parse(controlConstant[i].ToString()) * int.Parse(inputCnp[i].ToString());
            }
            trueControlDigit %= 11;
            trueControlDigit = trueControlDigit == 10 ? 1 : trueControlDigit;
            
            if (controlDigit != trueControlDigit)
            {
                throw new CnpException("Control digit incorrect");
            }
        }

        private static Dictionary<int, (string name, int noOfDays)> GetMonthInfo()
        {
            var monthInfo = new Dictionary<int, (string name, int noOfDays)>();

            string path = "E:\\Cod\\Playground\\CnpValidator\\months.txt";
            using (var sr = File.OpenText(path))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    var split = line.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    monthInfo.Add(int.Parse(split[0]), (split[1], int.Parse(split[2])) );
                }
            }

            return monthInfo;
        }

        private static Dictionary<int, string> GetCountyCodes()
        {
            var countryCodes = new Dictionary<int, string>();

            string path = "E:\\Cod\\Playground\\CnpValidator\\counties.txt";
            using (var sr = File.OpenText(path))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    var split = line.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    countryCodes.Add(int.Parse(split[0]), split[1]);
                }
            }

            return countryCodes;
        }

        public static void WriteOutput(string userGender, int userBirthDay, string userBirthMonth, int userBirthYear, string userCounty)
        {
            Console.WriteLine($" Gender: {userGender} \n Birthday: {userBirthDay} {userBirthMonth} {userBirthYear} \n County: {userCounty}");
        }
    }
}
