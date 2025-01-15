namespace Shouldly.Tests.ShouldNotThrow;

public class TaskOfTScenario
{
    [Fact]
    [UseCulture("en-US")]
    public void TaskOfTScenarioShouldFail()
    {
        var task = Task.Run(() => throw new RankException());

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
        var task = Task.Run(() => "foo");

        var result = task.ShouldNotThrow();
        result.ShouldBe("foo");
    }
}