/**
 * TranslateData.cs
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
using System.Linq;
using System.Text;

using Newtonsoft.Json;

namespace Google.API.Translate
{
    [JsonObject(MemberSerialization.OptOut)]
    public class TranslateResult : ResultObject
    {
        [JsonObject(MemberSerialization.OptOut)]
        public class TranslateData
        {
            private string m_TranslatedText;

            private string m_DetectedSourceLanguage;

            [JsonProperty("translatedText")]
            public string TranslatedText
            {
                get { return m_TranslatedText; }
                set { m_TranslatedText = value; }
            }

            [JsonProperty("detectedSourceLanguage")]
            public string DetectedSourceLanguage
            {
                get { return m_DetectedSourceLanguage; }
                set { m_DetectedSourceLanguage = value; }
            }
        }

        private TranslateData m_ResponseData;

        [JsonProperty("responseData")]
        public TranslateData ResponseData
        {
            get { return m_ResponseData; }
            set { m_ResponseData = value; }
        }
    }
}
