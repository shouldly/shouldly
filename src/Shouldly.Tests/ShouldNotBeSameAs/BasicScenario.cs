namespace Shouldly.Tests.ShouldNotBeSameAs;

public class BasicScenario
{
    [Fact]
    public void BasicScenarioShouldFail()
    {
        var zulu = new object();
        Verify.ShouldFail(() =>
                zulu.ShouldNotBeSameAs(zulu, "Some additional context"),

            errorWithSource:
            """
            zulu
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
        var zulu = new object();
        var tutsie = new object();

        zulu.ShouldNotBeSameAs(tutsie);
    }
}