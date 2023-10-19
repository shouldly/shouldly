namespace Shouldly.Tests.ShouldBeGreaterThan;

public class StringScenario
{
    [Fact]
    public void StringScenarioShouldFail()
    {
        var aVar = "a";
        Verify.ShouldFail(() =>
                aVar.ShouldBeGreaterThan("b", "Some additional context"),

            errorWithSource:
            """
            aVar
                should be greater than
            "b"
                but was
            "a"

            Additional Info:
                Some additional context
            """,

            errorWithoutSource:
            """
            "a"
                should be greater than
            "b"
                but was not

            Additional Info:
                Some additional context
            """);
    }

    [Fact]
    public void ShouldPass()
    {
        "b".ShouldBeGreaterThan("a");
    }
}