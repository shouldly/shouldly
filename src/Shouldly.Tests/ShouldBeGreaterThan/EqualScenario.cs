namespace Shouldly.Tests.ShouldBeGreaterThan;

public class EqualScenario
{
    [Fact]
    public void EqualScenarioShouldFail()
    {
        var one = 1;
        Verify.ShouldFail(() =>
            one.ShouldBeGreaterThan(1, "Some additional context"));
    }

    [Fact]
    public void ShouldPass()
    {
        7.ShouldBeGreaterThan(1);
    }
}