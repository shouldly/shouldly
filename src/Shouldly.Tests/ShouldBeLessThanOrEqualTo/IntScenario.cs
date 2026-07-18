namespace Shouldly.Tests.ShouldBeLessThanOrEqualTo;

public class IntScenario
{
    [Fact]
    public void IntScenarioShouldFail()
    {
        var seven = 7;
        Verify.ShouldFail(() =>
            seven.ShouldBeLessThanOrEqualTo(1, "Some additional context"));
    }

    [Fact]
    public void ShouldPass()
    {
        1.ShouldBeLessThanOrEqualTo(1);
    }
}