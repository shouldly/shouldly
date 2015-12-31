using System;
using Shouldly.Tests.Strings;
using Xunit;

namespace Shouldly.Tests.ShouldNotThrow
{
    public class NestedBlockLambdaScenario
    {
        [Fact]
        public void NestedBlockLambdaScenarioShouldFail()
        {
            Verify.ShouldFail(() =>
            {
                Should.NotThrow(() =>
                {
                    if (true)
                    {
                        throw new Exception("Dummy message.");
                    }
                }, () => "Additional info");
            },

errorWithSource:
@"`if (true) { throw new Exception(""Dummy message.""); }`
    should not throw but threw
System.Exception
    with message
""Dummy message.""

Additional Info:
    Additional info",

errorWithoutSource:
#if net40 || net45
@"Task
    should not throw but threw
System.Exception
    with message
""Dummy message.""

Additional Info:
    Additional info");
#else
@"delegate
    should not throw but threw
System.Exception
    with message
""Dummy message.""

Additional Info:
    Additional info");
#endif
        }
    }
} 