/**
 * ResultObject.cs
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
    public class ResultObject
    {
        private string m_ResponseDetails;

        private string m_ResponseStatus;

        [JsonProperty("responseDetails")]
        public string ResponseDetails
        {
            get { return m_ResponseDetails; }
            set { m_ResponseDetails = value; }
        }

        [JsonProperty("responseStatus")]
        public string ResponseStatus
        {
            get { return m_ResponseStatus; }
            set { m_ResponseStatus = value; }
        }
    }
}
