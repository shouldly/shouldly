namespace Shouldly.Tests.ShouldNotContain;

public class PredicateClosureScenario
{
    [Fact]
    public void PredicateClosureScenarioShouldFail()
    {
        var capturedOuterVar = 4;
        var arr = new[] { 1, 2, 3 };
        Verify.ShouldFail(() =>
                arr.ShouldNotContain(i => i < capturedOuterVar, "Some additional context"),

            errorWithSource:
            """
            arr
                should not contain an element satisfying the condition
            (i < capturedOuterVar)
                but does

            Additional Info:
                Some additional context
            """,

            errorWithoutSource:
            """
            [1, 2, 3]
                should not contain an element satisfying the condition
            (i < capturedOuterVar)
                but does

            Additional Info:
                Some additional context
            """);
    }
}