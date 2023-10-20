namespace Shouldly.Tests.ShouldNotThrow;

public class FuncOfTaskOfStringScenario
{
    [Fact]
    [UseCulture("en-US")]
    public void FuncOfTaskOfStringScenarioShouldFail()
    {
        var task = Task.Factory.StartNew<string>(() => throw new RankException(),
            CancellationToken.None, TaskCreationOptions.None,
            TaskScheduler.Default);
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
        var task = Task.Factory.StartNew(() => "Foo",
            CancellationToken.None, TaskCreationOptions.None,
            TaskScheduler.Default);

        var result = task.ShouldNotThrow();
        result.ShouldBe("Foo");
    }
}