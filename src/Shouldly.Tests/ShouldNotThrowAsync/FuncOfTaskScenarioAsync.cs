namespace Shouldly.Tests.ShouldNotThrowAsync;

public class FuncOfTaskScenarioAsync
{
    [Fact]
    public async Task ShouldThrowAWobbly()
    {
        try
        {
            var task = Task.Run(() => throw new InvalidOperationException("exception message"), TestContext.Current.CancellationToken);

            await task.ShouldNotThrowAsync("Some additional context");
        }
        catch (ShouldAssertException ex)
        {
            ex.Message.ShouldContainWithoutWhitespace(
                """
                `await task` should not throw but threw System.InvalidOperationException with message "exception message"
                Additional Info: Some additional context
                """);
        }
    }

    [Fact]
    public async Task ShouldThrowAWobbly_WithNestedTasks()
    {
        try
        {
            var task = Task.Run(() => {
                var child1 = Task.Run(() => {
                    var child2 = Task.Run(() =>
                        throw new InvalidOperationException());
                    throw new InvalidOperationException();
                });
            }, TestContext.Current.CancellationToken);

            await task.ShouldNotThrowAsync("Some additional context");
        }
        catch (ShouldAssertException ex)
        {
            ex.Message.ShouldContainWithoutWhitespace(
                """
                `await task`
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
        var task = Task.Run(() => { }, TestContext.Current.CancellationToken);

        await task.ShouldNotThrowAsync();
    }
}