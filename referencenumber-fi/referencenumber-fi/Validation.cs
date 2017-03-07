using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace referencenumber_fi
{
    class Validation
    {
        public static readonly Random _r = new Random();
        private static readonly object _syncLock = new object();

        public static Regex _RrefNumber = new Regex("[0-9]{4,20}");

        // <summary>
        // 
        // </summary>
        public static bool Validate(string refNumber, bool includesChecksum = false)
        {
            string basePart = refNumber.Substring(0, refNumber.Length - 1);
            string checkSum = refNumber.Substring(refNumber.Length - 1, 1);

            if (GenChecksum(basePart) == Convert.ToInt16(checkSum) && _RrefNumber.IsMatch(refNumber) && includesChecksum || 
                (_RrefNumber.IsMatch(refNumber) && !includesChecksum))
                return true;
            return false;
        }
        public static string ConvertToRef(string basePart)
        {
            if (!Validate(basePart, false))
                throw new ArgumentException();
            return basePart + GenChecksum(basePart);
            
        }
        public static int GenChecksum(string input) {

            List<int> res = new List<int>();
            int[] rev =
                  Array.ConvertAll(input.ToCharArray().Reverse().ToArray(), c => (int)Char.GetNumericValue(c));

            for (int i = 1; i <= rev.Count(); ++i)
            {
                int a = rev[i - 1];
                int x = a % 7 == 0 ?
                    a * 1 : a % 3 == 0 ?
                    a * 3 : a % 1 == 0 ? a * 7 : 0;

                res.Add(x);
            }

            int roundUp = (res.Sum() - res.Sum() % 10) + 10;

            int fres = res.Sum() != 10 ? roundUp - res.Sum() : 0;
            return fres;
        }
        public static List<string> GenRefSums(int times)
        {
            List<string> res = new List<string>();
            string range = "1234567890";

            for (int i = 0; i < times; i++)
                lock (_syncLock)
                {
                    string refNumber = ConvertToRef(Convert.ToString(Enumerable.Range(0, 10)
                                           .Select(x => range[_r.Next(0, range.Length)])));
                    res.Add(refNumber);
                }     
           

            return res;
        }
    }
}
