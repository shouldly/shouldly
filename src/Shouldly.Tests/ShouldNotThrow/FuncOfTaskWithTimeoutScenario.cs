namespace Shouldly.Tests.ShouldNotThrow;

public class FuncOfTaskWithTimeoutScenario
{
    [Fact]
    public void ShouldThrowAWobbly()
    {
        var tcs = new TaskCompletionSource<object?>(TaskCreationOptions.RunContinuationsAsynchronously);
        Func<Task> perpetual = () => tcs.Task;

        var ex = Should.Throw<ShouldCompleteInException>(() =>
            perpetual.ShouldNotThrow(TimeSpan.FromSeconds(0.5), "Some additional context"));

        ex.Message.ShouldContainWithoutWhitespace(ChuckedAWobblyErrorMessage);
    }

    private string ChuckedAWobblyErrorMessage =
        """
        
            Task
                should complete in
            00:00:00.5000000
                but did not
            Additional Info:
            Some additional context
        """;

    [Fact]
    public void ShouldPass()
    {
        var tcs = new TaskCompletionSource<object?>(TaskCreationOptions.RunContinuationsAsynchronously);
        tcs.SetResult(null);
        Func<Task> completed = () => tcs.Task;

        completed.ShouldNotThrow(TimeSpan.FromSeconds(2));
    }
}
