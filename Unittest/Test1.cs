namespace Unittest
{
    [TestClass]
    public sealed class Test1
    {
        [TestMethod]
        public void TestMethod1()
        {
            int x = 1;
            int y = 2;

            Assert.AreEqual(3, x + y);
        }
    }
}
