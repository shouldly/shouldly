namespace Shouldly.Tests.ShouldBePositive;

public class ShortScenario
{
    [Fact]
    public void ShortScenarioShouldFail()
    {
        var @short = (short)-3;
        Verify.ShouldFail(() =>
            @short.ShouldBePositive("Some additional context"));
    }

    [Fact]
    public void ShouldPass()
    {
        ((short)7).ShouldBePositive();
    }
}