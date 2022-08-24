namespace Shouldly.Tests.ShouldBeSameAs;

public class SameListDifferentInstanceScenario
{
    [Fact]
    public void SameListDifferentInstanceScenarioShouldFail()
    {
        var list = new List<int> { 1, 2, 3 };
        var equalListWithDifferentRef = new List<int> { 1, 2, 3 };
        Verify.ShouldFail(() =>
                list.ShouldBeSameAs(equalListWithDifferentRef),

            errorWithSource:
            @"list
    should be same as
[1, 2, 3]
    but was
[1, 2, 3]
    difference
[1, 2, 3]",

            errorWithoutSource:
            @"[1, 2, 3]
    should be same as
[1, 2, 3]
    but was not
    difference
[1, 2, 3]");
    }
}