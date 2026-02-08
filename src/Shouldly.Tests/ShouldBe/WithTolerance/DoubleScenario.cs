namespace Shouldly.Tests.ShouldBe.WithTolerance;

public class DoubleScenario
{
    [Fact]
    [UseCulture("en-US")]
    public void DoubleScenarioShouldFail()
    {
        const double pi = MathEx.PI;
        Verify.ShouldFail(() =>
            pi.ShouldBe(3.24d, 0.01d, "Some additional context"));
    }

    [Fact]
    public void ShouldPass()
    {
        const double pi = MathEx.PI;
        pi.ShouldBe(3.14d, 0.01d);
    }

    [Fact]
    public void ShouldPassWithZeroTolerance()
    {
        const double pi = MathEx.PI;
        pi.ShouldBe(pi, 0);
    }
}