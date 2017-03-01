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
        private int _balance;

        public List<Transaction> Transactions;

        public Account(string accountNumber, string name, string owner, int startingMoney = 0)
        {
            this._accountNumber = accountNumber;
            this._accountName = name;
            this._owner = owner;
            this._balance = startingMoney;

            this.Transactions = new List<Transaction>();
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
    }
}
