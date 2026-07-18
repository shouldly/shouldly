namespace Shouldly.Tests.ShouldThrow;

public class FuncOfTaskScenario
{
    [Fact]
    public void FuncOfTaskScenarioShouldFail()
    {
        var task = Task.Run(() => { }, TestContext.Current.CancellationToken);

        Verify.ShouldFail(() =>
            task.ShouldThrow<InvalidOperationException>("Some additional context"));
    }

    [Fact]
    public void FuncOfTaskScenarioShouldFail_ExceptionTypePassedIn()
    {
        var task = Task.Run(() => { }, TestContext.Current.CancellationToken);

        Verify.ShouldFail(() =>
            task.ShouldThrow("Some additional context", typeof(InvalidOperationException)));
    }

    [Fact]
    public void ShouldPass()
    {
        var task = Task.Run(() => throw new InvalidOperationException(), TestContext.Current.CancellationToken);

        var ex = task.ShouldThrow<InvalidOperationException>();

        ex.ShouldNotBe(null);
        ex.ShouldBeOfType<InvalidOperationException>();
    }

    [Fact]
    public void ShouldPass_ExceptionTypePassedIn()
    {
        var task = Task.Run(() => throw new InvalidOperationException(), TestContext.Current.CancellationToken);

        var ex = task.ShouldThrow(typeof(InvalidOperationException));

        ex.ShouldNotBe(null);
        ex.ShouldBeOfType<InvalidOperationException>();
    }
}