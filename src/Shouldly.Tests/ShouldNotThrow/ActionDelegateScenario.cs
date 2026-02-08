namespace Shouldly.Tests.ShouldNotThrow;

public class ActionDelegateScenario
{
    [Fact]
    [UseCulture("en-US")]
    public void ActionDelegateScenarioShouldFail()
    {
        Verify.ShouldFail(() =>
            Should.NotThrow(new Action(() => throw new InvalidOperationException()), "Some additional context"));
    }

    [Fact]
    public void ShouldPass()
    {
        var action = new Action(() => { });
        action.ShouldNotThrow();
    }
}