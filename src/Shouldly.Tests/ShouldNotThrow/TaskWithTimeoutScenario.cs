namespace Shouldly.Tests.ShouldNotThrow;

public class TaskWithTimeoutScenario
{
    [Fact]
    public void ShouldThrowAWobbly()
    {
        var tcs = new TaskCompletionSource<object?>(TaskCreationOptions.RunContinuationsAsynchronously);
        var perpetualTask = tcs.Task;

        var ex = Should.Throw<ShouldCompleteInException>(() => perpetualTask.ShouldNotThrow(TimeSpan.FromSeconds(0.5), "Some additional context"));
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
        var completedTask = tcs.Task;

        completedTask.ShouldNotThrow(TimeSpan.FromSeconds(2));
    }
}