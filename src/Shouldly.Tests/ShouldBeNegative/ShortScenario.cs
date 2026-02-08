namespace Shouldly.Tests.ShouldBeNegative;

public class ShortScenario
{
    [Fact]
    public void ShortScenarioShouldFail()
    {
        var @short = (short)3;
        Verify.ShouldFail(() =>
            @short.ShouldBeNegative("Some additional context"));
    }

    [Fact]
    public void ShouldPass()
    {
        ((short)-7).ShouldBeNegative();
    }
}