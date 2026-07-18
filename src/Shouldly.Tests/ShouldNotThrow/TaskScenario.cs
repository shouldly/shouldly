namespace Shouldly.Tests.ShouldNotThrow;

public class TaskScenario
{
    [Fact]
    [UseCulture("en-US")]
    public void TaskScenarioShouldFail()
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