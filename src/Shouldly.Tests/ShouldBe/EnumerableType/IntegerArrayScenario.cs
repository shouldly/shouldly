namespace Shouldly.Tests.ShouldBe.EnumerableType
{
    public class IntegerArrayScenario
    {
    [Fact]
    public void IntegerArrayScenarioShouldFail()
    {
        Verify.ShouldFail(() =>
new[] { 99, 2, 3, 5 }.ShouldBe(new[] { 1, 2, 3, 4 }, "Some additional context"),

errorWithSource:
@"new[] { 99, 2, 3, 5 }
    should be
[1, 2, 3, 4]
    but was
[99, 2, 3, 5]
    difference
[*99*, 2, 3, *5*]

Additional Info:
    Some additional context",

errorWithoutSource:
@"[99, 2, 3, 5]
    should be
[1, 2, 3, 4]
    but was not
    difference
[*99*, 2, 3, *5*]

Additional Info:
    Some additional context");
    }

    [Fact]
    public void ShouldPass()
    {
        new[] { 1, 2, 3, 4 }.ShouldBe(new[] { 1, 2, 3, 4 });
    }
}
}