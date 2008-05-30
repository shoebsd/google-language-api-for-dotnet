using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NUnit.Framework;

using Google.API.Translate;

namespace Google.API.Translate.Test
{
    [TestFixture]
    public class TestArgumentAttribute
    {
        [Test]
        public void ConstructorTest()
        {
            string name = "Some name";
            ArgumentAttribute aa = new ArgumentAttribute(name);
            Assert.AreEqual(name, aa.Name);
            Assert.IsTrue(aa.Optional);
            Assert.IsNull(aa.DefaultValue);

            aa.Optional = false;
            Assert.IsFalse(aa.Optional);
        }

        [Test]
        public void ConstructorTest2()
        {
            string name = "Some name";
            string defaultValue = "Some default value.";
            ArgumentAttribute aa = new ArgumentAttribute(name, defaultValue);
            Assert.AreEqual(name, aa.Name);
            Assert.IsFalse(aa.Optional);
            Assert.AreEqual(defaultValue, aa.DefaultValue);

            aa.Optional = true;
            Assert.IsTrue(aa.Optional);
        }
    }
}
