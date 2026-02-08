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
        });
    }
}