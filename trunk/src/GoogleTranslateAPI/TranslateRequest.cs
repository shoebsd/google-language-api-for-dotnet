/**
 * TranslateRequest.cs
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

namespace Google.API.Translate
{
    public enum TranslateFormat
    {
        text,
        html,
    }

    public class TranslateRequest : RequestBase
    {
        private static readonly string s_BaseAddress = @"http://ajax.googleapis.com/ajax/services/language/translate";
        private static readonly string s_LangpairFormat = "{0}%7C{1}";

        public TranslateRequest(string text, string from, string to)
            : base(text)
        {
            From = from;
            To = to;
        }

        public TranslateRequest(string text, string from, string to, TranslateFormat format)
            : this(text, from, to)
        {
            Format = format;
        }

        public string From { get; private set; }

        public string To { get; private set; }

        [Argument("langpair", Optional = false)]
        private string LanguagePair
        {
            get
            {
                string languagePair = string.Format(s_LangpairFormat, From, To);
                return languagePair;
            }
        }

        [Argument("format?")]
        public TranslateFormat Format { get; private set; }

        protected override string BaseAddress
        {
            get { return s_BaseAddress; }
        }
    }
}
