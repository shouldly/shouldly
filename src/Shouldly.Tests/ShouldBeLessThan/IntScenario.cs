namespace Shouldly.Tests.ShouldBeLessThan;

public class IntScenario
{
    [Fact]
    public void IntScenarioShouldFail()
    {
        var seven = 7;
        Verify.ShouldFail(() =>
            seven.ShouldBeLessThan(1, "Some additional context"));
    }

    [Fact]
    public void ShouldPass()
    {
        1.ShouldBeLessThan(7);
    }
}