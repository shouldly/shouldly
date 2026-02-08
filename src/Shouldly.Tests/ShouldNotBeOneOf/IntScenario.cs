namespace Shouldly.Tests.ShouldNotBeOneOf;

public class IntScenario
{
    [Fact]
    public void IntScenarioShouldFail()
    {
        var one = 1;
        Verify.ShouldFail(() =>
            one.ShouldNotBeOneOf(1, 2, 3));
    }

    [Fact]
    public void ShouldPass()
    {
        1.ShouldNotBeOneOf(4, 5, 6);
    }
}