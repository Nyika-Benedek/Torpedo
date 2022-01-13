using Microsoft.VisualStudio.TestTools.UnitTesting;
using Torpedo.Models;

namespace TestProject3
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            Assert.AreEqual(new Coordinate(0, 0), new Coordinate(0, 0));
        }

        [TestMethod]
        public void TestMethod2()
        {
            Assert.AreEqual(new Coordinate(0, 0), new Coordinate(0, 1));
        }
    }
}
