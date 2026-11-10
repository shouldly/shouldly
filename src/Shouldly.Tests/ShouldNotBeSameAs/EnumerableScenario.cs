namespace Shouldly.Tests.ShouldNotBeSameAs;

public class EnumerableScenario
{
    [Fact]
    public void EnumerableScenarioShouldFail()
    {
        var list = new List<int> { 1, 2, 3 };
        Verify.ShouldFail(() =>
            list.ShouldNotBeSameAs(list));
    }
}
