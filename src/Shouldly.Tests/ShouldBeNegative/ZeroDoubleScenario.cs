namespace Shouldly.Tests.ShouldBeNegative;

public class ZeroDoubleScenario
{
    [Fact]
    public void ZeroDoubleScenarioShouldFail()
    {
        var val = 0.0;
        Verify.ShouldFail(() =>
                val.ShouldBeNegative("Some additional context"),

            errorWithSource:
            """
            val
                should be negative but
            0d
                is positive

            Additional Info:
                Some additional context
            """,

            errorWithoutSource:
            """
            0d
                should be negative but is positive

            Additional Info:
                Some additional context
            """);
    }
}