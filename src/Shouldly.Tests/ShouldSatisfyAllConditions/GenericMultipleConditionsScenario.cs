namespace Shouldly.Tests.ShouldSatisfyAllConditions;

public class GenericMultipleConditionsScenario
{
    [Fact]
    public void GenericMultipleConditionsScenarioShouldFail()
    {
        var result = 4;
        Verify.ShouldFail(() =>
            result.ShouldSatisfyAllConditions(
                "Some additional context",
                r => r.ShouldBeOfType<float>("Some additional context"),
                r => r.ShouldBeGreaterThan(5, "Some additional context")));
    }

    [Fact]
    public void ShouldPass()
    {
        var result = 4;
        result.ShouldSatisfyAllConditions(
            _ => result.ShouldBeOfType<int>(),
            _ => result.ShouldBeGreaterThan(3));
    }
}