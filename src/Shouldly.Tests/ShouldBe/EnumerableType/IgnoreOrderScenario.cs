namespace Shouldly.Tests.ShouldBe.EnumerableType;

public class IgnoreOrderScenario
{
    [Fact]
    public void IgnoreOrderScenarioShouldFail()
    {
        Verify.ShouldFail(() =>
            new List<int> { 1, 4, 2 }.ShouldBe([1, 2, 3], true, "Some additional context"));
    }

    [Fact]
    public void ShouldPass()
    {
        new List<int> { 1, 3, 2 }.ShouldBe([1, 2, 3], ignoreOrder: true);
    }
}