namespace Shouldly.Tests.ShouldBeOneOf;

public class IntScenario
{
    [Fact]
    public void IntScenarioShouldFail()
    {
        var one = 1;
        Verify.ShouldFail(() =>
            one.ShouldBeOneOf(4, 5, 6));
    }

    [Fact]
    public void ShouldPass()
    {
        1.ShouldBeOneOf(1, 2, 3);
    }
}