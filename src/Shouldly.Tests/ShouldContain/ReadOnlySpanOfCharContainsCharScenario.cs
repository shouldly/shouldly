namespace Shouldly.Tests.ShouldContain;

public class ReadOnlySpanOfCharContainsCharScenario
{
    private const string Target = "Foo";

    [Fact]
    public void ReadOnlySpanOfCharContainsCharScenarioShouldFail()
    {
        Verify.ShouldFail(() =>
                Target.AsSpan().ShouldContain('B', "Some additional context"),

            errorWithSource:
            """
            Target.AsSpan()
                should contain
            B
                but was actually
            [F, o, o]

            Additional Info:
                Some additional context
            """,

            errorWithoutSource:
            """
            [F, o, o]
                should contain
            B
                but did not

            Additional Info:
                Some additional context
            """);
    }

    [Fact]
    public void ShouldPass()
    {
        Target.AsSpan().ShouldContain('F');
    }
}