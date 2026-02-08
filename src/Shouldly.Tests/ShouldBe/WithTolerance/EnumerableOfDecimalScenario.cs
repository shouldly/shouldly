namespace Shouldly.Tests.ShouldBe.WithTolerance;

public class EnumerableOfDecimalScenario
{
    [Fact]
    [UseCulture("en-US")]
    public void EnumerableOfDecimalScenarioShouldFail()
    {
        var firstSet = new[] { 1.23m, 2.34m, 3.45001m };
        var secondSet = new[] { 1.4301m, 2.34m, 3.45m };
        Verify.ShouldFail(() =>
            firstSet.ShouldBe(secondSet, 0.1m, "Some additional context"));
    }

    [Fact]
    public void ShouldPass()
    {
        var firstSet = new[] { 1.23m, 2.34m, 3.45001m };
        var secondSet = new[] { 1.2301m, 2.34m, 3.45m };
        firstSet.ShouldBe(secondSet, 0.01m);
    }
}