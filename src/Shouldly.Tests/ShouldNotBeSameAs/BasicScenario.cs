namespace Shouldly.Tests.ShouldNotBeSameAs;

public class BasicScenario
{
    [Fact]
    public void BasicScenarioShouldFail()
    {
        var apple = new object();
        Verify.ShouldFail(() =>
                apple.ShouldNotBeSameAs(apple, "Some additional context"),

            errorWithSource:
            """
            apple
                should not be same as
            System.Object (000000)
                but was
            System.Object (000000)

            Additional Info:
                Some additional context
            """,

            errorWithoutSource:
            """
            System.Object (000000)
                should not be same as
            System.Object (000000)
                but was

            Additional Info:
                Some additional context
            """);
    }

    [Fact]
    public void ShouldPass()
    {
        var apple = new object();
        var orange = new object();

        apple.ShouldNotBeSameAs(orange);
    }
}