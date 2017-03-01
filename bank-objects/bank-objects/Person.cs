using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bank_objects
{
    public class Person
    {
        private string _accountNumber;

        private string _socialSecurityId;
        private string _name;



        public Person(string accountNumber, string socialSecurityId, string name)
        {
            this._accountNumber = accountNumber;

            this._socialSecurityId = socialSecurityId;
            this._name = name;

        }

        public override string ToString()
        {
            return string.Format("Account Number: {0} \nAccount name: {1} \nSSI: {2}",
                _accountNumber, _socialSecurityId, _name);
        }
    }
}
