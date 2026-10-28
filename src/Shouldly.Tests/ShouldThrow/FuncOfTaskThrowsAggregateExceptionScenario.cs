namespace Shouldly.Tests.ShouldThrow;

public class FuncOfTaskThrowsAggregateExceptionScenario
{
    public static TheoryData<string> Shapes => ["thrown synchronously", "faulting the task"];

    private static Func<Task> Throwing(string shape, AggregateException exception) =>
        shape == "thrown synchronously"
            ? () => throw exception
            : () => Task.FromException(exception);

    [Theory]
    [MemberData(nameof(Shapes))]
    public void ShouldMatchTheAggregateException(string shape)
    {
        var expected = new AggregateException(new InvalidOperationException());
        var action = Throwing(shape, expected);

        action.ShouldThrow<AggregateException>().ShouldBeSameAs(expected);
        action.ShouldThrow<AggregateException>(TimeSpan.FromSeconds(10)).ShouldBeSameAs(expected);
        Should.Throw<AggregateException>(action).ShouldBeSameAs(expected);
    }

    [Theory]
    [MemberData(nameof(Shapes))]
    public void ShouldMatchTheAggregateException_ExceptionTypePassedIn(string shape)
    {
        var expected = new AggregateException(new InvalidOperationException());
        var action = Throwing(shape, expected);

        action.ShouldThrow(typeof(AggregateException)).ShouldBeSameAs(expected);
        action.ShouldThrow(TimeSpan.FromSeconds(10), typeof(AggregateException)).ShouldBeSameAs(expected);
        Should.Throw(action, typeof(AggregateException)).ShouldBeSameAs(expected);
    }

    [Theory]
    [MemberData(nameof(Shapes))]
    public void ShouldNotMatchTheInnerException(string shape)
    {
        var action = Throwing(shape, new AggregateException(new InvalidOperationException()));

        Should.Throw<ShouldAssertException>(() => action.ShouldThrow<InvalidOperationException>());
        Should.Throw<ShouldAssertException>(() => action.ShouldThrow(typeof(InvalidOperationException)));
    }
}
