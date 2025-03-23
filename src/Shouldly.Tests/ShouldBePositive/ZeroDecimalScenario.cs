namespace Shouldly.Tests.ShouldBePositive;

public class ZeroDecimalScenario
{
    [Fact]
    public void ZeroDecimalScenarioShouldFail()
    {
        var val = 0m;
        Verify.ShouldFail(() =>
                val.ShouldBePositive("Some additional context"),

// TODO is zero negative?
            errorWithSource:
            """
            val
                should be positive but
            0m
                is negative

            Additional Info:
                Some additional context
            """,

            errorWithoutSource:
            """
            0m
                should be positive but is negative

            Additional Info:
                Some additional context
            """);
    }
}