using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloWorld.BusObj
{
    public abstract class BaseWriter : IWriter
    {
        public abstract void Write(string str);

        public static T Get<T>() where T: IWriter, new()
        {
            return new T();
        }
    }
}
