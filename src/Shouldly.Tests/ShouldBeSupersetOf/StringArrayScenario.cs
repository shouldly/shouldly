namespace Shouldly.Tests.ShouldBeSupersetOf;

public class StringArrayScenario
{
    [Fact]
    public void StringArrayScenarioShouldFail()
    {
        Verify.ShouldFail(() =>
                new[] { "1", "2" }.ShouldBeSupersetOf(["1", "2", "3"], "Some additional context"),

            errorWithSource:
            """
            new[] { "1", "2" }
                should be superset of
            ["1", "2", "3"]
                but
            ["3"]
                is outside superset

            Additional Info:
                Some additional context
            """,

            errorWithoutSource:
            """
            ["1", "2"]
                should be superset of
            ["1", "2", "3"]
                but
            ["3"]
                is outside superset

            Additional Info:
                Some additional context
            """);
    }

    [Fact]
    public void ShouldPass()
    {
        new[] { "1", "2", "3", "4" }.ShouldBeSupersetOf(["1", "2", "3"]);
    }
}