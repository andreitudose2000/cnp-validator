using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using CnpValidatorRemake;

namespace CnpValidatorRemake
{
    class CountyValidator : ICnpValidator
    {
        private Dictionary<int, string> countyCodes;
        public bool IsValid(string inputCnp)
        {
            int countyDigits = int.Parse(inputCnp.Substring(7, 2));
            this.countyCodes ??= GetCountyCodes();

            return countyCodes.ContainsKey(countyDigits);
        }

        public string GetCounty(string inputCnp)
        {
            int countyDigits = int.Parse(inputCnp.Substring(7, 2));
            if (!IsValid(inputCnp))
            {
                return null;
            }
            return this.countyCodes[countyDigits];
        }

        private Dictionary<int, string> GetCountyCodes()
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
    }
}
