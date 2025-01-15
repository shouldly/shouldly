namespace Shouldly.Tests.ShouldNotThrow;

public class FuncDelegateScenario
{
    [Fact]
    [UseCulture("en-US")]
    public void FuncDelegateScenarioShouldFail()
    {
        var action = new Func<int>(() => throw new InvalidOperationException());
        Verify.ShouldFail(() =>
                action.ShouldNotThrow("Some additional context"),

            errorWithSource:
            """
            `action()`
                should not throw but threw
            System.InvalidOperationException
                with message
            "Operation is not valid due to the current state of the object."

            Additional Info:
                Some additional context
            """,

            errorWithoutSource:
            """
            delegate
                should not throw but threw
            System.InvalidOperationException
                with message
            "Operation is not valid due to the current state of the object."

            Additional Info:
                Some additional context
            """);
    }

    [Fact]
    public void ShouldPass()
    {
        var action = new Func<int>(() => 1);
        action.ShouldNotThrow().ShouldBe(1);
    }
}