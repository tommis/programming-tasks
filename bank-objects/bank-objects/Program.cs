using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace bank_objects
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Bank opBank = new Bank("OKOYOFIHH", "osuuspankki");

            Person asiakas = new Person("123D", "Tommi", "1");
            Person asiakas2 = new Person("199S", "Tupu", "2");
            Person asiakas3 = new Person("245M", "Lupu", "3");

            opBank.CreateAccount("Käyttötili", "123D");
            opBank.CreateAccount("Using account", "199S");
            opBank.CreateAccount("Säästö Tili", "199S");
            opBank.CreateAccount("Investments", "245M");

            Random r = new Random();
            opBank.Accounts.ForEach(delegate (Account a) {
                Transaction trans = new Transaction(a.AccountNumber, opBank.Accounts[r.Next(opBank.Accounts.Count)].AccountNumber, r.Next(5, 250), "euro");
                a.Transactions.Add(trans);
            });
            Console.WriteLine(asiakas.ToString());

            Console.ReadLine();
        }
    }
}
