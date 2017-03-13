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

            opBank.Accounts.ForEach(delegate (Account a) {
                for (int i = 0; i < 10; i++) {
                    Transaction trans = new Transaction(a.AccountNumber, opBank.Accounts[Utils.Rnd.Next(opBank.Accounts.Count)].AccountNumber, Utils.Rnd.Next(5, 250), "euro", "minus", Utils.RandomDay());

                    opBank.AddTransaction(a.AccountNumber, trans);
                }
            });
            
            Console.WriteLine(opBank.GetTransActionsFor(opBank.Accounts[0].AccountNumber).Count());
            Console.WriteLine(opBank.GetAccountBalance(opBank.Accounts[0].AccountNumber));

            Console.WriteLine(asiakas.ToString());

            Console.WriteLine(opBank.GetTransActionsForInRange(opBank.Accounts[1].AccountNumber, new DateTime(2000, 1, 1), new DateTime(2005, 1, 1)).Count());

            Console.ReadLine();
        }
    }
}
