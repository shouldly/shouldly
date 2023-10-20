namespace Shouldly.Tests.ShouldThrow;

public class TaskScenario
{
    [Fact]
    public void TaskScenarioShouldFail()
    {
        var task = Task.Factory.StartNew(() => { },
            CancellationToken.None, TaskCreationOptions.None,
            TaskScheduler.Default);

        Verify.ShouldFail(() =>
                task.ShouldThrow<InvalidOperationException>("Some additional context"),

            errorWithSource:
            """
            Task `task`
                should throw
            System.InvalidOperationException
                but did not

            Additional Info:
                Some additional context
            """,

            errorWithoutSource:
            """
            Task
                should throw
            System.InvalidOperationException
                but did not

            Additional Info:
                Some additional context
            """);
    }

    [Fact]
    public void TaskScenarioShouldFail_ExceptionTypePassedIn()
    {
        var task = Task.Factory.StartNew(() => { },
            CancellationToken.None, TaskCreationOptions.None,
            TaskScheduler.Default);

        Verify.ShouldFail(() =>
                task.ShouldThrow("Some additional context", typeof(InvalidOperationException)),

            errorWithSource:
            """
            Task `task`
                should throw
            System.InvalidOperationException
                but did not

            Additional Info:
                Some additional context
            """,

            errorWithoutSource:
            """
            Task
                should throw
            System.InvalidOperationException
                but did not

            Additional Info:
                Some additional context
            """);
    }

    [Fact]
    public void ShouldPass()
    {
        var task = Task.Factory.StartNew(() => throw new InvalidOperationException(),
            CancellationToken.None, TaskCreationOptions.None,
            TaskScheduler.Default);

        var ex = task.ShouldThrow<InvalidOperationException>();

        ex.ShouldNotBe(null);
        ex.ShouldBeOfType<InvalidOperationException>();
    }

    [Fact]
    public void ShouldPass_ExceptionTypePassedIn()
    {
        var task = Task.Factory.StartNew(() => throw new InvalidOperationException(),
            CancellationToken.None, TaskCreationOptions.None,
            TaskScheduler.Default);

        var ex = task.ShouldThrow(typeof(InvalidOperationException));

        ex.ShouldNotBe(null);
        ex.ShouldBeOfType<InvalidOperationException>();
    }
}