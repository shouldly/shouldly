namespace Shouldly.Tests.ShouldSatisfyAllConditions;

public class MultipleConditionsScenario
{
    [Fact]
    public void MultipleConditionsScenarioShouldFail()
    {
        var result = 4;
        Verify.ShouldFail(() =>
            result.ShouldSatisfy(
                [
                    () => result.ShouldBeOfType<float>("Some additional context"),
                    () => result.ShouldBeGreaterThan(5, "Some additional context")
                ],
                "Some additional context"));
    }

    [Fact]
    public void ShouldPass()
    {
        var result = 4;
        result.ShouldSatisfy(
        [
            () => result.ShouldBeOfType<int>(),
            () => result.ShouldBeGreaterThan(3)
        ]);
    }
}