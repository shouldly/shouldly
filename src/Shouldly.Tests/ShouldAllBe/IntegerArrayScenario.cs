namespace Shouldly.Tests.ShouldAllBe;

public class IntegerArrayScenario
{
    [Fact]
    public void IntegerArrayScenarioShouldFail()
    {
        Verify.ShouldFail(() =>
                new[] { 1, 2, 3 }.ShouldAllBe(x => x < 2, "Some additional context"),

            errorWithSource:
            @"new[] { 1, 2, 3 }
    should satisfy the condition
(x < 2)
    but
[2, 3]
    do not

Additional Info:
    Some additional context",

            errorWithoutSource:
            @"[1, 2, 3]
    should satisfy the condition
(x < 2)
    but
[2, 3]
    do not

Additional Info:
    Some additional context");
    }

    [Fact]
    public void ShouldPass()
    {
        new[] { 1, 2, 3 }.ShouldAllBe(x => x < 4);
    }
}