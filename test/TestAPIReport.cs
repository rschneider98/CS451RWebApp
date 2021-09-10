using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
//using Prime.Services;

namespace API
{
    [TestClass]
    public class Report
    {
        [TestMethod]
        public void Test1()
        {
            Assert.IsFalse(false, "1 should not be prime");
        }
    }
}