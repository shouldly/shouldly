namespace Shouldly.Tests.ShouldNotThrow;

public class FuncOfTaskOfStringScenario
{
    [Fact]
    [UseCulture("en-US")]
    public void FuncOfTaskOfStringScenarioShouldFail()
    {
        var task = Task.Run(() => throw new RankException(), TestContext.Current.CancellationToken);
        Verify.ShouldFail(() =>
                task.ShouldNotThrow("Some additional context"),

            errorWithSource:
            """
            Task `task`
                should not throw but threw
            System.RankException
                with message
            "Attempted to operate on an array with the incorrect number of dimensions."

            Additional Info:
                Some additional context
            """,

            errorWithoutSource:
            """
            Task
                should not throw but threw
            System.RankException
                with message
            "Attempted to operate on an array with the incorrect number of dimensions."

            Additional Info:
                Some additional context
            """);
    }

    [Fact]
    public void ShouldPass()
    {
        var task = Task.Run(() => "Foo");

        var result = task.ShouldNotThrow();
        result.ShouldBe("Foo");
    }
}