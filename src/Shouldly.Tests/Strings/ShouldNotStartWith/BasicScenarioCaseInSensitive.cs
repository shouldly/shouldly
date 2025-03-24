namespace Shouldly.Tests.Strings.ShouldNotStartWith;

public class BasicScenarioCaseInSensitive
{
    [Fact]
    public void BasicScenarioCaseInSensitiveShouldFail()
    {
        Verify.ShouldFail(() =>
                "Cheese".ShouldNotStartWith("cH", Case.Insensitive, "Some additional context"),

            errorWithSource:
            """
            "Cheese"
                should not start with
            "cH"
                but was

            Additional Info:
                Some additional context
            """,

            errorWithoutSource:
            """
            "Cheese"
                should not start with
            "cH"
                but was

            Additional Info:
                Some additional context
            """);
    }

    [Fact]
    public void ShouldPass()
    {
        "Cheese".ShouldNotStartWith("Ce");
    }
}