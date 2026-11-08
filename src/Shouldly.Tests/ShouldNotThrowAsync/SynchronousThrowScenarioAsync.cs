namespace Shouldly.Tests.ShouldNotThrowAsync;

/// <summary>
/// A delegate that throws before returning its task (validation in a non-async method, for example)
/// should fail the assertion rather than escape it.
/// </summary>
public class SynchronousThrowScenarioAsync
{
    [Fact]
    public async Task FuncOfTaskShouldThrowAWobbly()
    {
        Func<Task> action = () => ValidateThenRun(-1);

        var ex = await Should.ThrowAsync<ShouldAssertException>(() => Should.NotThrowAsync(action, "Some additional context"));

        ex.Message.ShouldContainWithoutWhitespace(
            """
            `action` should not throw but threw System.InvalidOperationException with message "Value must not be negative."
            Additional Info: Some additional context
            """);
    }

    [Fact]
    public async Task FuncOfTaskExtensionShouldThrowAWobbly()
    {
        Func<Task> action = () => ValidateThenRun(-1);

        var ex = await Should.ThrowAsync<ShouldAssertException>(() => action.ShouldNotThrowAsync("Some additional context"));

        ex.Message.ShouldContainWithoutWhitespace(
            """
            `action` should not throw but threw System.InvalidOperationException with message "Value must not be negative."
            Additional Info: Some additional context
            """);
    }

    [Fact]
    public async Task FuncOfTaskOfTShouldThrowAWobbly()
    {
        Func<Task<int>> action = () => ValidateThenGet(-1);

        var ex = await Should.ThrowAsync<ShouldAssertException>(() => Should.NotThrowAsync(action, "Some additional context"));

        ex.Message.ShouldContainWithoutWhitespace(
            """
            `action` should not throw but threw System.InvalidOperationException with message "Value must not be negative."
            Additional Info: Some additional context
            """);
    }

    [Fact]
    public async Task FuncOfTaskOfTExtensionShouldThrowAWobbly()
    {
        Func<Task<int>> action = () => ValidateThenGet(-1);

        var ex = await Should.ThrowAsync<ShouldAssertException>(() => action.ShouldNotThrowAsync("Some additional context"));

        ex.Message.ShouldContainWithoutWhitespace(
            """
            `action` should not throw but threw System.InvalidOperationException with message "Value must not be negative."
            Additional Info: Some additional context
            """);
    }

    [Fact]
    public async Task FuncOfTaskOfTShouldPassAndReturnTheResult()
    {
        var result = await Should.NotThrowAsync(() => ValidateThenGet(42));

        result.ShouldBe(42);
    }

    // Deliberately not async: the exception is thrown before any task is returned.
    private static Task ValidateThenRun(int value)
    {
        if (value < 0)
            throw new InvalidOperationException("Value must not be negative.");

        return Task.CompletedTask;
    }

    private static Task<int> ValidateThenGet(int value)
    {
        if (value < 0)
            throw new InvalidOperationException("Value must not be negative.");

        return Task.FromResult(value);
    }
}
