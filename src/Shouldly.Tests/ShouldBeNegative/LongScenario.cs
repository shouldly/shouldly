namespace Shouldly.Tests.ShouldBeNegative;

public class LongScenario
{
    [Fact]
    public void LongScenarioShouldFail()
    {
        var @long = 3L;
        Verify.ShouldFail(() =>
                @long.ShouldBeNegative("Some additional context"),

            errorWithSource:
            """
            @long
                should be negative but
            3L
                is positive

            Additional Info:
                Some additional context
            """,

            errorWithoutSource:
            """
            3L
                should be negative but is positive

            Additional Info:
                Some additional context
            """);
    }

    [Fact]
    public void ShouldPass()
    {
        (-7L).ShouldBeNegative();
    }
}