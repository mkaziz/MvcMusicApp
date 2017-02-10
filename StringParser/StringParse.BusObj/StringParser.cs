using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringParse.BusObj
{
    //"(id,created,employee(id,firstname,employeeType(id), lastname),location(id,lat,long))" 
    public class StringContainer: IComparable
    {
        public string Data { get; set; }
        public List<StringContainer> Children { get; set; }


        public static StringContainer Get(string input)
        {
            ValidateString(input);
            var container = new StringContainer("", input);

            return container;
        }

        public void Sort()
        {
            if (Children != null)
            {
                Children.Sort();

                foreach (var child in Children)
                {
                    child.Sort();
                }
            }
        }

        protected static void ValidateString(string data)
        {
            int parenCount = 0;
            for (int i = 0; i < data.Length; i++)
            {
                if (data.ElementAt(i) == '(')
                    parenCount++;
                else if (data.ElementAt(i) == ')')
                    parenCount--;

                if (parenCount < 0)
                    throw new ArgumentException("Data string passed in with incorrect format");

            }

            if (parenCount > 0)
                throw new ArgumentException("Data string passed in with incorrect format");
        }

        protected StringContainer(string data, string childrenInput="")
        {
            Data = data;
            ParseChildren(childrenInput);
        }

        public override string ToString()
        {
            return Data;
        }

        public string ToDeepString()
        {
            return BuildString(new StringBuilder(), 0).ToString();
        }

        protected StringBuilder BuildString(StringBuilder builder, int indentation = -1)
        {
            builder.AppendLine(string.Format("{0} {1}", new String('-', indentation), this.Data));

            if (Children != null)
            {
                foreach (var child in Children)
                {
                    child.BuildString(builder, indentation + 1);
                }
            }

            return builder;
        }

        protected void ParseChildren(string str)
        {
            if (str.Length == 0)
                return;

            // snip off any parens on either end
            if (str.IndexOf('(') == 0 && str.LastIndexOf(')') == str.Length - 1)
                str = str.Substring(1, str.Length - 2);

            int currentIndex = 0;
            Children = new List<StringContainer>();
            do
            {
                int nextCommaIndex = str.IndexOf(',', currentIndex);
                int nextParenIndex = str.IndexOf('(', currentIndex);

                if (nextCommaIndex == -1) nextCommaIndex = int.MaxValue;
                if (nextParenIndex == -1) nextParenIndex = int.MaxValue;

                if (nextCommaIndex == int.MaxValue && nextParenIndex == int.MaxValue) // single word passed in
                {
                    currentIndex = HandleSingleWord(str, currentIndex);
                }
                else if (nextCommaIndex < nextParenIndex)
                {
                    currentIndex = HandleNextLeaf(str, currentIndex, nextCommaIndex);

                }
                else if (nextParenIndex < nextCommaIndex)
                {
                    currentIndex = HandleNextNode(str, currentIndex, nextParenIndex);
                }
                else
                {
                    currentIndex++; // should never reach here, but in case of error, will prevent stackoverflow
                }

            } while (currentIndex < str.Length);
        }

        private int HandleNextNode(string str, int currentIndex, int nextParenIndex)
        {
            int closingParenIndex = nextParenIndex + 1;
            int parenCount = 1;
            for (int i = closingParenIndex; i < str.Length; i++)
            {
                if (str.ElementAt(i) == '(')
                    parenCount++;
                else if (str.ElementAt(i) == ')')
                    parenCount--;

                if (parenCount == 0)
                {
                    closingParenIndex = i;
                    break;
                }

            }
            var nextChild = str.Substring(currentIndex, nextParenIndex - currentIndex);
            var grandChildrenString = str.Substring(nextParenIndex, closingParenIndex - nextParenIndex + 1);
            Children.Add(new StringContainer(nextChild, grandChildrenString));
            currentIndex = closingParenIndex + 2; // skip paren + comma
            return currentIndex;
        }

        private int HandleNextLeaf(string str, int currentIndex, int nextCommaIndex)
        {
            var nextChild = str.Substring(currentIndex, nextCommaIndex - currentIndex);
            Children.Add(new StringContainer(nextChild));
            currentIndex = nextCommaIndex + 1;
            return currentIndex;
        }

        private int HandleSingleWord(string str, int currentIndex)
        {
            var child = str.Substring(currentIndex, str.Length - currentIndex);
            Children.Add(new StringContainer(child));
            currentIndex = str.Length;
            return currentIndex;
        }

        public int CompareTo(object obj)
        {
            return string.Compare(this.ToString(), obj.ToString());
        }
    }
}
