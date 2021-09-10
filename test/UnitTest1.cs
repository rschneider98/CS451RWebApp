using System;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using NUnit.Framework.Internal;
using NUnit.Framework.Internal.Commands;

namespace APITests
{
    [TestFixture(TestName = "Display name of Tests")]
    public class UnitTest1
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TestGet()
        {
            Assert.AreEqual(4, 2+2);
        }
    }
}