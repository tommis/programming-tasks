using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace bank_objects
{
    public class Utils
    {
        public static readonly Random Rnd = new Random();
        private static readonly object _syncLock = new object();

        private static string RandomString(int size, string range = "0123456789")
        {
            string input = range;

            lock (_syncLock)
            {
                var chars = Enumerable.Range(0, size)
                                       .Select(x => input[Rnd.Next(0, input.Length)]);
                return new string(chars.ToArray());
            }
        }
        public static bool BbanValidate(string input)
        {
            var rBban = new Regex("[0-9]{6}-?[0-9]{2,8}");
            return rBban.IsMatch(input);
        }

        public static string GenAccountNumber()
        {
            string bban = Utils.RandomString(12);
            string iban = bban.Insert(bban.Substring(0, 1).IndexOfAny("45".ToCharArray()) == -1 ? 6 : 7, "".PadLeft(14 - bban.Length, Convert.ToChar("0")));

            iban = "FI" + GenCheckSum(iban + "1518" + "00") + iban;

            return iban;
        }

        private static string GenCheckSum(string input)
        {
            decimal sum = (98 - (Convert.ToDecimal(input) % 97));
            return sum < 10 ? "0" + Convert.ToString(sum) : Convert.ToString(sum);
        }

        public static DateTime RandomDay()
        {
            DateTime startDate = new DateTime(2000, 1, 1);
            TimeSpan timeSpan = DateTime.UtcNow - startDate;
            TimeSpan newSpan = new TimeSpan(0, Rnd.Next(0, (int)timeSpan.TotalMinutes), 0);
            DateTime newDate = startDate + newSpan;

            return newDate;
        }
    }
}
