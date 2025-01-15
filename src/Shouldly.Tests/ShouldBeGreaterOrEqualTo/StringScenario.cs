namespace Shouldly.Tests.ShouldBeGreaterOrEqualTo;

public class StringScenario
{
    [Fact]
    public void StringScenarioShouldFail()
    {
        var aVar = "a";
        Verify.ShouldFail(() =>
                aVar.ShouldBeGreaterThanOrEqualTo("b", "Some additional context"),

            errorWithSource:
            """
            aVar
                should be greater than or equal to
            "b"
                but was
            "a"

            Additional Info:
                Some additional context
            """,

            errorWithoutSource:
            """
            "a"
                should be greater than or equal to
            "b"
                but was not

            Additional Info:
                Some additional context
            """);
    }

    [Fact]
    public void ShouldPass()
    {
        "a".ShouldBeGreaterThanOrEqualTo("a");
    }
}