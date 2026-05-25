namespace Shouldly.Tests.ShouldBe.EnumerableType;

public class IgnoreOrderFalseScenario2
{
    [Fact]
    public void IgnoreOrderFalseScenario2ShouldFail()
    {
        Verify.ShouldFail(() =>
            new List<int> { 1, 3, 2 }.ShouldBe([1, 2, 3], false, "Some additional context"));
    }

    [Fact]
    public void ShouldPass()
    {
        new List<int> { 1, 2, 3 }.ShouldBe([1, 2, 3], ignoreOrder: false);
    }
}