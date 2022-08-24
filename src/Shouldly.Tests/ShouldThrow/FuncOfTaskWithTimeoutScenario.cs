namespace Shouldly.Tests.ShouldThrow;

public class FuncOfTaskWithTimeoutScenario
{
    [Fact]
    public void ShouldThrowAWobbly()
    {
        var task = Task.Factory.StartNew(() => { Task.Delay(5000).Wait(); },
            CancellationToken.None, TaskCreationOptions.None,
            TaskScheduler.Default);

        var ex = Should.Throw<ShouldCompleteInException>(() => task.ShouldThrow<ShouldCompleteInException>(TimeSpan.FromSeconds(0.5), "Some additional context"));

        ex.Message.ShouldContainWithoutWhitespace(ChuckedAWobblyErrorMessage);
    }

    [Fact]
    public void ShouldThrowAWobbly_ExceptionTypePassedIn()
    {
        var task = Task.Factory.StartNew(() => { Task.Delay(5000).Wait(); },
            CancellationToken.None, TaskCreationOptions.None,
            TaskScheduler.Default);

        var ex = Should.Throw(() => task.ShouldThrow(TimeSpan.FromSeconds(0.5), "Some additional context", typeof(ShouldCompleteInException)), typeof(ShouldCompleteInException));

        ex.Message.ShouldContainWithoutWhitespace(ChuckedAWobblyErrorMessage);
    }

    private string ChuckedAWobblyErrorMessage = @"Task
        should complete in
    00:00:00.5000000
        but did not
    Additional Info:
    Some additional context";

    [Fact]
    public void ShouldPass()
    {
        var task = Task.Factory.StartNew(() => throw new InvalidOperationException(),
            CancellationToken.None, TaskCreationOptions.None,
            TaskScheduler.Default);

        var ex = task.ShouldThrow<InvalidOperationException>(TimeSpan.FromSeconds(10));

        ex.ShouldNotBe(null);
        ex.ShouldBeOfType<InvalidOperationException>();
    }

    [Fact]
    public void ShouldPass_ExceptionTypePassedIn()
    {
        var task = Task.Factory.StartNew(() => throw new InvalidOperationException(),
            CancellationToken.None, TaskCreationOptions.None,
            TaskScheduler.Default);

        var ex = task.ShouldThrow(TimeSpan.FromSeconds(10), typeof(InvalidOperationException));

        ex.ShouldNotBe(null);
        ex.ShouldBeOfType<InvalidOperationException>();
    }
}