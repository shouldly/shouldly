namespace Shouldly.Tests.ShouldNotThrowAsync;

public class FuncOfTaskScenarioAsync
{
    [Fact]
    public async Task ShouldThrowAWobbly()
    {
        try
        {
            var task = Task.Factory.StartNew(() => throw new InvalidOperationException("exception message"),
                CancellationToken.None, TaskCreationOptions.None,
                TaskScheduler.Default);

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
            var task = Task.Factory.StartNew(() => {
                var child1 = Task.Factory.StartNew(() => {
                    var child2 = Task.Factory.StartNew(() =>
                        throw new InvalidOperationException(), TaskCreationOptions.AttachedToParent);
                    throw new InvalidOperationException();
                }, TaskCreationOptions.AttachedToParent);
            });

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
        var task = Task.Factory.StartNew(() => { },
            CancellationToken.None, TaskCreationOptions.None,
            TaskScheduler.Default);

        await task.ShouldNotThrowAsync();
    }
}