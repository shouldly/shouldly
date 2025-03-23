namespace Shouldly.Tests.ShouldBeSameAs;

public class BasicScenario
{
    [Fact]
    public void BasicScenarioShouldFail()
    {
        var apple = new object();
        var orange = new object();
        Verify.ShouldFail(() =>
                apple.ShouldBeSameAs(orange, "Some additional context"),

            errorWithSource:
            """
            apple
                should be same as
            System.Object (000000)
                but was
            System.Object (000000)

            Additional Info:
                Some additional context
            """,

            errorWithoutSource:
            """
            System.Object (000000)
                should be same as
            System.Object (000000)
                but was not

            Additional Info:
                Some additional context
            """);
    }

    [Fact]
    public void ShouldPass()
    {
        var apple = new object();

        apple.ShouldBeSameAs(apple);
    }
}