namespace Shouldly.Tests.ShouldBeNegative;

public class ZeroShortScenario
{
    [Fact]
    public void ZeroLongScenarioShouldFail()
    {
        short val = 0;
        Verify.ShouldFail(() =>
                val.ShouldBeNegative("Some additional context"),

            errorWithSource:
            """
            val
                should be negative but
            0
                is positive

            Additional Info:
                Some additional context
            """,

            errorWithoutSource:
            """
            0
                should be negative but is positive

            Additional Info:
                Some additional context
            """);
    }
}