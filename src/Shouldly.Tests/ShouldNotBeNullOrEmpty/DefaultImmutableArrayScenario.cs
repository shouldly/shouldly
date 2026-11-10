using System.Collections.Immutable;

namespace Shouldly.Tests.ShouldNotBeNullOrEmpty;

public class DefaultImmutableArrayScenario
{
    [Fact]
    public void FailsForDefault()
    {
        var actual = default(ImmutableArray<int>);
        Verify.ShouldFail(() =>
            actual.ShouldNotBeNullOrEmpty("Some additional context"));
    }
}
