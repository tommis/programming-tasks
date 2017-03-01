using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bank_objects
{
    public class Transaction
    {
        private string _accountNumberFrom;
        private string _accountNumberTo;
        private decimal _amount;
        private string _currency;
        private DateTime _dateTime;


        public Transaction(string accountNumberFrom, string accountNumberTo, decimal amount, string currency, DateTime? date = null)
        {
            this._accountNumberFrom = accountNumberFrom;
            this._accountNumberTo = accountNumberTo;
            this._amount = amount;
            this._currency = currency;
            this._dateTime = date ?? DateTime.UtcNow;
        }

        public DateTime Date
        {
            get { return _dateTime; }
            set { _dateTime = value; }
        }

    }
}
