using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestLocalProject;
using Shouldly;

namespace UnitTestProjectLocal
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            TestClass testClass = new TestClass();
            try
            {
                testClass.TestMethod();
                throw new Exception("Should not be there");
            }
            catch (PersonalizedException) { }
            catch (Exception) { throw; }
            Should.Throw<PersonalizedException>(() => { testClass.TestMethod(); });

        }
    }
}
