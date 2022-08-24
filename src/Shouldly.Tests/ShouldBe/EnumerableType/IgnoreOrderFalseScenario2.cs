namespace Shouldly.Tests.ShouldBe.EnumerableType
{
    public class IgnoreOrderFalseScenario2
    {
    [Fact]
    public void IgnoreOrderFalseScenario2ShouldFail()
    {
        Verify.ShouldFail(() =>
new List<int> { 1, 3, 2 }.ShouldBe(new[] { 1, 2, 3 }, false, "Some additional context"),

errorWithSource:
@"new List<int> { 1, 3, 2 }
    should be
[1, 2, 3]
    but was
[1, 3, 2]
    difference
[1, *3*, *2*]

Additional Info:
    Some additional context",

errorWithoutSource:
@"[1, 3, 2]
    should be
[1, 2, 3]
    but was not
    difference
[1, *3*, *2*]

Additional Info:
    Some additional context");
    }

    [Fact]
    public void ShouldPass()
    {
        new List<int> { 1, 2, 3 }.ShouldBe(new[] { 1, 2, 3 }, ignoreOrder: false);
    }
}
}