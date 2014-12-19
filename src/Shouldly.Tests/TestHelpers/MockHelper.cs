using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Moq;

namespace Shouldly.Tests.TestHelpers
{
    public class MockHelper
    {
        internal static Mock<ITestEnvironment> GetMockTestEnvironment(object actual, object expected)
        {
            var mockTestEnvironment = new Mock<ITestEnvironment>();
            mockTestEnvironment.SetupGet(x => x.Actual).Returns(actual);
            mockTestEnvironment.SetupGet(x => x.Expected).Returns(expected);
            return mockTestEnvironment;
        }
    }
}
