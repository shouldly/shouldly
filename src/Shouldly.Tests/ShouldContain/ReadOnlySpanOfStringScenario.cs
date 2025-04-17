namespace Shouldly.Tests.ShouldContain;

public class ReadOnlySpanOfStringScenario
{
    private readonly string[] _target = ["a", "b", "c"];

    [Fact]
    public void ReadOnlySpanOfStringScenarioShouldFail()
    {
        Verify.ShouldFail(() =>
                {
                    var target = new ReadOnlySpan<string>(_target);
                    target.ShouldContain("d", "Some additional context");
                },

            errorWithSource:
            """
            target
                should contain
            "d"
                but was actually
            ["a", "b", "c"]

            Additional Info:
                Some additional context
            """,

            errorWithoutSource:
            """
            ["a", "b", "c"]
                should contain
            "d"
                but did not

            Additional Info:
                Some additional context
            """);
    }

    [Fact]
    public void ShouldPass()
    {
        var target = new ReadOnlySpan<string>(_target);
        target.ShouldContain("b");
    }
}