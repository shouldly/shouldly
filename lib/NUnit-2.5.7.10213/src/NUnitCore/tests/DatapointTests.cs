using System;
#if NET_2_0
using System.Collections.Generic;
#endif
using NUnit.Framework;
using NUnit.TestData.DatapointFixture;
using NUnit.TestUtilities;

namespace NUnit.Core.Tests
{
    public class DatapointTests
    {
        private void RunTestOnFixture(Type fixtureType)
        {
            TestResult result = TestBuilder.RunTestFixture(fixtureType);
            NUnit.Util.ResultSummarizer summary = new NUnit.Util.ResultSummarizer(result);
            Assert.That(summary.Passed, Is.EqualTo(2));
            Assert.That(summary.Inconclusive, Is.EqualTo(3));
            Assert.That(result.ResultState, Is.EqualTo(ResultState.Success));
        }

        [Test]
        public void WorksOnField()
        {
            RunTestOnFixture(typeof(SquareRootTest_Field_Double));
        }

        [Test]
        public void WorksOnArray()
        {
            RunTestOnFixture(typeof(SquareRootTest_Field_ArrayOfDouble));
        }

        [Test]
        public void WorksOnPropertyReturningArray()
        {
            RunTestOnFixture(typeof(SquareRootTest_Property_ArrayOfDouble));
        }

        [Test]
        public void WorksOnMethodReturningArray()
        {
            RunTestOnFixture(typeof(SquareRootTest_Method_ArrayOfDouble));
        }

#if NET_2_0 && CS_3_0
        [Test]
        public void WorksOnIEnumerableOfT()
        {
            RunTestOnFixture(typeof(SquareRootTest_Field_IEnumerableOfDouble));
        }

        [Test]
        public void WorksOnPropertyReturningIEnumerableOfT()
        {
            RunTestOnFixture(typeof(SquareRootTest_Property_IEnumerableOfDouble));
        }

        [Test]
        public void WorksOnMethodReturningIEnumerableOfT()
        {
            RunTestOnFixture(typeof(SquareRootTest_Method_IEnumerableOfDouble));
        }
#endif
    }
}
