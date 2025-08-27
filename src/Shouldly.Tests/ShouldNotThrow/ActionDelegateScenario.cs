namespace Shouldly.Tests.ShouldNotThrow;

public class ActionDelegateScenario
{
    [Fact]
    [UseCulture("en-US")]
    public void ActionDelegateScenarioShouldFail()
    {
        Verify.ShouldFail(() =>
                Should.NotThrow(new Action(() => throw new InvalidOperationException()), "Some additional context"),

            errorWithSource:
            """
            `new Action(() => throw new InvalidOperationException())`
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
        var action = new Action(() => { });
        action.ShouldNotThrow();
    }

    [Fact]
    public void ShouldPassWhenExpectedExceptionIsNotThrown() =>
        Should.NotThrow<InvalidOperationException>(new Action(() => throw new()));

    [Fact]
    public void ShouldFailWhenExpectedExceptionIsThrown() =>
        Verify.ShouldFail(() => Should.NotThrow<InvalidOperationException>(new Action(() => throw new InvalidOperationException()), "Some additional context"),
            errorWithSource:
            """
            `new Action(() => throw new InvalidOperationException())`
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

    [Fact]
    public void ActionScenarioShouldPassWhenExpectedExceptionIsNotThrown()
    {
        var action = new Action(() => throw new());
        action.ShouldNotThrow<InvalidOperationException>();
    }

    [Fact]
    public void ActionScenarioShouldFailWhenExpectedExceptionIsThrown()
    {
        var action = new Action(() => throw new InvalidOperationException());

        Verify.ShouldFail(
            () => action.ShouldNotThrow<InvalidOperationException>("Some additional context"),

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
}