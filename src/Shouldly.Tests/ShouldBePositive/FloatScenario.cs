namespace Shouldly.Tests.ShouldBePositive;

public class FloatScenario
{
    [Fact]
    public void FloatScenarioShouldFail()
    {
        var @float = -3f;
        Verify.ShouldFail(() =>
            @float.ShouldBePositive("Some additional context"));
    }

    [Fact]
    public void ShouldPass()
    {
        7f.ShouldBePositive();
    }
}