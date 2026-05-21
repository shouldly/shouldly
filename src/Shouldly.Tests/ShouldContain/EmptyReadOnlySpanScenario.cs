namespace Shouldly.Tests.ShouldContain;

public class EmptyReadOnlySpanScenario
{
    [Fact]
    public void EmptyReadOnlySpanScenarioShouldFail()
    {
        Verify.ShouldFail(() =>
                {
                    var target = ReadOnlySpan<int>.Empty;
                    target.ShouldContain(1, "Some additional context");
                },

            errorWithSource:
            """
            target
                should contain
            1
                but was actually
            []

            Additional Info:
                Some additional context
            """,

            errorWithoutSource:
            """
            []
                should contain
            1
                but did not

            Additional Info:
                Some additional context
            """);
    }
}