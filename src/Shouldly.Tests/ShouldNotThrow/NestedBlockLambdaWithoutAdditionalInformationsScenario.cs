namespace Shouldly.Tests.ShouldNotThrow;

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
                        throw new("Dummy message.");
                    }
                });
            },

            errorWithSource:
            @"`if (true) { throw new(""Dummy message.""); }`
    should not throw but threw
System.Exception
    with message
""Dummy message.""",

            errorWithoutSource:
            @"Task
    should not throw but threw
System.Exception
    with message
""Dummy message.""");
    }
}