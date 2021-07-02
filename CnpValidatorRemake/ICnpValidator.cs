using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CnpValidatorRemake
{
    interface ICnpValidator
    {
        bool IsValid(string inputCnp);

        public static Dictionary<int, (string name, int noOfDays)> GetMonthInfo()
        {
            var monthInfo = new Dictionary<int, (string name, int noOfDays)>();

            string path = "E:\\Cod\\Playground\\CnpValidator\\months.txt";
            using (var sr = File.OpenText(path))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    var split = line.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    monthInfo.Add(int.Parse(split[0]), (split[1], int.Parse(split[2])));
                }
            }

            return monthInfo;
        }
    }
}
