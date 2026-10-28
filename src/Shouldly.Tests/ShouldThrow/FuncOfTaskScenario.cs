namespace Shouldly.Tests.ShouldThrow;

public class FuncOfTaskScenario
{
    [Fact]
    public void FuncOfTaskScenarioShouldFail()
    {
        Func<Task> action = () => Task.Run(() => { }, TestContext.Current.CancellationToken);

        Verify.ShouldFail(() =>
            action.ShouldThrow<InvalidOperationException>("Some additional context"));
    }

    [Fact]
    public void FuncOfTaskScenarioShouldFail_ExceptionTypePassedIn()
    {
        Func<Task> action = () => Task.Run(() => { }, TestContext.Current.CancellationToken);

        Verify.ShouldFail(() =>
            action.ShouldThrow("Some additional context", typeof(InvalidOperationException)));
    }

    [Fact]
    public void ShouldPass()
    {
        Func<Task> action = () => Task.Run(() => throw new InvalidOperationException(), TestContext.Current.CancellationToken);

        var ex = action.ShouldThrow<InvalidOperationException>();

        ex.ShouldNotBe(null);
        ex.ShouldBeOfType<InvalidOperationException>();
    }

    [Fact]
    public void ShouldPass_ExceptionTypePassedIn()
    {
        Func<Task> action = () => Task.Run(() => throw new InvalidOperationException(), TestContext.Current.CancellationToken);

        var ex = action.ShouldThrow(typeof(InvalidOperationException));

        ex.ShouldNotBe(null);
        ex.ShouldBeOfType<InvalidOperationException>();
    }

    [Fact]
    public void ShouldPassWhenDerivedExceptionIsThrown_ExceptionTypePassedIn()
    {
        var expected = new ArgumentNullException();
        Func<Task> action = () => Task.FromException(expected);

        var ex = action.ShouldThrow(typeof(ArgumentException));

        ex.ShouldBeSameAs(expected);
    }

    [Fact]
    public void ShouldPassWhenCanceledTaskThrowsDerivedException()
    {
        var expected = new CustomCanceledException();

        async Task ThrowAsync()
        {
            await Task.Yield();
            throw expected;
        }

        var ex = Should.Throw<CustomCanceledException>(ThrowAsync);

        ex.ShouldBeSameAs(expected);
    }

    [Fact]
    public void ShouldPassWhenCanceledTaskThrowsDerivedException_ExceptionTypePassedIn()
    {
        var expected = new CustomCanceledException();

        async Task ThrowAsync()
        {
            await Task.Yield();
            throw expected;
        }

        var ex = Should.Throw(ThrowAsync, typeof(OperationCanceledException));

        ex.ShouldBeSameAs(expected);
    }

    private class CustomCanceledException : OperationCanceledException;
}
