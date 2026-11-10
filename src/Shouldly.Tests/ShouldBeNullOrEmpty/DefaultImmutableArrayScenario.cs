using System.Collections.Immutable;

namespace Shouldly.Tests.ShouldBeNullOrEmpty;

public class DefaultImmutableArrayScenario
{
    [Fact]
    public void PassesForDefault()
    {
        default(ImmutableArray<int>).ShouldBeNullOrEmpty();
    }
}
