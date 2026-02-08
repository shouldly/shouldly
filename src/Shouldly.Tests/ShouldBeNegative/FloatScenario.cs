namespace Shouldly.Tests.ShouldBeNegative;

public class FloatScenario
{
    [Fact]
    public void FloatScenarioShouldFail()
    {
        var @float = 3f;
        Verify.ShouldFail(() =>
            @float.ShouldBeNegative("Some additional context"));
    }

    [Fact]
    public void ShouldPass()
    {
        (-7f).ShouldBeNegative();
    }
}