using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StringParse.BusObj;

namespace StringParser.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestEmptyString()
        {
            var container = StringContainer.Get(string.Empty);
            Assert.AreEqual(container.Data, string.Empty);
            Assert.IsNull(container.Children);
        }
        [TestMethod]
        public void TestEmptyParens()
        {
            var container = StringContainer.Get("()");
            Assert.AreEqual(container.Data, string.Empty);
            Assert.AreEqual(container.Children[0].Data, string.Empty);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestMismatchedParents()
        {
            var container = StringContainer.Get(")(");
            Assert.AreEqual(container.Data, string.Empty);
            Assert.AreEqual(container.Children[0].Data, string.Empty);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestMismatchedParentsTooManyOpens()
        {
            var container = StringContainer.Get("(word(otherWord)");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestMismatchedParentsTooManyCloses()
        {
            var container = StringContainer.Get("(word(otherWord)))");
        }


        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestMismatchedParentsTooManyOpensNested()
        {
            var container = StringContainer.Get("(word(otherWord(Hello,Bye(Other))");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestMismatchedParentsTooManyClosesNested()
        {
            var container = StringContainer.Get("(word(otherWord(Hello,Bye(O)))ther))");
        }

        [TestMethod]
        public void TestOneChild()
        {
            var container = StringContainer.Get("(id)");
            Assert.AreEqual(container.Children[0].Data, "id");
            Assert.IsNull(container.Children[0].Children);
        }

        [TestMethod]
        public void TestTwoChildren()
        {
            var container = StringContainer.Get("(id,data)");
            Assert.AreEqual(container.Children[0].Data, "id");
            Assert.IsNull(container.Children[0].Children);
            Assert.AreEqual(container.Children[1].Data, "data");
            Assert.IsNull(container.Children[1].Children);
        }

        [TestMethod]
        public void TestThreeChildren()
        {
            var container = StringContainer.Get("(id,data,name)");
            Assert.AreEqual(container.Children[0].Data, "id");
            Assert.IsNull(container.Children[0].Children);
            Assert.AreEqual(container.Children[1].Data, "data");
            Assert.IsNull(container.Children[1].Children);
            Assert.AreEqual(container.Children[2].Data, "name");
            Assert.IsNull(container.Children[2].Children);
        }

        [TestMethod]
        public void TestOneChildWithGrandson()
        {
            var container = StringContainer.Get("(id(name))");
            Assert.AreEqual(container.Children[0].Data, "id");
            Assert.AreEqual(container.Children[0].Children[0].Data, "name");
            Assert.IsNull(container.Children[0].Children[0].Children);
        }

        [TestMethod]
        public void TestSecondChildWithGrandson()
        {
            var container = StringContainer.Get("(data,id(name))");
            Assert.AreEqual(container.Children[0].Data, "data");
            Assert.IsNull(container.Children[0].Children);

            Assert.AreEqual(container.Children[1].Data, "id");

            Assert.AreEqual(container.Children[1].Children[0].Data, "name");
            Assert.IsNull(container.Children[1].Children[0].Children);
        }

        [TestMethod]
        public void TestThirdChildWithGrandson()
        {
            var container = StringContainer.Get("(data,id,you(name))");
            Assert.AreEqual(container.Children[0].Data, "data");
            Assert.IsNull(container.Children[0].Children);

            Assert.AreEqual(container.Children[1].Data, "id");
            Assert.IsNull(container.Children[1].Children);

            Assert.AreEqual(container.Children[2].Data, "you");
            Assert.AreEqual(container.Children[2].Children[0].Data, "name");
            Assert.IsNull(container.Children[2].Children[0].Children);
        }

        [TestMethod]
        public void TestThirdChildWithGrandchildren()
        {
            var container = StringContainer.Get("(data,id,you(name,age))");
            Assert.AreEqual(container.Children[0].Data, "data");
            Assert.IsNull(container.Children[0].Children);

            Assert.AreEqual(container.Children[1].Data, "id");
            Assert.IsNull(container.Children[1].Children);

            Assert.AreEqual(container.Children[2].Data, "you");
            Assert.AreEqual(container.Children[2].Children[0].Data, "name");
            Assert.IsNull(container.Children[2].Children[0].Children);
            Assert.AreEqual(container.Children[2].Children[1].Data, "age");
            Assert.IsNull(container.Children[2].Children[1].Children);
        }

        [TestMethod]
        public void TestMiddleChildWithGrandchildren()
        {
            var container = StringContainer.Get("(data,id(name,age),you(name,age))");
            Assert.AreEqual(container.Children[0].Data, "data");
            Assert.IsNull(container.Children[0].Children);

            Assert.AreEqual(container.Children[1].Data, "id");
            Assert.AreEqual(container.Children[1].Children[0].Data, "name");
            Assert.IsNull(container.Children[1].Children[0].Children);
            Assert.AreEqual(container.Children[1].Children[1].Data, "age");
            Assert.IsNull(container.Children[1].Children[1].Children);

            Assert.AreEqual(container.Children[2].Data, "you");
            Assert.AreEqual(container.Children[2].Children[0].Data, "name");
            Assert.IsNull(container.Children[2].Children[0].Children);
            Assert.AreEqual(container.Children[2].Children[1].Data, "age");
            Assert.IsNull(container.Children[2].Children[1].Children);
        }

        [TestMethod]
        public void TestUltraNesting()
        {
            var container = StringContainer.Get("(data(name1,age1),id(name2,age2(hello,bye)),you(name3,age3))");
            Assert.AreEqual(container.Children[0].Data, "data");
            Assert.AreEqual(container.Children[0].Children[0].Data, "name1");
            Assert.IsNull(container.Children[0].Children[0].Children);
            Assert.AreEqual(container.Children[0].Children[1].Data, "age1");
            Assert.IsNull(container.Children[0].Children[1].Children);

            Assert.AreEqual(container.Children[1].Data, "id");
            Assert.AreEqual(container.Children[1].Children[0].Data, "name2");
            Assert.IsNull(container.Children[1].Children[0].Children);
            Assert.AreEqual(container.Children[1].Children[1].Data, "age2");
            Assert.AreEqual(container.Children[1].Children[1].Children[0].Data, "hello");
            Assert.IsNull(container.Children[1].Children[1].Children[0].Children);
            Assert.AreEqual(container.Children[1].Children[1].Children[1].Data, "bye");
            Assert.IsNull(container.Children[1].Children[1].Children[0].Children);

            Assert.AreEqual(container.Children[2].Data, "you");
            Assert.AreEqual(container.Children[2].Children[0].Data, "name3");
            Assert.IsNull(container.Children[2].Children[0].Children);
            Assert.AreEqual(container.Children[2].Children[1].Data, "age3");
            Assert.IsNull(container.Children[2].Children[1].Children);
        }

        [TestMethod]
        public void TestUltraNestingWithoutEnclosingParens()
        {
            var container = StringContainer.Get("data(name1,age1),id(name2,age2(hello,bye)),you(name3,age3)");
            Assert.AreEqual(container.Children[0].Data, "data");
            Assert.AreEqual(container.Children[0].Children[0].Data, "name1");
            Assert.IsNull(container.Children[0].Children[0].Children);
            Assert.AreEqual(container.Children[0].Children[1].Data, "age1");
            Assert.IsNull(container.Children[0].Children[1].Children);

            Assert.AreEqual(container.Children[1].Data, "id");
            Assert.AreEqual(container.Children[1].Children[0].Data, "name2");
            Assert.IsNull(container.Children[1].Children[0].Children);
            Assert.AreEqual(container.Children[1].Children[1].Data, "age2");
            Assert.AreEqual(container.Children[1].Children[1].Children[0].Data, "hello");
            Assert.IsNull(container.Children[1].Children[1].Children[0].Children);
            Assert.AreEqual(container.Children[1].Children[1].Children[1].Data, "bye");
            Assert.IsNull(container.Children[1].Children[1].Children[0].Children);

            Assert.AreEqual(container.Children[2].Data, "you");
            Assert.AreEqual(container.Children[2].Children[0].Data, "name3");
            Assert.IsNull(container.Children[2].Children[0].Children);
            Assert.AreEqual(container.Children[2].Children[1].Data, "age3");
            Assert.IsNull(container.Children[2].Children[1].Children);
        }

        [TestMethod]
        public void TestTrailingWords()
        {
            var container = StringContainer.Get("(data(name1,age1),id(name2,age2),you,name3,age3)");
            Assert.AreEqual(container.Children[0].Data, "data");
            Assert.AreEqual(container.Children[0].Children[0].Data, "name1");
            Assert.IsNull(container.Children[0].Children[0].Children);
            Assert.AreEqual(container.Children[0].Children[1].Data, "age1");
            Assert.IsNull(container.Children[0].Children[1].Children);

            Assert.AreEqual(container.Children[1].Data, "id");
            Assert.AreEqual(container.Children[1].Children[0].Data, "name2");
            Assert.IsNull(container.Children[1].Children[0].Children);
            Assert.AreEqual(container.Children[1].Children[1].Data, "age2");
            Assert.IsNull(container.Children[1].Children[1].Children);

            Assert.AreEqual(container.Children[2].Data, "you");
            Assert.AreEqual(container.Children[3].Data, "name3");
            Assert.IsNull(container.Children[3].Children);
            Assert.AreEqual(container.Children[4].Data, "age3");
            Assert.IsNull(container.Children[4].Children);
        }


        [TestMethod]
        public void TestTestString()
        {
            string str = "(id,created,employee(id,firstname,employeeType(id),lastname),location(id,lat,long),something,anything)";
            var container = StringContainer.Get(str);

            Assert.AreEqual(container.Children[0].Data, "id");
            Assert.IsNull(container.Children[0].Children);

            Assert.AreEqual(container.Children[1].Data, "created");
            Assert.IsNull(container.Children[1].Children);

            Assert.AreEqual(container.Children[2].Data, "employee");
            Assert.AreEqual(container.Children[2].Children[0].Data, "id");
            Assert.IsNull(container.Children[2].Children[0].Children);
            Assert.AreEqual(container.Children[2].Children[1].Data, "firstname");
            Assert.IsNull(container.Children[2].Children[1].Children);
            Assert.AreEqual(container.Children[2].Children[2].Data, "employeeType");
            Assert.AreEqual(container.Children[2].Children[2].Children[0].Data, "id");
            Assert.IsNull(container.Children[2].Children[2].Children[0].Children);
            Assert.AreEqual(container.Children[2].Children[3].Data, "lastname");
            Assert.IsNull(container.Children[2].Children[3].Children);

            Assert.AreEqual(container.Children[3].Data, "location");
            Assert.AreEqual(container.Children[3].Children[0].Data, "id");
            Assert.IsNull(container.Children[3].Children[0].Children);
            Assert.AreEqual(container.Children[3].Children[1].Data, "lat");
            Assert.IsNull(container.Children[3].Children[1].Children);
            Assert.AreEqual(container.Children[3].Children[2].Data, "long");
            Assert.IsNull(container.Children[3].Children[2].Children);

            Assert.AreEqual(container.Children[4].Data, "something");
            Assert.IsNull(container.Children[4].Children);

            Assert.AreEqual(container.Children[5].Data, "anything");
            Assert.IsNull(container.Children[5].Children);
        }

        [TestMethod]
        public void TestSuperDeepNestedString()
        {
            string str = "(id,created,employee(id,firstname,employeeType(id),lastname,employee(id,firstname,employeeType(id),lastname,employee(id,firstname,employeeType(id),lastname,employee(id,firstname,employeeType(id),lastname,employee(id,firstname,employeeType(id),lastname,employee(id,firstname,employeeType(id),lastname,employee(id,firstname,employeeType(id),lastname,employee(id,firstname,employeeType(id),lastname,employee(id,firstname,employeeType(id),lastname,employee(id,firstname,employeeType(id),lastname,employee(id,firstname,employeeType(id),lastname,employee(id,firstname,employeeType(id),lastname,employee(id,firstname,employeeType(id),lastname,employee(id,firstname,employeeType(id),lastname,employee(id,firstname,employeeType(id),lastname,employee(id,firstname,employeeType(id),lastname,employee(id,firstname,employeeType(id),lastname,employee(id,firstname,employeeType(id),lastname,employee(id,firstname,employeeType(id),lastname,employee(id,firstname,employeeType(id),lastname,employee(id,firstname,employeeType(id),lastname,employee(id,firstname,employeeType(id),lastname,employee(id,firstname,employeeType(id),lastname))))))))))))))))))))))),location(id,lat,long),something,anything)";
            var container = StringContainer.Get(str);
            container.Sort();
        }

        [TestMethod]
        public void TestSortedTestString()
        {
            string str = "(id,created,employee(id,firstname,employeeType(id),lastname),location(id,lat,long),something,anything)";
            var container = StringContainer.Get(str);
            container.Sort();

            Assert.AreEqual(container.Children[0].Data, "anything");
            Assert.IsNull(container.Children[0].Children);

            Assert.AreEqual(container.Children[1].Data, "created");
            Assert.IsNull(container.Children[1].Children);

            Assert.AreEqual(container.Children[2].Data, "employee");
            Assert.AreEqual(container.Children[2].Children[0].Data, "employeeType");
            Assert.AreEqual(container.Children[2].Children[0].Children[0].Data, "id");
            Assert.IsNull(container.Children[2].Children[0].Children[0].Children);
            Assert.AreEqual(container.Children[2].Children[1].Data, "firstname");
            Assert.IsNull(container.Children[2].Children[1].Children);
            Assert.AreEqual(container.Children[2].Children[2].Data, "id");
            Assert.IsNull(container.Children[2].Children[2].Children);
            Assert.AreEqual(container.Children[2].Children[3].Data, "lastname");
            Assert.IsNull(container.Children[2].Children[3].Children);

            Assert.AreEqual(container.Children[3].Data, "id");
            Assert.IsNull(container.Children[3].Children);

            Assert.AreEqual(container.Children[4].Data, "location");
            Assert.AreEqual(container.Children[4].Children[0].Data, "id");
            Assert.IsNull(container.Children[4].Children[0].Children);
            Assert.AreEqual(container.Children[4].Children[1].Data, "lat");
            Assert.IsNull(container.Children[4].Children[1].Children);
            Assert.AreEqual(container.Children[4].Children[2].Data, "long");
            Assert.IsNull(container.Children[4].Children[2].Children);

            Assert.AreEqual(container.Children[5].Data, "something");
            Assert.IsNull(container.Children[5].Children);


        }

    }
}
