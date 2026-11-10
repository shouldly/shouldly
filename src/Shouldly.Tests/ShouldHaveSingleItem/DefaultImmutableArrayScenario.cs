using System.Collections.Immutable;

namespace Shouldly.Tests.ShouldHaveSingleItem;

public class DefaultImmutableArrayScenario
{
    [Fact]
    public void DefaultImmutableArrayScenarioShouldFail()
    {
        var actual = default(ImmutableArray<int>);
        Verify.ShouldFail(() =>
            actual.ShouldHaveSingleItem("Some additional context"));
    }
}
