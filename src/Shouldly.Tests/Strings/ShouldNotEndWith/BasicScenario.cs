namespace Shouldly.Tests.Strings.ShouldNotEndWith;

public class BasicScenario
{
    [Fact]
    public void BasicScenarioShouldFail()
    {
        var str = "Cheese";
        Verify.ShouldFail(() =>
                str.ShouldNotEndWith("se", "Some additional context"),

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
        "Cheese".ShouldNotEndWith("ze");
    }
}