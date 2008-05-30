using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Google.API.Translate
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public sealed class ArgumentAttribute : Attribute
    {
        private readonly string m_Name;
        private bool m_Optional;
        private readonly object m_DefaultValue;

        public ArgumentAttribute(string name)
        {
            m_Name = name;
            Optional = true;
            m_DefaultValue = null;
        }

        public ArgumentAttribute(string name, object defaultValue)
        {
            m_Name = name;
            if (defaultValue == null)
            {
                Optional = true;
            }
            else
            {
                Optional = false;
            }
            m_DefaultValue = defaultValue;
        }

        public string Name
        {
            get { return m_Name; }
        }

        public bool Optional
        {
            get { return m_Optional; }
            set { m_Optional = value; }
        }

        public object DefaultValue
        {
            get { return m_DefaultValue; }
        }
    }
}
