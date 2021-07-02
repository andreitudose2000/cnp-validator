using System;

namespace CnpValidator
{
    class Program
    {
        static void Main(string[] args)
        {
            string cnpInput = Console.ReadLine().Trim();
            try
            {
                CnpValidator.Validate(cnpInput);
            }
            catch (CnpException e)
            {
                Console.WriteLine(e.Message);
            }

        }
    }
}
