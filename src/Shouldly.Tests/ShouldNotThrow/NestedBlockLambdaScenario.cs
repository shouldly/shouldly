namespace Shouldly.Tests.ShouldNotThrow;

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
                }, "Additional info");
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
            @"Task
    should not throw but threw
System.Exception
    with message
""Dummy message.""

Additional Info:
    Additional info");
    }
}