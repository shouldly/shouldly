namespace Shouldly.Tests.ShouldBeLessThan;

public class StringScenario
{
    [Fact]
    public void StringScenarioShouldFail()
    {
        var beeeee = "b";
        Verify.ShouldFail(() =>
                beeeee.ShouldBeLessThan("a", "Some additional context"),

            errorWithSource:
            """
            beeeee
                should be less than
            "a"
                but was
            "b"

            Additional Info:
                Some additional context
            """,

            errorWithoutSource:
            """
            "b"
                should be less than
            "a"
                but was not

            Additional Info:
                Some additional context
            """);
    }

    [Fact]
    public void ShouldPass()
    {
        "a".ShouldBeLessThan("b");
    }
}