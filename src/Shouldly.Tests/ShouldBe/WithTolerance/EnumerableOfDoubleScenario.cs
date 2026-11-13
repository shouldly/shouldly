namespace Shouldly.Tests.ShouldBe.WithTolerance;

public class EnumerableOfDoubleScenario
{
    [Fact]
    [UseCulture("en-US")]
    public void EnumerableOfDoubleScenarioShouldFail()
    {
        Verify.ShouldFail(() =>
            new[] { MathEx.PI, MathEx.PI }.ShouldBe([3.24, 3.24], 0.01, "Some additional context"));
    }

    // Only the item outside the tolerance is marked: the message compares the items as the check did.
    [Fact]
    [UseCulture("en-US")]
    public void ItemsWithinTheToleranceAreNotMarkedAsDifferent()
    {
        Verify.ShouldFail(() =>
            new[] { 1.0, 2.005, 3.5 }.ShouldBe([1.0, 2.0, 3.0], 0.01, "Some additional context"));
    }

    [Fact]
    public void ShouldPass()
    {
        new[] { MathEx.PI, MathEx.PI }.ShouldBe([3.14, 3.14], 0.01);
    }
}