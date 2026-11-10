using System.Collections.Immutable;

namespace Shouldly.Tests.ShouldNotBeEmpty;

public class DefaultImmutableArrayScenario
{
    [Fact]
    public void DefaultImmutableArrayScenarioShouldFail()
    {
        var actual = default(ImmutableArray<int>);
        Verify.ShouldFail(() =>
            actual.ShouldNotBeEmpty("Some additional context"));
    }

    [Fact]
    public void ShouldPass()
    {
        ImmutableArray.Create(1).ShouldNotBeEmpty();
    }
}
