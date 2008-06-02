/**
 * RequestBase.cs
 *
 * Copyright (C) 2008,  iron9light
 *
 * This program is free software; you can redistribute it and/or
 * modify it under the terms of the GNU General Public License
 * as published by the Free Software Foundation; either version 2
 * of the License, or (at your option) any later version.
 *
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 *
 * You should have received a copy of the GNU General Public License
 * along with this program. if not, write to the Free Software
 * Foundation, Inc., 59 Temple Place - Suite 330, Boston, MA  02111-1307, USA.
 */

using System;
using System.Collections.Generic;
using System.Net;
using System.Reflection;
using System.Text;
using System.Web;

namespace Google.API.Translate
{
    internal abstract class RequestBase
    {
        #region Fields

        private string m_UrlString;

        #endregion

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

        #region Properties

        [Argument("q", Optional = false, NeedEncode = true)]
        public string Content { get; private set; }

        [Argument("v", "1.0")]
        public string Version { get; private set; }

        [Argument("key?")]
        public string Key { get; private set; }

        public string UrlString
        {
            get
            {
                if (m_UrlString == null)
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

        protected abstract string BaseAddress { get; }

        private ICollection<KeyValuePair<string, string>> Arguments
        {
            get
            {
                return GetArguments();
            }
        }

        #endregion

        #region Methods

        public WebRequest GetWebRequest()
        {
            return WebRequest.Create(UrlString);
        }

        public override string ToString()
        {
            return UrlString;
        }

        private ICollection<KeyValuePair<string, string>> GetArguments()
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            PropertyInfo[] properties = this.GetType().GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
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
                    throw new TranslateException(string.Format("Property {0}({1}) cannot be null", info.Name, name));
                }

                string valueString = value.ToString();

                if (argAttr.NeedEncode)
                {
                    valueString = HttpUtility.UrlEncode(valueString);
                }

                dict[name] = valueString;

            }
            return dict;
        }

        private string GetUrlString()
        {
            if (Arguments.Count == 0)
            {
                return BaseAddress;
            }

            StringBuilder sb = new StringBuilder(BaseAddress);
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
                sb.Append(argument.Value);
            }
            return sb.ToString();
        }

        #endregion
    }
}
