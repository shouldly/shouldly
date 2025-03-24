namespace Shouldly.Tests.ShouldBeNegative;

public class ZeroDecimalScenario
{
    [Fact]
    public void ZeroDecimalScenarioShouldFail()
    {
        var val = 0m;
        Verify.ShouldFail(() =>
                val.ShouldBeNegative("Some additional context"),

            errorWithSource:
            """
            val
                should be negative but
            0m
                is positive

            Additional Info:
                Some additional context
            """,

            errorWithoutSource:
            """
            0m
                should be negative but is positive

            Additional Info:
                Some additional context
            """);
    }
}