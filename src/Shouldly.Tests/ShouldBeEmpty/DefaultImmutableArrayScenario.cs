using System.Collections.Immutable;

namespace Shouldly.Tests.ShouldBeEmpty;

public class DefaultImmutableArrayScenario
{
    [Fact]
    public void DefaultImmutableArrayScenarioShouldFail()
    {
        var actual = default(ImmutableArray<int>);
        Verify.ShouldFail(() =>
            actual.ShouldBeEmpty("Some additional context"));
    }

    [Fact]
    public void ShouldPass()
    {
        ImmutableArray<int>.Empty.ShouldBeEmpty();
    }
}
