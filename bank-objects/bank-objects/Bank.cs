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

        private List<Account> _accounts;


        public Bank(string bic, string name = "")
        {
            this._bic = bic;
            this._name = name;

            this._accounts = new List<Account>();
        }
        
        
        public List<Account> Accounts {
            get { return _accounts; }
        }

        public void AddTransaction(string accountNumber, Transaction trans)
        {
            Account fromAccount = _accounts.Single(x => x.AccountNumber == accountNumber);
            fromAccount.Transactions.Add(trans);
            fromAccount.Balance = fromAccount.Balance - trans.Amount;

            Account toAccount = _accounts.Single(x => x.AccountNumber == trans.AccountNumberTo);
            trans.Operation = "plus";
            fromAccount.Transactions.Add(trans);
            toAccount.Balance = toAccount.Balance + trans.Amount;
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
                    a.Transactions.FindAll(t => DateTime.Compare(t.Date, from) >= 0 && DateTime.Compare(t.Date, to) < 0).ToList());

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
            return _accounts.Single(x => x.AccountNumber == accountNumber).Balance;
        }

        public void CreateAccount(string name, string owner)
        {
            _accounts.Add(new Account(Utils.GenAccountNumber(), name, owner));
        }

    }
}
