namespace Shouldly.Tests.ShouldThrow;

public class FuncOfTaskWithTimeoutScenario
{
    [Fact]
    public void ShouldThrowAWobbly()
    {
        var tcs = new TaskCompletionSource<object?>(TaskCreationOptions.RunContinuationsAsynchronously);
        Func<Task> perpetual = () => tcs.Task;

        var ex = Should.Throw<ShouldCompleteInException>(
            () => perpetual.ShouldThrow<ShouldCompleteInException>(TimeSpan.FromSeconds(0.1), "Some additional context"));

        ex.Message.ShouldContainWithoutWhitespace(ChuckedAWobblyErrorMessage);
    }

    [Fact]
    public void ShouldThrowAWobbly_ExceptionTypePassedIn()
    {
        var tcs = new TaskCompletionSource<object?>(TaskCreationOptions.RunContinuationsAsynchronously);
        Func<Task> perpetual = () => tcs.Task;

        var ex = Should.Throw(
            () => perpetual.ShouldThrow(
                TimeSpan.FromSeconds(0.1),
                "Some additional context",
                typeof(ShouldCompleteInException)),
            typeof(ShouldCompleteInException));

        ex.Message.ShouldContainWithoutWhitespace(ChuckedAWobblyErrorMessage);
    }

    private string ChuckedAWobblyErrorMessage =
        """
        Task
                should complete in
            00:00:00.1000000
                but did not
            Additional Info:
            Some additional context
        """;

    [Fact]
    public void ShouldPass()
    {
        var tcs = new TaskCompletionSource<object?>(TaskCreationOptions.RunContinuationsAsynchronously);
        tcs.SetException(new InvalidOperationException());
        Func<Task> faulted = () => tcs.Task;

        var ex = faulted.ShouldThrow<InvalidOperationException>(TimeSpan.FromSeconds(10));

        ex.ShouldNotBe(null);
        ex.ShouldBeOfType<InvalidOperationException>();
    }

    [Fact]
    public void ShouldPass_ExceptionTypePassedIn()
    {
        var tcs = new TaskCompletionSource<object?>(TaskCreationOptions.RunContinuationsAsynchronously);
        tcs.SetException(new InvalidOperationException());
        Func<Task> faulted = () => tcs.Task;

        var ex = faulted.ShouldThrow(TimeSpan.FromSeconds(10), typeof(InvalidOperationException));

        ex.ShouldNotBe(null);
        ex.ShouldBeOfType<InvalidOperationException>();
    }
}
