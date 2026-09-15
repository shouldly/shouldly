namespace Shouldly.Tests.ShouldNotThrowAsync;

public class FuncOfTaskScenarioAsync
{
    [Fact]
    public async Task ShouldThrowAWobbly()
    {
        try
        {
            Func<Task> action = () => Task.Run(() => throw new InvalidOperationException("exception message"), TestContext.Current.CancellationToken);

            await action.ShouldNotThrowAsync("Some additional context");
        }
        catch (ShouldAssertException ex)
        {
            ex.Message.ShouldContainWithoutWhitespace(
                """
                `action` should not throw but threw System.InvalidOperationException with message "exception message"
                Additional Info: Some additional context
                """);
        }
    }

    [Fact]
    public async Task ShouldThrowAWobbly_WithNestedTasks()
    {
        try
        {
            Func<Task> action = () => Task.Run(() => {
                var child1 = Task.Run(() => {
                    var child2 = Task.Run(() =>
                        throw new InvalidOperationException());
                    throw new InvalidOperationException();
                });
            }, TestContext.Current.CancellationToken);

            await action.ShouldNotThrowAsync("Some additional context");
        }
        catch (ShouldAssertException ex)
        {
            ex.Message.ShouldContainWithoutWhitespace(
                """
                `action`
                should not throw but threw
                System.AggregateException
                """);
            ex.Message.ShouldContainWithoutWhitespace(
                """
                Additional Info:
                Some additional context
                """);
        }
    }

    [Fact]
    public async Task ShouldPass()
    {
        Func<Task> action = () => Task.Run(() => { }, TestContext.Current.CancellationToken);

        await action.ShouldNotThrowAsync();
    }

    [Fact]
    public async Task ShouldThrowAWobbly_WhenTaskIsCanceled()
    {
        Func<Task> action = () => Task.FromCanceled(new CancellationToken(canceled: true));

        var ex = await Shouldly.Should.ThrowAsync<ShouldAssertException>(() => action.ShouldNotThrowAsync("Some additional context"));

        ex.Message.ShouldContainWithoutWhitespace(
            """
            `action`
            should not throw but threw
            System.Threading.Tasks.TaskCanceledException
            """);
        ex.Message.ShouldContainWithoutWhitespace(
            """
            Additional Info:
            Some additional context
            """);
    }
}