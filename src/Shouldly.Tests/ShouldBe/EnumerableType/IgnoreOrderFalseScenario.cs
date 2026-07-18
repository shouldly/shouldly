namespace Shouldly.Tests.ShouldBe.EnumerableType;

public class IgnoreOrderFalseScenario
{
    [Fact]
    public void IgnoreOrderFalseScenarioShouldFail()
    {
        Verify.ShouldFail(() =>
            new List<int> { 1, 4, 2 }.ShouldBe([1, 2, 3], false, "Some additional context"));
    }

    [Fact]
    public void ShouldPass()
    {
        new List<int> { 1, 2, 3 }.ShouldBe([1, 2, 3], ignoreOrder: false);
    }
}