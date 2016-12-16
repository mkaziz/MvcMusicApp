using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloWorld.BusObj
{
    public class ConsoleWriter : BaseWriter
    {
        public override void Write(string str)
        {
            Console.WriteLine(str);
            Console.ReadKey(); // to prevent screen from shutting down
        }
    }
}
