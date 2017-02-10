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
            var container = StringContainer.Get(str);
            container.Sort();
            System.Console.WriteLine(container.ToDeepString());
            System.Console.ReadKey();
        }
    }
}
