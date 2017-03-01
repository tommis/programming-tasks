using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;


namespace bank_objects
{
    public class Bank
    {
        private string _bic;
        private string _name;

        public List<Account> Accounts;


        public Bank(string bic, string name = "")
        {
            this._bic = bic;
            this._name = name;

            this.Accounts = new List<Account>();
        }


        public List<Transaction> getTransActionsFor(string accountNumber)
        {
            try
            {
                return Accounts.Single(x => x.AccountNumber == accountNumber).Transactions;
            }
            catch (System.InvalidOperationException)
            {
                Console.WriteLine("Didn't find anything");

                return new List<Transaction>();
            }
        }
        public List<Transaction> getTransActionsForInRange(string accountNumber, DateTime from, DateTime to)
        {
            Account a = Accounts.Single(x => x.AccountNumber == accountNumber);

            List<Transaction> trans = new List<Transaction>(
                a.Transactions.FindAll(t => DateTime.Compare(t.Date, from) >= 0 && DateTime.Compare(t.Date, to) < 0));

            return trans;
        }

        public void CreateAccount(string name, string owner)
        {
            Accounts.Add(new Account(GenAccountNumber(), name, owner));
        }
        public static bool BbanValidate(string input)
        {
            var rBban = new Regex("[0-9]{6}-?[0-9]{2,8}");
            return rBban.IsMatch(input);
        }
        public static string GenAccountNumber()
        {
            string bban = RandomString(12);
            string iban = bban.Insert(bban.Substring(0, 1).IndexOfAny("45".ToCharArray()) == -1 ? 6 : 7, "".PadLeft(14 - bban.Length, Convert.ToChar("0")));

            iban = "FI" + GenCheckSum(iban + "1518" + "00") + iban;

            return iban;
        }
        public static string GenCheckSum(string input)
        {
            decimal sum = (98 - (Convert.ToDecimal(input) % 97));
            return sum < 10 ? "0" + Convert.ToString(sum) : Convert.ToString(sum);
        }
        private static readonly Random r = new Random();
        private static readonly object syncLock = new object();
        private static string RandomString(int Size, string range = "0123456789")
        {
            string input = range;

            lock (syncLock)
            {
                var chars = Enumerable.Range(0, Size)
                                       .Select(x => input[r.Next(0, input.Length)]);
                return new string(chars.ToArray());
            }
        }
    }
}
