/**
 * TestArgumentAttribute.cs
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

using NUnit.Framework;

namespace Google.API.Translate.Test
{
    [TestFixture]
    public class TestArgumentAttribute
    {
        [Test]
        public void ConstructorTest()
        {
            string name = "Some name";
            ArgumentAttribute attribute = new ArgumentAttribute(name);
            Assert.AreEqual(name, attribute.Name);
            Assert.IsTrue(attribute.Optional);
            Assert.IsNull(attribute.DefaultValue);
            Assert.IsFalse(attribute.NeedEncode);

            attribute.Optional = false;
            Assert.IsFalse(attribute.Optional);

            attribute.NeedEncode = true;
            Assert.IsTrue(attribute.NeedEncode);
        }

        [Test]
        public void ConstructorTest2()
        {
            string name = "Some name";
            string defaultValue = "Some default value.";
            ArgumentAttribute attribute = new ArgumentAttribute(name, defaultValue);
            Assert.AreEqual(name, attribute.Name);
            Assert.IsFalse(attribute.Optional);
            Assert.AreEqual(defaultValue, attribute.DefaultValue);
            Assert.IsFalse(attribute.NeedEncode);

            attribute.Optional = true;
            Assert.IsTrue(attribute.Optional);

            attribute.NeedEncode = true;
            Assert.IsTrue(attribute.NeedEncode);
        }
    }
}
