using HelloWorld.BusObj;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloWorld
{
    class Program
    {
        static void Main(string[] args)
        {
            var writer = BaseWriter.Get<ConsoleWriter>();
            writer.Write("Hello World");
        }
    }
}
