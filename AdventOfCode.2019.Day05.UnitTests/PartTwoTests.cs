using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Text;

namespace AdventOfCode._2019.Day05.UnitTests
{
    [TestClass]
    public class PartTwoTests
    {
        [TestMethod]
        public void PartTwo_Not_Equal_To_8_Using_Poistion_Mode()
        {
            int[] program = new int[] { 3, 9, 8, 9, 10, 9, 4, 9, 99, -1, 8 };

            var outputBuilder = new StringBuilder();
            IntcodeProgramExecutor.InputReader = new StringReader("11");
            IntcodeProgramExecutor.OutputWriter = new StringWriter(outputBuilder);

            IntcodeProgramExecutor.Execute(program);

            Assert.IsTrue(outputBuilder.ToString().Contains("Output value: 0"));
        }

        [TestMethod]
        public void PartTwo_TestCase_Equal_To_8_Using_Poistion_Mode()
        {
            int[] program = new int[] { 3, 9, 8, 9, 10, 9, 4, 9, 99, -1, 8 };

            var outputBuilder = new StringBuilder();
            IntcodeProgramExecutor.InputReader = new StringReader("8");
            IntcodeProgramExecutor.OutputWriter = new StringWriter(outputBuilder);

            IntcodeProgramExecutor.Execute(program);

            Assert.IsTrue(outputBuilder.ToString().Contains("Output value: 1"));
        }

        [TestMethod]
        public void PartTwo_TestCase_Not_Less_Than_8_Using_Poistion_Mode()
        {
            int[] program = new int[] { 3, 9, 7, 9, 10, 9, 4, 9, 99, -1, 8 };

            var outputBuilder = new StringBuilder();
            IntcodeProgramExecutor.InputReader = new StringReader("11");
            IntcodeProgramExecutor.OutputWriter = new StringWriter(outputBuilder);

            IntcodeProgramExecutor.Execute(program);

            Assert.IsTrue(outputBuilder.ToString().Contains("Output value: 0"));
        }

        [TestMethod]
        public void PartTwo_TestCase_Less_Than_8_Using_Poistion_Mode()
        {
            int[] program = new int[] { 3, 9, 7, 9, 10, 9, 4, 9, 99, -1, 8 };

            var outputBuilder = new StringBuilder();
            IntcodeProgramExecutor.InputReader = new StringReader("2");
            IntcodeProgramExecutor.OutputWriter = new StringWriter(outputBuilder);

            IntcodeProgramExecutor.Execute(program);

            Assert.IsTrue(outputBuilder.ToString().Contains("Output value: 1"));
        }

        [TestMethod]
        public void PartTwo_Not_Equal_To_8_Using_Immediate_Mode()
        {
            int[] program = new int[] { 3, 3, 1108, -1, 8, 3, 4, 3, 99 };

            var outputBuilder = new StringBuilder();
            IntcodeProgramExecutor.InputReader = new StringReader("11");
            IntcodeProgramExecutor.OutputWriter = new StringWriter(outputBuilder);

            IntcodeProgramExecutor.Execute(program);

            Assert.IsTrue(outputBuilder.ToString().Contains("Output value: 0"));
        }

        [TestMethod]
        public void PartTwo_TestCase_Equal_To_8_Using_Immediate_Mode()
        {
            int[] program = new int[] { 3, 3, 1108, -1, 8, 3, 4, 3, 99 };

            var outputBuilder = new StringBuilder();
            IntcodeProgramExecutor.InputReader = new StringReader("8");
            IntcodeProgramExecutor.OutputWriter = new StringWriter(outputBuilder);

            IntcodeProgramExecutor.Execute(program);

            Assert.IsTrue(outputBuilder.ToString().Contains("Output value: 1"));
        }

        [TestMethod]
        public void PartTwo_TestCase_Not_Less_Than_8_Using_Immediate_Mode()
        {
            int[] program = new int[] { 3, 3, 1107, -1, 8, 3, 4, 3, 99 };

            var outputBuilder = new StringBuilder();
            IntcodeProgramExecutor.InputReader = new StringReader("11");
            IntcodeProgramExecutor.OutputWriter = new StringWriter(outputBuilder);

            IntcodeProgramExecutor.Execute(program);

            Assert.IsTrue(outputBuilder.ToString().Contains("Output value: 0"));
        }

        [TestMethod]
        public void PartTwo_TestCase_Less_Than_8_Using_Immediate_Mode()
        {
            int[] program = new int[] { 3, 3, 1107, -1, 8, 3, 4, 3, 99 };

            var outputBuilder = new StringBuilder();
            IntcodeProgramExecutor.InputReader = new StringReader("2");
            IntcodeProgramExecutor.OutputWriter = new StringWriter(outputBuilder);

            IntcodeProgramExecutor.Execute(program);

            Assert.IsTrue(outputBuilder.ToString().Contains("Output value: 1"));
        }

        [TestMethod]
        public void PartTwo_TestCase_Is_Zero_Using_Position_Mode()
        {
            int[] program = new int[] { 3, 12, 6, 12, 15, 1, 13, 14, 13, 4, 13, 99, -1, 0, 1, 9 };

            var outputBuilder = new StringBuilder();
            IntcodeProgramExecutor.InputReader = new StringReader("0");
            IntcodeProgramExecutor.OutputWriter = new StringWriter(outputBuilder);

            IntcodeProgramExecutor.Execute(program);

            Assert.IsTrue(outputBuilder.ToString().Contains("Output value: 0"));
        }

        [TestMethod]
        public void PartTwo_TestCase_Is_Non_Zero_Using_Position_Mode()
        {
            int[] program = new int[] { 3, 12, 6, 12, 15, 1, 13, 14, 13, 4, 13, 99, -1, 0, 1, 9 };

            var outputBuilder = new StringBuilder();
            IntcodeProgramExecutor.InputReader = new StringReader("2");
            IntcodeProgramExecutor.OutputWriter = new StringWriter(outputBuilder);

            IntcodeProgramExecutor.Execute(program);

            Assert.IsTrue(outputBuilder.ToString().Contains("Output value: 1"));
        }


        [TestMethod]
        public void PartTwo_TestCase_Is_Zero_Using_Immediate_Mode()
        {
            int[] program = new int[] { 3, 3, 1105, -1, 9, 1101, 0, 0, 12, 4, 12, 99, 1 };

            var outputBuilder = new StringBuilder();
            IntcodeProgramExecutor.InputReader = new StringReader("0");
            IntcodeProgramExecutor.OutputWriter = new StringWriter(outputBuilder);

            IntcodeProgramExecutor.Execute(program);

            Assert.IsTrue(outputBuilder.ToString().Contains("Output value: 0"));
        }

        [TestMethod]
        public void PartTwo_TestCase_Is_Non_Zero_Using_Immediate_Mode()
        {
            int[] program = new int[] { 3, 3, 1105, -1, 9, 1101, 0, 0, 12, 4, 12, 99, 1 };

            var outputBuilder = new StringBuilder();
            IntcodeProgramExecutor.InputReader = new StringReader("-2");
            IntcodeProgramExecutor.OutputWriter = new StringWriter(outputBuilder);

            IntcodeProgramExecutor.Execute(program);

            Assert.IsTrue(outputBuilder.ToString().Contains("Output value: 1"));
        }
    }
}
