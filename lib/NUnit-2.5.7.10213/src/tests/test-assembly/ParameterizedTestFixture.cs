using System;
using NUnit.Framework;

namespace NUnit.TestData
{
    [TestFixture(1)]
    [TestFixture(2)]
    public class ParameterizedTestFixture
    {
        [Test]
        public void MethodWithoutParams()
        {
        }

        [TestCase(10,20)]
        public void MethodWithParams(int x, int y)
        {
        }
    }
}
