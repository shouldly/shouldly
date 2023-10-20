namespace Shouldly.Tests.ShouldNotBePositive;

public class ShortScenario
{
    [Fact]
    public void ShortScenarioShouldFail()
    {
        var @short = (short)3;
        Verify.ShouldFail(() =>
                @short.ShouldNotBePositive("Some additional context"),

            errorWithSource:
            @"@short
    should not be positive but
3
    is positive

Additional Info:
    Some additional context",

            errorWithoutSource:
            @"3
    should not be positive but is positive

Additional Info:
    Some additional context");
    }

    [Fact]
    public void ShouldPass()
    {
        ((short)-7).ShouldNotBePositive();
    }
}