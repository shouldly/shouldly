namespace Shouldly.Tests.ShouldThrow;

public class FuncOfTaskOfStringThrowsDifferentExceptionScenario
{
    [Fact]
    public void FuncOfTaskOfStringThrowsDifferentExceptionScenarioShouldFail()
    {
        var action = new Func<Task<string>>(() => throw new RankException());

        Verify.ShouldFail(() =>
                action.ShouldThrow<InvalidOperationException>("Some additional context"),

            errorWithSource:
            """
            Task `action`
                should throw
            System.InvalidOperationException
                but threw
            System.RankException

            Additional Info:
                Some additional context
            """,

            errorWithoutSource:
            """
            Task
                should throw
            System.InvalidOperationException
                but threw
            System.RankException

            Additional Info:
                Some additional context
            """);
    }

    [Fact]
    public void FuncOfTaskOfStringThrowsDifferentExceptionScenarioShouldFail_ExceptionTypePassedIn()
    {
        var action = new Func<Task<string>>(() => throw new RankException());

        Verify.ShouldFail(() =>
                action.ShouldThrow("Some additional context", typeof(InvalidOperationException)),

            errorWithSource:
            """
            Task `action`
                should throw
            System.InvalidOperationException
                but threw
            System.RankException

            Additional Info:
                Some additional context
            """,

            errorWithoutSource:
            """
            Task
                should throw
            System.InvalidOperationException
                but threw
            System.RankException

            Additional Info:
                Some additional context
            """);
    }

    [Fact]
    public void ShouldPass()
    {
        var task = Task.Run(() => throw new InvalidOperationException());

        var ex = task.ShouldThrow<InvalidOperationException>();

        ex.ShouldNotBe(null);
        ex.ShouldBeOfType<InvalidOperationException>();
    }

    [Fact]
    public void ShouldPass_ExceptionTypePassedIn()
    {
        var task = Task.Run(() => throw new InvalidOperationException());

        var ex = task.ShouldThrow(typeof(InvalidOperationException));

        ex.ShouldNotBe(null);
        ex.ShouldBeOfType<InvalidOperationException>();
    }
}