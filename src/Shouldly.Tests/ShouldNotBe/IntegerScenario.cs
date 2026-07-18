namespace Shouldly.Tests.ShouldNotBe;

public class IntegerScenario
{
    [Fact]
    public void IntegerScenarioShouldFail()
    {
        const int one = 1;
        Verify.ShouldFail(() =>
            one.ShouldNotBe(1));
    }

    [Fact]
    public void ShouldPass()
    {
        1.ShouldNotBe(2);
    }
}