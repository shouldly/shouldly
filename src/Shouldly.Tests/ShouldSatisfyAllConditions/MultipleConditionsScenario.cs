namespace Shouldly.Tests.ShouldSatisfyAllConditions;

public class MultipleConditionsScenario
{
    [Fact]
    public void MultipleConditionsScenarioShouldFail()
    {
        var result = 4;
        Verify.ShouldFail(() =>
            result.ShouldSatisfyAllConditions(
                "Some additional context",
                () => result.ShouldBeOfType<float>("Some additional context"),
                () => result.ShouldBeGreaterThan(5, "Some additional context")));
    }

    [Fact]
    public void ShouldPass()
    {
        var result = 4;
        result.ShouldSatisfyAllConditions(
            () => result.ShouldBeOfType<int>(),
            () => result.ShouldBeGreaterThan(3));
    }
}