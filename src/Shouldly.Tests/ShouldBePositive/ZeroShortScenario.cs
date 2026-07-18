namespace Shouldly.Tests.ShouldBePositive;

public class ZeroShortScenario
{
    [Fact]
    public void ZeroShortScenarioShouldFail()
    {
        short val = 0;
        Verify.ShouldFail(() =>
            val.ShouldBePositive("Some additional context"));
    }
}