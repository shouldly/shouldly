namespace Shouldly.Tests.ShouldNotBe;

public class BoolScenario
{
    [Fact]
    public void BoolScenarioShouldFail()
    {
        const bool myFalseValue = false;
        Verify.ShouldFail(() =>
            myFalseValue.ShouldNotBe(false, "Some additional context"));
    }

    [Fact]
    public void ShouldPass()
    {
        false.ShouldNotBe(true);
    }
}