namespace Shouldly.Tests.ShouldContain;

public class EmptyArrayScenario
{
    [Fact]
    public void EmptyArrayScenarioShouldFail()
    {
        var target = new int[0];
        Verify.ShouldFail(() =>
            target.ShouldContain(1, "Some additional context"));
    }
}