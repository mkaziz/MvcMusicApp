using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HelloWorld.BusObj;
using HelloWorld.Database;

namespace HelloWorld.UnitTest
{
    [TestClass]
    public class UnitTest1
    {
        // Test to ensure the GET Factory Method works 

        [TestMethod]
        public void FactoryMethodGetsDatabaseWriter()
        {
            var writer = BaseWriter.Get<DatabaseWriter>();
            Assert.IsTrue(writer is DatabaseWriter);
        }

        [TestMethod]
        public void FactoryMethodGetsConsoleWriter()
        {
            var writer = BaseWriter.Get<ConsoleWriter>();
            Assert.IsTrue(writer is ConsoleWriter);
        }

    }
}
