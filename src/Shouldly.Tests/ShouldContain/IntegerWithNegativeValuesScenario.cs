namespace Shouldly.Tests.ShouldContain;

public class IntegerWithNegativeValuesScenario
{
    private readonly int[] _target = [2, 3, 4, 5, 4, 123665, 11234, -13562377];

    [Fact]
    public void IntegerWithNegativeValuesScenarioShouldFail()
    {
        Verify.ShouldFail(() =>
                _target.ShouldContain(6, "Some additional context"),

            errorWithSource:
            """
            _target
                should contain
            6
                but was actually
            [2, 3, 4, 5, 4, 123665, 11234, -13562377]

            Additional Info:
                Some additional context
            """,

            errorWithoutSource:
            """
            [2, 3, 4, 5, 4, 123665, 11234, -13562377]
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
        _target.ShouldContain(-13562377);
    }
}