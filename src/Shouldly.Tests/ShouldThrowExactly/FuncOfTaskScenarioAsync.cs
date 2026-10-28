namespace Shouldly.Tests.ShouldThrowExactly;

public class FuncOfTaskScenarioAsync
{
    [Fact]
    public async Task ShouldPassWhenExactTypeIsThrown()
    {
        var expected = new ArgumentException();
        Func<Task> action = async () =>
        {
            await Task.Yield();
            throw expected;
        };

        ArgumentException ex = await action.ShouldThrowExactlyAsync<ArgumentException>();

        ex.ShouldBeSameAs(expected);
    }

    [Fact]
    public async Task ShouldPassWhenExactTypeIsThrown_Static()
    {
        var expected = new ArgumentException();

        var ex = await Should.ThrowExactlyAsync<ArgumentException>(() => Task.FromException<string>(expected));

        ex.ShouldBeSameAs(expected);
    }

    [Fact]
    public async Task DerivedExceptionShouldFail()
    {
        Func<Task> action = () => Task.FromException(new ArgumentNullException());

        var ex = await Should.ThrowAsync<ShouldAssertException>(() =>
            action.ShouldThrowExactlyAsync<ArgumentException>("Some additional context"));

        ex.Message.ShouldMatchApproved(c => c.NoDiff());
    }

    [Fact]
    public async Task ShouldThrowExactlyAsync()
    {
        #region ShouldThrowExactlyAsync

        Task<int> registerAsync(string name) => Task.FromException<int>(new ArgumentNullException(nameof(name)));
        var exception = await Should.ThrowExactlyAsync<ArgumentNullException>(() => registerAsync(null!));
        exception.ParamName.ShouldBe("name");
        #endregion
    }

    [Fact]
    public async Task DerivedExceptionShouldFail_Static()
    {
        Task DoSomething() => Task.FromException(new ArgumentNullException());

        var ex = await Should.ThrowAsync<ShouldAssertException>(() =>
            Should.ThrowExactlyAsync<ArgumentException>(() => DoSomething()));

        ex.Message.ShouldMatchApproved(c => c.NoDiff());
    }

    [Fact]
    public async Task NotThrowingShouldFail()
    {
        Func<Task> action = () => Task.CompletedTask;

        var ex = await Should.ThrowAsync<ShouldAssertException>(() =>
            action.ShouldThrowExactlyAsync<InvalidOperationException>("Some additional context"));

        ex.Message.ShouldMatchApproved(c => c.NoDiff());
    }

    [Fact]
    public async Task AggregateExceptionFaultingTheTaskShouldNotBeUnwrapped()
    {
        var expected = new AggregateException(new ArgumentException());
        Func<Task> action = () => Task.FromException(expected);

        (await action.ShouldThrowExactlyAsync<AggregateException>()).ShouldBeSameAs(expected);
        await Should.ThrowAsync<ShouldAssertException>(() => action.ShouldThrowExactlyAsync<ArgumentException>());
    }
}