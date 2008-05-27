/**
 * DetectResult.cs
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

using Newtonsoft.Json;

namespace Google.API.Translate
{
    [JsonObject(MemberSerialization.OptOut)]
    public class DetectResult : ResultObject
    {
        [JsonObject(MemberSerialization.OptOut)]
        public class DetectData
        {
            private string m_LanguageCode;

            private bool m_IsReliable;

            private double m_Confidence;

            [JsonProperty("language")]
            public string LanguageCode
            {
                get { return m_LanguageCode; }
                set { m_LanguageCode = value; }
            }

            [JsonProperty("isReliable")]
            public bool IsReliable
            {
                get { return m_IsReliable; }
                set { m_IsReliable = value; }
            }

            [JsonProperty("confidence")]
            public double Confidence
            {
                get { return m_Confidence; }
                set { m_Confidence = value; }
            }
        }

        private DetectData m_ResponseData;

        [JsonProperty("responseData")]
        public DetectData ResponseData
        {
            get { return m_ResponseData; }
            set { m_ResponseData = value; }
        }
    }
}
