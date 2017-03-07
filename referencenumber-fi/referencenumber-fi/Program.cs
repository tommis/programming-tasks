using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace referencenumber_fi
{
    class Program
    {

        static void Main(string[] args)
        {
            string refnumber = "12345672";

            Console.WriteLine(Validation.Validate(refnumber, true));
            Console.WriteLine(Validation.GenRefSums(10));

            Console.ReadLine();
        }
    }
}
