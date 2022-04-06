using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecossistema.Util
{
    public class StringValueAttribute : Attribute
    {
        public string StringValue { get; protected set; }

        public StringValueAttribute(string value)
        {
            StringValue = value;
        }
    }

    public class Int32ValueAttribute : Attribute
    {
        public int Int32Value { get; protected set; }

        public Int32ValueAttribute(int value)
        {
            Int32Value = value;
        }
    }
}