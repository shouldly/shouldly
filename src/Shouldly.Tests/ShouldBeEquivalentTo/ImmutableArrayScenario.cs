using System.Collections.Immutable;

namespace Shouldly.Tests.ShouldBeEquivalentTo;

public class ImmutableArrayScenario
{
    [Fact]
    public void ShouldPass()
    {
        var subject = ImmutableArray.Create(1, 2, 6, 4, 5);
        subject.ShouldBeEquivalentTo(ImmutableArray.Create(1, 2, 6, 4, 5));
    }
}