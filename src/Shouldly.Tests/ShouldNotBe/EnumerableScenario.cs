namespace Shouldly.Tests.ShouldNotBe;

public class EnumerableScenario
{
    [Fact]
    public void EnumerableScenarioShouldFail()
    {
        var list = new List<int> { 1, 2 };
        Verify.ShouldFail(() =>
            list.ShouldNotBe(new List<int> { 1, 2 }, "Some additional context"));
    }

    [Fact]
    public void ShouldPass()
    {
        new List<int> { 1, 2 }.ShouldNotBe(new List<int> { 1, 3 });
    }
}
