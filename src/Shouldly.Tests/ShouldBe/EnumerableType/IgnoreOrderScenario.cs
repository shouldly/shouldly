namespace Shouldly.Tests.ShouldBe.EnumerableType;

public class IgnoreOrderScenario
{
    [Fact]
    public void IgnoreOrderScenarioShouldFail()
    {
        Verify.ShouldFail(() =>
                new List<int> { 1, 4, 2 }.ShouldBe(new[] { 1, 2, 3 }, true, "Some additional context"),

            errorWithSource:
            @"new List<int> { 1, 4, 2 }
    should be (ignoring order)
[1, 2, 3]
    but
new List<int> { 1, 4, 2 }
    is missing
[3]
    and
[1, 2, 3]
    is missing
[4]

Additional Info:
    Some additional context",

            errorWithoutSource:
            @"[1, 4, 2]
    should be (ignoring order)
[1, 2, 3]
    but
[1, 4, 2]
    is missing
[3]
    and
[1, 2, 3]
    is missing
[4]

Additional Info:
    Some additional context");
    }

    [Fact]
    public void ShouldPass()
    {
        new List<int> { 1, 3, 2 }.ShouldBe(new[] { 1, 2, 3 }, ignoreOrder: true);
    }
}