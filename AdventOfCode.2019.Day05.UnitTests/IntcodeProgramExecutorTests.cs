using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCode._2019.Day05.UnitTests
{
    [TestClass]
    public class IntcodeProgramExecutorTests
    {
        [TestMethod]
        public void TestCase_01()
        {
            int[] input = new int[] { 1, 0, 0, 0, 99 };
            int[] output = IntcodeProgramExecutor.Execute(input);

            Assert.AreEqual(output[0], 2);
            Assert.AreEqual(output[1], 0);
            Assert.AreEqual(output[2], 0);
            Assert.AreEqual(output[3], 0);
            Assert.AreEqual(output[4], 99);
        }

        [TestMethod]
        public void TestCase_02()
        {
            int[] input = new int[] { 2, 3, 0, 3, 99 };
            int[] output = IntcodeProgramExecutor.Execute(input);

            Assert.AreEqual(output[0], 2);
            Assert.AreEqual(output[1], 3);
            Assert.AreEqual(output[2], 0);
            Assert.AreEqual(output[3], 6);
            Assert.AreEqual(output[4], 99);
        }

        [TestMethod]
        public void TestCase_03()
        {
            int[] input = new int[] { 2, 4, 4, 5, 99, 0 };
            int[] output = IntcodeProgramExecutor.Execute(input);

            Assert.AreEqual(output[0], 2);
            Assert.AreEqual(output[1], 4);
            Assert.AreEqual(output[2], 4);
            Assert.AreEqual(output[3], 5);
            Assert.AreEqual(output[4], 99);
            Assert.AreEqual(output[5], 9801);
        }

        [TestMethod]
        public void TestCase_04()
        {
            int[] input = new int[] { 1, 1, 1, 4, 99, 5, 6, 0, 99 };
            int[] output = IntcodeProgramExecutor.Execute(input);

            Assert.AreEqual(output[0], 30);
            Assert.AreEqual(output[1], 1);
            Assert.AreEqual(output[2], 1);
            Assert.AreEqual(output[3], 4);
            Assert.AreEqual(output[4], 2);
            Assert.AreEqual(output[5], 5);
            Assert.AreEqual(output[6], 6);
            Assert.AreEqual(output[7], 0);
            Assert.AreEqual(output[8], 99);
        }

        [TestMethod]
        public void TestCase_05()
        {
            int[] input = new int[] { 1002, 4, 3, 4, 33 };
            int[] output = IntcodeProgramExecutor.Execute(input);

            Assert.AreEqual(output[0], 1002);
            Assert.AreEqual(output[1], 4);
            Assert.AreEqual(output[2], 3);
            Assert.AreEqual(output[3], 4);
            Assert.AreEqual(output[4], 99);            
        }

        [TestMethod]
        public void TestCase_06()
        {
            int[] input = new int[] { 1101, 100, -1, 4, 0 };
            int[] output = IntcodeProgramExecutor.Execute(input);

            Assert.AreEqual(output[0], 1101);
            Assert.AreEqual(output[1], 100);
            Assert.AreEqual(output[2], -1);
            Assert.AreEqual(output[3], 4);
            Assert.AreEqual(output[4], 99);
        }
    }
}
