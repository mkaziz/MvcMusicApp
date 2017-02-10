using StringParse.BusObj;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringParser.Console
{
    public class Program
    {
        static void Main(string[] args)
        {
            string str = "(id,created,employee(id,firstname,employeeType(id), lastname),location(id,lat,long),something,anything)";
            System.Console.WriteLine(StringContainer.Get(str).ToDeepString());
            System.Console.ReadKey();
        }
    }
}
