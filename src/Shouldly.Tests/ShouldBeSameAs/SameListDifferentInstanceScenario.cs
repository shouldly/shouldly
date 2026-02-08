namespace Shouldly.Tests.ShouldBeSameAs;

public class SameListDifferentInstanceScenario
{
    [Fact]
    public void SameListDifferentInstanceScenarioShouldFail()
    {
        var list = new List<int> { 1, 2, 3 };
        var equalListWithDifferentRef = new List<int> { 1, 2, 3 };
        Verify.ShouldFail(() =>
            list.ShouldBeSameAs(equalListWithDifferentRef));
    }
}