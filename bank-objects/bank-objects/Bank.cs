using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace bank_objects
{
    public class Bank
    {
        private string _bic;
        private string _name;

        public List<Account> _accounts;


        public Bank(string bic, string name = "")
        {
            this._bic = bic;
            this._name = name;

            this._accounts = new List<Account>();
        }

        
        public List<Account> Accounts {
            get { return _accounts; }
        }

        public List<Transaction> GetTransActionsFor(string accountNumber)
        {
            try
            {
                List<Transaction> trans = _accounts.Single(x => x.AccountNumber == accountNumber).Transactions;

                return trans;
            }
            catch (System.InvalidOperationException)
            {
                Console.WriteLine("Didn't find anything");

                return new List<Transaction>();
            }
        }

        /// <summary>
        /// Get all transacstions between dates from and to.
        /// </summary>
        public List<Transaction> GetTransActionsForInRange(string accountNumber, DateTime from, DateTime to)
        {
            try
            {
                Account a = _accounts.Single(x => x.AccountNumber == accountNumber);

                List<Transaction> trans = new List<Transaction>(
                    a._transactions.FindAll(t => DateTime.Compare(t.Date, from) >= 0 && DateTime.Compare(t.Date, to) < 0).ToList());

                return trans;
            }
            catch (System.InvalidOperationException)
            {
                Console.WriteLine("Didn't find anything");

                return new List<Transaction>();
            }
        }

        public decimal GetAccountBalance(string accountNumber)
        {
            try
            {
                Account account = _accounts.Single(x => x.AccountNumber == accountNumber);

                decimal balance = account.Balance;
                foreach (Transaction t in account.Transactions)
                {
                    balance = balance - t.Amount;
                }

                // get all received money
                foreach (Account a in _accounts)
                {
                    decimal amount = a.Transactions.Where(z => z.AccountNumberTo == accountNumber).Sum(s => s.Amount);
                    balance = balance + amount;
                }

                return balance;
            }
            catch(System.ArgumentOutOfRangeException)
            {
                return 0.00M;
            }
        }

        public void CreateAccount(string name, string owner)
        {
            _accounts.Add(new Account(Utils.GenAccountNumber(), name, owner));
        }

    }
}
