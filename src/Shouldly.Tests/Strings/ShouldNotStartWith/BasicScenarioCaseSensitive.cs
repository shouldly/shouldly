namespace Shouldly.Tests.Strings.ShouldNotStartWith;

public class BasicScenarioCaseSensitive
{
    [Fact]
    public void BasicScenarioCaseSensitiveShouldFail()
    {
        Verify.ShouldFail(() =>
                "Cheese".ShouldNotStartWith("Ch", Case.Sensitive, "Some additional context"),

            errorWithSource:
            @"""Cheese""
    should not start with
""Ch""
    but was

Additional Info:
    Some additional context",

            errorWithoutSource:
            @"""Cheese""
    should not start with
""Ch""
    but was

Additional Info:
    Some additional context");
    }

    [Fact]
    public void ShouldPass()
    {
        "Cheese".ShouldNotStartWith("CH", Case.Sensitive);
    }
}