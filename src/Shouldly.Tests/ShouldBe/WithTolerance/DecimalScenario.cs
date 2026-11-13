namespace Shouldly.Tests.ShouldBe.WithTolerance;

public class DecimalScenario
{
    [Fact]
    [UseCulture("en-US")]
    public void DecimalScenarioShouldFail()
    {
        const decimal pi = (decimal)MathEx.PI;
        Verify.ShouldFail(() =>
            pi.ShouldBe(3.24m, 0.01m, "Some additional context"));
    }

    [Fact]
    public void ShouldPass()
    {
        const decimal pi = (decimal)MathEx.PI;
        pi.ShouldBe(3.14m, 0.01m);
    }

    [Fact]
    public void ShouldPassWithZeroTolerance()
    {
        const decimal pi = (decimal)MathEx.PI;
        pi.ShouldBe(pi, 0);
    }

    // The difference is beyond what a decimal holds, so beyond any tolerance: the assertion fails rather than
    // overflowing.
    [Fact]
    public void ValuesTooFarApartToSubtractShouldFail()
    {
        Should.Throw<ShouldAssertException>(() => decimal.MaxValue.ShouldBe(decimal.MinValue, 1m));
        Should.Throw<ShouldAssertException>(() => decimal.MinValue.ShouldBe(decimal.MaxValue, decimal.MaxValue));
    }
}
