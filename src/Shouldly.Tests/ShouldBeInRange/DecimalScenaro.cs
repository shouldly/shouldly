namespace Shouldly.Tests.ShouldBeInRange;

public class DecimalScenario
{
    [Fact]
    [UseCulture("en-US")]
    public void DecimalScenarioShouldFail()
    {
        var val = 1.5m;
        Verify.ShouldFail(() =>
            val.ShouldBeInRange(1.6m, 1.7m, "Some additional context"));
    }

    [Fact]
    public void ShouldPass()
    {
        1.5m.ShouldBeInRange(1.4m, 1.6m);
    }
}