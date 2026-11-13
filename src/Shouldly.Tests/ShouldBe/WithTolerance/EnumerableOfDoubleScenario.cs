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
    [UseCulture("en-US")]
    public void NullActualShouldFail()
    {
        IEnumerable<double>? readings = null;
        Verify.ShouldFail(() =>
            readings!.ShouldBe([1.0, 2.0], 0.01, "Some additional context"));
    }

    // A null sequence fails the assertion, as it does without a tolerance, rather than throwing.
    [Fact]
    public void NullActualFailsForEveryTolerance()
    {
        Should.Throw<ShouldAssertException>(() => ((IEnumerable<double>)null!).ShouldBe([1.0], 0.01));
        Should.Throw<ShouldAssertException>(() => ((IEnumerable<float>)null!).ShouldBe([1f], 0.01));
        Should.Throw<ShouldAssertException>(() => ((IEnumerable<decimal>)null!).ShouldBe([1m], 0.01m));
        Should.Throw<ShouldAssertException>(() => ((IEnumerable<string>)null!).ShouldBe(["a"], Case.Insensitive));
        Should.Throw<ShouldAssertException>(() => new[] { 1.0 }.ShouldBe(null!, 0.01));
    }

    [Fact]
    public void ShouldPass()
    {
        new[] { MathEx.PI, MathEx.PI }.ShouldBe([3.14, 3.14], 0.01);
    }
}