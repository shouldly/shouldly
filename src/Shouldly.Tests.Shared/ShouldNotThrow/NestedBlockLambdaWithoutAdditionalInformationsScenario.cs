using System;
using Shouldly.Tests.Strings;
using Xunit;

namespace Shouldly.Tests.ShouldNotThrow
{
    public class NestedBlockLambdaWithoutAdditionalInformationsScenario
    {
        [Fact]
        public void NestedBlockLambdaWithoutAdditionalInformationsScenarioShouldFail()
        {
            Verify.ShouldFail(() =>
            {
                Should.NotThrow(() =>
                {
                    if (true)
                    {
                        throw new Exception("Dummy message.");
                    }
                });
            },

errorWithSource:
@"`if (true) { throw new Exception(""Dummy message.""); }`
    should not throw but threw
System.Exception
    with message
""Dummy message.""",

errorWithoutSource:
#if net40 || net45
@"Task
    should not throw but threw
System.Exception
    with message
""Dummy message.""");
#else
@"delegate
    should not throw but threw
System.Exception
    with message
""Dummy message.""");
#endif
        }
    }
} 