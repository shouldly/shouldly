namespace Shouldly.Tests.ShouldThrow;

public class TaskWithTimeoutScenario
{
    [Fact]
    public void ShouldThrowAWobbly()
    {
        var tcs = new TaskCompletionSource<object?>(TaskCreationOptions.RunContinuationsAsynchronously);
        var perpetualTask = tcs.Task;
        
        var ex = Should.Throw<ShouldCompleteInException>(() => perpetualTask.ShouldThrow<InvalidOperationException>(TimeSpan.FromSeconds(0.5), "Some additional context"));
        ex.Message.ShouldContainWithoutWhitespace(ChuckedAWobblyErrorMessage);
    }

    [Fact]
    public void ShouldThrowAWobbly_ExceptionTypePassedIn()
    {
        var tcs = new TaskCompletionSource<object?>(TaskCreationOptions.RunContinuationsAsynchronously);
        var perpetualTask = tcs.Task;

        var ex = Should.Throw(() => perpetualTask.ShouldThrow<InvalidOperationException>(TimeSpan.FromSeconds(0.5), "Some additional context"), typeof(ShouldCompleteInException));
        ex.Message.ShouldContainWithoutWhitespace(ChuckedAWobblyErrorMessage);
    }

    private string ChuckedAWobblyErrorMessage = """
                                                
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
        tcs.SetException(new InvalidOperationException());
        var faultedTask = tcs.Task;

        var ex = faultedTask.ShouldThrow<InvalidOperationException>(TimeSpan.FromSeconds(2));
        ex.ShouldNotBe(null);
        ex.ShouldBeOfType<InvalidOperationException>();
    }

    [Fact]
    public void ShouldPass_ExceptionTypePassedIn()
    {
        var tcs = new TaskCompletionSource<object?>(TaskCreationOptions.RunContinuationsAsynchronously);
        tcs.SetException(new InvalidOperationException());
        var faultedTask = tcs.Task;

        var ex = faultedTask.ShouldThrow(TimeSpan.FromSeconds(2), typeof(InvalidOperationException));
        ex.ShouldNotBe(null);
        ex.ShouldBeOfType<InvalidOperationException>();
    }
}