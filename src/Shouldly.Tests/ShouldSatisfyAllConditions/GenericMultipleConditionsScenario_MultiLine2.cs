namespace Shouldly.Tests.ShouldSatisfyAllConditions;

public class GenericMultipleConditionsScenario_MultiLine2
{
    [Fact]
    public void GenericMultipleConditionsScenario_MultiLine2ShouldFail()
    {
        var result = 4;
        Verify.ShouldFail(() =>
            result.ShouldSatisfyAllConditions(
                r => r.ShouldBeOfType<float>("Some additional context"),
                r => r.ShouldBeGreaterThan(5, "Some additional context")));
    }

    [Fact]
    public void ShouldPass()
    {
        var result = 4;
        result.ShouldSatisfyAllConditions(
            r
                => r
                    .ShouldBeOfType<int>(),
            r
                =>
                r
                    .ShouldBeGreaterThan(3));
    }
}