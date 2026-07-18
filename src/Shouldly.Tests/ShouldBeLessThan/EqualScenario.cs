namespace Shouldly.Tests.ShouldBeLessThan;

public class EqualScenario
{
    [Fact]
    public void EqualScenarioShouldFail()
    {
        var one = 1;
        Verify.ShouldFail(() =>
            one.ShouldBeLessThan(1, "Some additional context"));
    }
}