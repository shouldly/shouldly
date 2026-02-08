namespace Shouldly.Tests.ShouldNotThrow;

public class FuncDelegateScenario
{
    [Fact]
    [UseCulture("en-US")]
    public void FuncDelegateScenarioShouldFail()
    {
        var action = new Func<int>(() => throw new InvalidOperationException());
        Verify.ShouldFail(() =>
            action.ShouldNotThrow("Some additional context"));
    }

    [Fact]
    public void ShouldPass()
    {
        var action = new Func<int>(() => 1);
        action.ShouldNotThrow().ShouldBe(1);
    }
}