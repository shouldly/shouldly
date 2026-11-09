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

        Verify.ShouldFail(() =>
            task.ShouldBeCompletedSuccessfully("Some additional context"));
    }

    [Fact]
    public void ShouldFailForFaultedTaskOfT()
    {
        var task = Task.FromException<int>(new InvalidOperationException("boom"));

        Verify.ShouldFail(() =>
            task.ShouldBeCompletedSuccessfully());
    }

    [Fact]
    public void ShouldFailForTaskFaultedWithMultipleExceptions()
    {
        var task = Task.WhenAll(
            Task.FromException(new InvalidOperationException("first")),
            Task.FromException(new ArgumentException("second")));

        Verify.ShouldFail(() =>
            task.ShouldBeCompletedSuccessfully());
    }

    [Fact]
    public void ShouldIncludeStackTraceOfThrownException()
    {
        var task = ThrowAsync();

        var ex = Should.Throw<ShouldAssertException>(() => task.ShouldBeCompletedSuccessfully());

        ex.Message.ShouldContain(nameof(ThrowAsync));
    }

    [Fact]
    public void ShouldFailForCanceledTask()
    {
        var task = Task.FromCanceled(new CancellationToken(canceled: true));

        Verify.ShouldFail(() =>
            task.ShouldBeCompletedSuccessfully());
    }

    [Fact]
    public void ShouldFailForIncompleteTask()
    {
        var tcs = new TaskCompletionSource<int>();

        Verify.ShouldFail(() =>
            tcs.Task.ShouldBeCompletedSuccessfully());
    }

    [Fact]
    public void ShouldFailForNullTask()
    {
        Task task = null!;

        Verify.ShouldFail(() =>
            task.ShouldBeCompletedSuccessfully());
    }

    [Fact]
    public void ShouldFailForNullTaskOfT()
    {
        Task<int> task = null!;

        Verify.ShouldFail(() =>
            task.ShouldBeCompletedSuccessfully());
    }

    static async Task ThrowAsync()
    {
        await Task.CompletedTask;
        throw new InvalidOperationException("boom");
    }
}
