namespace Shouldly.Tests.ShouldContain;

public class ReadOnlySpanOfIntegerScenario
{
    private readonly int[] _target = [1, 2, 3, 4, 5];

    [Fact]
    public void ReadOnlySpanOfIntegerScenarioShouldFail()
    {
        Verify.ShouldFail(() =>
                {
                    var target = new ReadOnlySpan<int>(_target);
                    target.ShouldContain(6, "Some additional context");
                },

            errorWithSource:
            """
            target
                should contain
            6
                but was actually
            [1, 2, 3, 4, 5]

            Additional Info:
                Some additional context
            """,

            errorWithoutSource:
            """
            [1, 2, 3, 4, 5]
                should contain
            6
                but did not

            Additional Info:
                Some additional context
            """);
    }

    [Fact]
    public void ShouldPass()
    {
        var target = new ReadOnlySpan<int>(_target);
        target.ShouldContain(3);
    }
}