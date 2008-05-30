using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NUnit.Framework;

using Google.API.Translate;

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
            Assert.AreEqual("http://www.xxx.com?a=default&b=50&p=xx&v=1.0", mockRequest.UrlString);
        }
    }

    class MockRequest : RequestBase
    {
        public MockRequest(string address, string content)
            : base(content)
        {
            m_AddressBase = address;
        }

        private string m_AddressBase;

        [Argument("a", "default")]
        public string ArgA { get; set; }

        [Argument("b", Optional = false)]
        public object ArgB { get; set; }

        protected override string AddressBase
        {
            get { return m_AddressBase; }
        }
    }
}
