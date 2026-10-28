namespace Shouldly.Tests.ShouldThrowExactly;

public class FuncOfTaskScenario
{
    [Fact]
    public void ShouldPassWhenExactTypeIsThrown()
    {
        var expected = new ArgumentException();
        Func<Task> action = () => Task.FromException(expected);

        ArgumentException ex = action.ShouldThrowExactly<ArgumentException>();

        ex.ShouldBeSameAs(expected);
    }

    [Fact]
    public void ShouldPassWhenExactTypeIsThrown_Static()
    {
        var expected = new ArgumentException();

        var ex = Should.ThrowExactly<ArgumentException>(() => Task.FromException(expected));

        ex.ShouldBeSameAs(expected);
    }

    [Fact]
    public void ShouldPassWhenExactTypeIsThrownWithinTimeout()
    {
        var expected = new ArgumentException();
        Func<Task> action = () => Task.FromException(expected);

        action.ShouldThrowExactly<ArgumentException>(TimeSpan.FromSeconds(10)).ShouldBeSameAs(expected);
        Should.ThrowExactly<ArgumentException>(action, TimeSpan.FromSeconds(10)).ShouldBeSameAs(expected);
    }

    [Fact]
    public void DerivedExceptionShouldFail()
    {
        Func<Task> action = () => Task.FromException(new ArgumentNullException());
        Verify.ShouldFail(() =>
            action.ShouldThrowExactly<ArgumentException>("Some additional context"));
    }

    [Fact]
    public void DerivedExceptionShouldFailWithinTimeout()
    {
        Func<Task> action = () => Task.FromException(new ArgumentNullException());
        Verify.ShouldFail(() =>
            action.ShouldThrowExactly<ArgumentException>(TimeSpan.FromSeconds(10), "Some additional context"));
    }

    [Fact]
    public void NotThrowingShouldFail()
    {
        Func<Task> action = () => Task.CompletedTask;
        Verify.ShouldFail(() =>
            action.ShouldThrowExactly<InvalidOperationException>("Some additional context"));
    }

    [Fact]
    public void TimeoutShouldNotBeReportedAsWrongException()
    {
        var tcs = new TaskCompletionSource<object?>(TaskCreationOptions.RunContinuationsAsynchronously);
        Func<Task> perpetual = () => tcs.Task;

        Should.Throw<ShouldCompleteInException>(
            () => perpetual.ShouldThrowExactly<InvalidOperationException>(TimeSpan.FromSeconds(0.1)));
    }
}
