namespace Shouldly.Tests.ShouldContain;

public class StringArrayScenario
{
    private readonly string[] _target = ["a", "b", "c"];

    [Fact]
    public void StringArrayScenarioShouldFail()
    {
        Verify.ShouldFail(() =>
                _target.ShouldContain("d", "Some additional context"),

            errorWithSource:
            """
            _target
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
        _target.ShouldContain("b");
    }
}