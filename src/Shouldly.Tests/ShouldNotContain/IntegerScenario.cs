namespace Shouldly.Tests.ShouldNotContain;

public class IntegerScenario
{
    private readonly int[] _target = { 1, 2, 3, 4, 5 };

    [Fact]
    public void IntegerScenarioShouldFail()
    {
        Verify.ShouldFail(() =>
                _target.ShouldNotContain(3, "Some additional context"),

            errorWithSource:
            """
            _target
                should not contain
            3
                but was actually
            [1, 2, 3, 4, 5]

            Additional Info:
                Some additional context
            """,

            errorWithoutSource:
            """
            [1, 2, 3, 4, 5]
                should not contain
            3
                but did

            Additional Info:
                Some additional context
            """);
    }

    [Fact]
    public void ShouldPass()
    {
        _target.ShouldNotContain(7);
    }
}