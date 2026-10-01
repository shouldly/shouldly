namespace Shouldly.Tests.ShouldNotThrow;

public class FuncOfTaskScenario
{
    [Fact]
    [UseCulture("en-US")]
    public void FuncOfTaskScenarioShouldFail()
    {
        Func<Task> action = () => Task.Run(() => throw new RankException(), TestContext.Current.CancellationToken);

        Verify.ShouldFail(() =>
            action.ShouldNotThrow("Some additional context"));
    }

    [Fact]
    public void ShouldPass()
    {
        Func<Task> action = () => Task.Run(() => { }, TestContext.Current.CancellationToken);

        action.ShouldNotThrow();
    }
}
