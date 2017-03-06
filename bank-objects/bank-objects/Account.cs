using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bank_objects
{

    public class Account
    {
        private string _accountNumber;
        private string _accountName;
        private string _owner;
        private decimal _balance;

        public List<Transaction> _transactions;

        public Account(string accountNumber, string name, string owner, int startingMoney = 1000)
        {
            this._accountNumber = accountNumber;
            this._accountName = name;
            this._owner = owner;
            this._balance = startingMoney;

            this._transactions = new List<Transaction>();
        }
       
        public string AccountNumber
        {
            get { return _accountNumber; }
        }

        public string Name
        {
            get { return _accountName; }
            set { _accountName = value; }
        }

        public string Owner {
            get { return _owner; }
            set { _owner = value;  }
        }

        public decimal Balance {
            get { return _balance; }
            set { _balance = value;  }
        }

        public List<Transaction> Transactions {
            get { return _transactions; }
            private set { _transactions = value; }
        }


        /// <summary>
        /// Get all transacstions between dates from and to.
        /// </summary>
        public List<Transaction> GetTransActionsForInRange(string accountNumber, DateTime from, DateTime to)
        {
            try
            {
                List<Transaction> trans = new List<Transaction>(
                    _transactions.FindAll(t => DateTime.Compare(t.Date, from) >= 0 && DateTime.Compare(t.Date, to) < 0).ToList());

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
            return _balance;
        }
    }
}
