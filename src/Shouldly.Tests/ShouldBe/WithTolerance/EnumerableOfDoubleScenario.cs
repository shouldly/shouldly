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

    [Fact]
    public void ShouldPass()
    {
        new[] { MathEx.PI, MathEx.PI }.ShouldBe([3.14, 3.14], 0.01);
    }
}