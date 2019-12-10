using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCode._2019.Day04.UnitTests
{
    [TestClass]
    public class PasswordvalidatorTests
    {
        [TestMethod]
        public void TestCase_01()
        {
            string passwordToTest = "111111";
            Assert.AreEqual(false, PasswordValidator.Validate(passwordToTest));
        }

        [TestMethod]
        public void TestCase_02()
        {
            string passwordToTest = "223450";
            Assert.AreEqual(false, PasswordValidator.Validate(passwordToTest));
        }

        [TestMethod]
        public void TestCase_03()
        {
            string passwordToTest = "123789";
            Assert.AreEqual(false, PasswordValidator.Validate(passwordToTest));
        }

        [TestMethod]
        public void TestCase_04()
        {
            string passwordToTest = "112233";
            Assert.AreEqual(true, PasswordValidator.Validate(passwordToTest));
        }

        [TestMethod]
        public void TestCase_05()
        {
            string passwordToTest = "123444";
            Assert.AreEqual(false, PasswordValidator.Validate(passwordToTest));
        }

        [TestMethod]
        public void TestCase_06()
        {
            string passwordToTest = "111122";
            Assert.AreEqual(true, PasswordValidator.Validate(passwordToTest));
        }

        [TestMethod]
        public void TestCase_07()
        {
            string passwordToTest = "113333";
            Assert.AreEqual(true, PasswordValidator.Validate(passwordToTest));
        }
    }
}
