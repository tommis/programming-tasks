using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace iban2bban
{
    public class Program
    {
        public static bool bbanValidate(string input)
        {
            var rBban = new Regex("[0-9]{6}-?[0-9]{2,8}");
            return rBban.IsMatch(input);
        }
        public static string convertToIban(string input)
        {
            string iban = input.Insert(input.Substring(0, 1).IndexOfAny("45".ToCharArray()) == -1 ? 6 : 7, "".PadLeft(14 - input.Length, Convert.ToChar("0")));

            iban =  "FI" + genChecksum(iban + "1518" + "00") + iban;

            return iban;
        }
        public static string genChecksum(string input)
        {
            decimal sum = (98 - (Convert.ToDecimal(input) % 97));
            return sum < 10 ? "0" + Convert.ToString(sum) : Convert.ToString(sum);
        }
        public static void Main(string[] args)
        {
            Console.Write("Bban: ");
            //string bban = Console.ReadLine();
            string bban = "512345-678";

            string bbanMachine = bban.Replace(" ", "").Replace("-", "");

            if (bbanValidate(bbanMachine))
            {
                string iban = convertToIban(bbanMachine);

                Console.WriteLine("\n" + iban);
            }
            else
                return;

            Console.ReadLine();
        }
    }
}
