using HelloWorld.BusObj;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloWorld.Database
{
    class Program
    {
        static void Main(string[] args)
        {
            // to demonstrate reuse of business object and extensibility of base classes in a separate project
            var writer = BaseWriter.Get<DatabaseWriter>();
            writer.Write("Hello Database!"); 
        }
    }
}
