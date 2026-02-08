namespace Shouldly.Tests.ShouldBeNegative;

public class ZeroShortScenario
{
    [Fact]
    public void ZeroLongScenarioShouldFail()
    {
        short val = 0;
        Verify.ShouldFail(() =>
            val.ShouldBeNegative("Some additional context"));
    }
}