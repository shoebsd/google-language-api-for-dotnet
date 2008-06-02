/**
 * TestRequestBase.cs
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
using NUnit.Framework;

namespace Google.API.Translate.Test
{
    [TestFixture]
    public class TestRequestBase
    {
        [Test]
        public void UrlStringTest()
        {
            MockRequest mockRequest = new MockRequest("http://www.xxx.com", "xx");
            mockRequest.ArgB = 50;
            Console.Write(mockRequest.Uri.Query);
            Assert.AreEqual("http://www.xxx.com?a=default&b=50&q=xx&v=1.0", mockRequest.UrlString);
        }
    }

    class MockRequest : RequestBase
    {
        public MockRequest(string address, string content)
            : base(content)
        {
            m_BaseAddress = address;
        }

        private string m_BaseAddress;

        [Argument("a", "default")]
        public string ArgA { get; set; }

        [Argument("b", Optional = false)]
        public object ArgB { get; set; }

        protected override string BaseAddress
        {
            get { return m_BaseAddress; }
        }
    }
}
