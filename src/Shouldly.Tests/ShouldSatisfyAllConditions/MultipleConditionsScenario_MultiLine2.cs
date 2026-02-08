namespace Shouldly.Tests.ShouldSatisfyAllConditions;

public class MultipleConditionsScenario_MultiLine2
{
    [Fact]
    public void MultipleConditionsScenario_MultiLine2ShouldFail()
    {
        var result = 4;
        Verify.ShouldFail(() =>
            result.ShouldSatisfyAllConditions(
                () => result.ShouldBeOfType<float>("Some additional context"),
                () => result.ShouldBeGreaterThan(5, "Some additional context")));
    }

    [Fact]
    public void ShouldPass()
    {
        var result = 4;
        result.ShouldSatisfyAllConditions(
            ()
                => result
                    .ShouldBeOfType<int>(),
            ()
                =>
                result
                    .ShouldBeGreaterThan(3));
    }
}