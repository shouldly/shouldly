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
    public void DuplicateCountMismatchScenarioShouldFail()
    {
        Verify.ShouldFail(() =>
            new List<int> { 1, 1, 2 }.ShouldBe([1, 2, 2], ignoreOrder: true));
    }

    [Fact]
    public void StringDuplicateCountMismatchScenarioShouldFail()
    {
        Verify.ShouldFail(() =>
            new List<string> { "a", "a", "b" }.ShouldBe(["a", "b", "b"], ignoreOrder: true));
    }

    [Fact]
    public void ShouldPass()
    {
        new List<int> { 1, 3, 2 }.ShouldBe([1, 2, 3], ignoreOrder: true);
    }
}