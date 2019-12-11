using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Text;

namespace AdventOfCode._2019.Day05.UnitTests
{
    [TestClass]
    public class PartOneTests
    {
        [TestMethod]
        public void PartOne_TestCase_01()
        {
            int[] program = new int[] { 1, 0, 0, 0, 99 };
            IntcodeProgramExecutor.Execute(program);

            Assert.AreEqual(2, program[0]);
            Assert.AreEqual(0, program[1]);
            Assert.AreEqual(0, program[2]);
            Assert.AreEqual(0, program[3]);
            Assert.AreEqual(99, program[4]);
        }

        [TestMethod]
        public void PartOne_TestCase_02()
        {
            int[] program = new int[] { 2, 3, 0, 3, 99 };
            IntcodeProgramExecutor.Execute(program);

            Assert.AreEqual(2, program[0]);
            Assert.AreEqual(3, program[1]);
            Assert.AreEqual(0, program[2]);
            Assert.AreEqual(6, program[3]);
            Assert.AreEqual(99, program[4]);
        }

        [TestMethod]
        public void PartOne_TestCase_03()
        {
            int[] program = new int[] { 2, 4, 4, 5, 99, 0 };
            IntcodeProgramExecutor.Execute(program);

            Assert.AreEqual(2, program[0]);
            Assert.AreEqual(4, program[1]);
            Assert.AreEqual(4, program[2]);
            Assert.AreEqual(5, program[3]);
            Assert.AreEqual(99, program[4]);
            Assert.AreEqual(9801, program[5]);
        }

        [TestMethod]
        public void PartOne_TestCase_04()
        {
            int[] program = new int[] { 1, 1, 1, 4, 99, 5, 6, 0, 99 };
            IntcodeProgramExecutor.Execute(program);

            Assert.AreEqual(30, program[0]);
            Assert.AreEqual(1, program[1]);
            Assert.AreEqual(1, program[2]);
            Assert.AreEqual(4, program[3]);
            Assert.AreEqual(2, program[4]);
            Assert.AreEqual(5, program[5]);
            Assert.AreEqual(6, program[6]);
            Assert.AreEqual(0, program[7]);
            Assert.AreEqual(99, program[8]);
        }

        [TestMethod]
        public void PartOne_TestCase_05()
        {
            int[] program = new int[] { 1002, 4, 3, 4, 33 };
            IntcodeProgramExecutor.Execute(program);

            Assert.AreEqual(1002, program[0]);
            Assert.AreEqual(4, program[1]);
            Assert.AreEqual(3, program[2]);
            Assert.AreEqual(4, program[3]);
            Assert.AreEqual(99, program[4]);
        }

        [TestMethod]
        public void PartOne_TestCase_06()
        {
            int[] program = new int[] { 1101, 100, -1, 4, 0 };
            IntcodeProgramExecutor.Execute(program);

            Assert.AreEqual(1101, program[0]);
            Assert.AreEqual(100, program[1]);
            Assert.AreEqual(-1, program[2]);
            Assert.AreEqual(4, program[3]);
            Assert.AreEqual(99, program[4]);
        }

        [TestMethod]
        public void PartOne_TestCase_07()
        {
            int[] program = new int[] { 3, 0, 4, 0, 99 };
            var outputBuilder = new StringBuilder();

            IntcodeProgramExecutor.InputReader = new StringReader("11");
            IntcodeProgramExecutor.OutputWriter = new StringWriter(outputBuilder);

            IntcodeProgramExecutor.Execute(program);

            Assert.AreEqual(11, program[0]);
            Assert.AreEqual(0, program[1]);
            Assert.AreEqual(4, program[2]);
            Assert.AreEqual(0, program[3]);
            Assert.AreEqual(99, program[4]);

            Assert.IsTrue(outputBuilder.ToString().Contains("Output value: 11"));
        }
    }
}
