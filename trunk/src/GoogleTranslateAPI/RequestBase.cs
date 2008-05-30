using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Reflection;
using System.Web;

namespace Google.API.Translate
{
    public abstract class RequestBase
    {
        private ICollection<KeyValuePair<string, string>> m_Arguments;
        private string m_UrlString;
        protected abstract string AddressBase { get; }

        #region Constructors

        protected RequestBase(string content)
        {
            Content = content;
        }

        protected RequestBase(string content, string key)
        {
            Content = content;
            Key = key;
        }

        protected RequestBase(string content, string version, string key)
        {
            Content = content;
            Version = version;
            Key = key;
        }

        #endregion

        [Argument("p", Optional = false)]
        public string Content { get; private set; }

        [Argument("v", "1.0")]
        public string Version { get; private set; }

        [Argument("key?")]
        public string Key { get; private set; }

        public string UrlString
        {
            get
            {
                if(m_UrlString == null)
                {
                    m_UrlString = GetUrlString();
                }
                return m_UrlString;
            }
        }

        public Uri Uri
        {
            get
            {
                return new Uri(UrlString);
            }
        }

        public WebRequest GetWebRequest()
        {
            return WebRequest.Create(UrlString);
        }

        ICollection<KeyValuePair<string, string>> Arguments
        {
            get
            {
                if (m_Arguments == null)
                {
                    m_Arguments = GetArguments();
                }
                return m_Arguments;
            }
        }

        ICollection<KeyValuePair<string, string>> GetArguments()
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            PropertyInfo[] properties = this.GetType().GetProperties();
            foreach (PropertyInfo info in properties)
            {
                object[] attrs = info.GetCustomAttributes(typeof(ArgumentAttribute), true);
                if (attrs.Length == 0)
                {
                    continue;
                }
                ArgumentAttribute argAttr = attrs[0] as ArgumentAttribute;
                string name = argAttr.Name;
                object value = info.GetValue(this, null);
                if (value == null)
                {
                    if (!argAttr.Optional)
                    {
                        value = argAttr.DefaultValue;
                    }
                    else
                    {
                        continue;
                    }
                }
                if (value == null)
                {
                    throw new Exception(string.Format("Property {0}({1}) cannot be null", info.Name, name));
                }

                dict[name] = value.ToString();

            }
            return dict;
        }

        string GetUrlString()
        {
            if (Arguments.Count == 0)
            {
                return AddressBase;
            }

            StringBuilder sb = new StringBuilder(AddressBase);
            sb.Append("?");
            bool isFirst = true;
            foreach (KeyValuePair<string, string> argument in Arguments)
            {
                if (!isFirst)
                {
                    sb.Append("&");
                }
                else
                {
                    isFirst = false;
                }
                sb.Append(argument.Key);
                sb.Append("=");
                sb.Append(HttpUtility.UrlEncode(argument.Value));
            }
            return sb.ToString();
        }
    }
}
