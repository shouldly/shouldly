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
                    throw new("Dummy message.");
                }
            }, "Additional info");
        });
    }
}