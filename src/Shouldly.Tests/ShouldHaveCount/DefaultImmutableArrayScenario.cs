using System.Collections.Immutable;

namespace Shouldly.Tests.ShouldHaveCount;

public class DefaultImmutableArrayScenario
{
    [Fact]
    public void DefaultImmutableArrayScenarioShouldFail()
    {
        var actual = default(ImmutableArray<int>);
        Verify.ShouldFail(() =>
            actual.ShouldHaveCount(0, "Some additional context"));
    }
}
