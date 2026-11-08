namespace Shouldly.Tests.ShouldNotThrowAsync;

public class FuncOfTaskOfTScenarioAsync
{
    [Fact]
    public async Task ShouldThrowAWobbly()
    {
        Func<Task<string>> action = () => Task.Run(string () => throw new InvalidOperationException("exception message"), TestContext.Current.CancellationToken);

        var ex = await Should.ThrowAsync<ShouldAssertException>(() => Should.NotThrowAsync(action, "Some additional context"));

        ex.Message.ShouldContainWithoutWhitespace(
            """
            `action` should not throw but threw System.InvalidOperationException with message "exception message"
            Additional Info: Some additional context
            """);
    }

    [Fact]
    public async Task ShouldPassAndReturnTheResult()
    {
        Func<Task<string>> action = () => Task.Run(() => "foo", TestContext.Current.CancellationToken);

        string result = await Should.NotThrowAsync(action);

        result.ShouldBe("foo");
    }

    [Fact]
    public async Task ExtensionShouldPassAndReturnTheResult()
    {
        Func<Task<string>> action = () => Task.Run(() => "foo", TestContext.Current.CancellationToken);

        string result = await action.ShouldNotThrowAsync();

        result.ShouldBe("foo");
    }

    [Fact]
    public async Task ShouldReturnTheResultOfAMethodGroup()
    {
        var result = await Should.NotThrowAsync(GetValueAsync);

        result.ShouldBe(42);
    }

    private static async Task<int> GetValueAsync()
    {
        await Task.Yield();
        return 42;
    }
}
