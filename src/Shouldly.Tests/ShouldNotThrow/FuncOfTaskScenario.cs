namespace Shouldly.Tests.ShouldNotThrow;

public class FuncOfTaskScenario
{
    [Fact]
    [UseCulture("en-US")]
    public void FuncOfTaskScenarioShouldFail()
    {
        var task = Task.Run(() => throw new RankException(), TestContext.Current.CancellationToken);

        Verify.ShouldFail(() =>
            task.ShouldNotThrow("Some additional context"));
    }

    [Fact]
    public void ShouldPass()
    {
        var task = Task.Run(() => { }, TestContext.Current.CancellationToken);

        task.ShouldNotThrow();
    }
}