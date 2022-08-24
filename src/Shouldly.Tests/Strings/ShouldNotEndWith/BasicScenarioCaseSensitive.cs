namespace Shouldly.Tests.Strings.ShouldNotEndWith;

public class BasicScenarioCaseSensitive
{
    [Fact]
    public void BasicScenarioCaseSensitiveShouldFail()
    {
        var str = "Cheese";
        Verify.ShouldFail(() =>
                str.ShouldNotEndWith("se", "Some additional context", Case.Sensitive),

            errorWithSource:
            @"str
    should not end with
""se""
    but was
""Cheese""

Additional Info:
    Some additional context",

            errorWithoutSource:
            @"""Cheese""
    should not end with
""se""
    but did

Additional Info:
    Some additional context");
    }

    [Fact]
    public void ShouldPass()
    {
        "Cheese".ShouldNotEndWith("SE", Case.Sensitive);
    }
}