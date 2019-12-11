using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Text;

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

            Assert.AreEqual(2, output[0]);
            Assert.AreEqual(0, output[1]);
            Assert.AreEqual(0, output[2]);
            Assert.AreEqual(0, output[3]);
            Assert.AreEqual(99, output[4]);
        }

        [TestMethod]
        public void TestCase_02()
        {
            int[] input = new int[] { 2, 3, 0, 3, 99 };
            int[] output = IntcodeProgramExecutor.Execute(input);

            Assert.AreEqual(2, output[0]);
            Assert.AreEqual(3, output[1]);
            Assert.AreEqual(0, output[2]);
            Assert.AreEqual(6, output[3]);
            Assert.AreEqual(99, output[4]);
        }

        [TestMethod]
        public void TestCase_03()
        {
            int[] input = new int[] { 2, 4, 4, 5, 99, 0 };
            int[] output = IntcodeProgramExecutor.Execute(input);

            Assert.AreEqual(2, output[0]);
            Assert.AreEqual(4, output[1]);
            Assert.AreEqual(4, output[2]);
            Assert.AreEqual(5, output[3]);
            Assert.AreEqual(99, output[4]);
            Assert.AreEqual(9801, output[5]);
        }

        [TestMethod]
        public void TestCase_04()
        {
            int[] input = new int[] { 1, 1, 1, 4, 99, 5, 6, 0, 99 };
            int[] output = IntcodeProgramExecutor.Execute(input);

            Assert.AreEqual(30, output[0]);
            Assert.AreEqual(1, output[1]);
            Assert.AreEqual(1, output[2]);
            Assert.AreEqual(4, output[3]);
            Assert.AreEqual(2, output[4]);
            Assert.AreEqual(5, output[5]);
            Assert.AreEqual(6, output[6]);
            Assert.AreEqual(0, output[7]);
            Assert.AreEqual(99, output[8]);
        }

        [TestMethod]
        public void TestCase_05()
        {
            int[] input = new int[] { 1002, 4, 3, 4, 33 };
            int[] output = IntcodeProgramExecutor.Execute(input);

            Assert.AreEqual(1002, output[0]);
            Assert.AreEqual(4, output[1]);
            Assert.AreEqual(3, output[2]);
            Assert.AreEqual(4, output[3]);
            Assert.AreEqual(99, output[4]);
        }

        [TestMethod]
        public void TestCase_06()
        {
            int[] input = new int[] { 1101, 100, -1, 4, 0 };
            int[] output = IntcodeProgramExecutor.Execute(input);

            Assert.AreEqual(1101, output[0]);
            Assert.AreEqual(100, output[1]);
            Assert.AreEqual(-1, output[2]);
            Assert.AreEqual(4, output[3]);
            Assert.AreEqual(99, output[4]);
        }

        [TestMethod]
        public void TestCase_07()
        {
            int[] input = new int[] { 3, 0, 4, 0, 99 };
            var stringBuilder = new StringBuilder();

            IntcodeProgramExecutor.InputReader = new StringReader("11");
            IntcodeProgramExecutor.OutputWriter = new StringWriter(stringBuilder);

            int[] output = IntcodeProgramExecutor.Execute(input);

            Assert.AreEqual(11, output[0]);
            Assert.AreEqual(0, output[1]);
            Assert.AreEqual(4, output[2]);
            Assert.AreEqual(0, output[3]);
            Assert.AreEqual(99, output[4]);

            Assert.IsTrue(stringBuilder.ToString().Contains("Output value: 11"));
        }
    }
}
