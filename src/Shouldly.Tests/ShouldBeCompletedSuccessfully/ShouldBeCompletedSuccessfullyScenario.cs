namespace Shouldly.Tests.ShouldBeCompletedSuccessfully;

public class ShouldBeCompletedSuccessfullyScenario
{
    [Fact]
    public void ShouldPassForCompletedTask()
    {
        Task.CompletedTask.ShouldBeCompletedSuccessfully();
    }

    [Fact]
    public void ShouldPassAndReturnResultForCompletedTaskOfT()
    {
        var task = Task.FromResult(42);

        task.ShouldBeCompletedSuccessfully().ShouldBe(42);
    }

    [Fact]
    public void ShouldFailForFaultedTask()
    {
        var task = Task.FromException(new InvalidOperationException("boom"));

        var ex = Should.Throw<ShouldAssertException>(() => task.ShouldBeCompletedSuccessfully());
        ex.Message.ShouldContain("task");
        ex.Message.ShouldContain(TaskStatus.RanToCompletion.ToString());
        ex.Message.ShouldContain(TaskStatus.Faulted.ToString());
    }

    [Fact]
    public void ShouldFailForFaultedTaskOfT()
    {
        var task = Task.FromException<int>(new InvalidOperationException("boom"));

        Should.Throw<ShouldAssertException>(() => task.ShouldBeCompletedSuccessfully());
    }

    [Fact]
    public void ShouldFailForCanceledTask()
    {
        var task = Task.FromCanceled(new CancellationToken(canceled: true));

        Should.Throw<ShouldAssertException>(() => task.ShouldBeCompletedSuccessfully());
    }

    [Fact]
    public void ShouldFailForIncompleteTask()
    {
        var tcs = new TaskCompletionSource<int>();

        Should.Throw<ShouldAssertException>(() => tcs.Task.ShouldBeCompletedSuccessfully());
    }
}
