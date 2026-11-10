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

    [Fact]
    public void ShouldPassWhenBothMembersAreDefault()
    {
        new Holder(default).ShouldBeEquivalentTo(new Holder(default));
    }

    [Fact]
    public void ShouldFailWhenActualMemberIsDefault()
    {
        Verify.ShouldFail(() =>
            new Holder(default).ShouldBeEquivalentTo(new Holder(ImmutableArray<int>.Empty), "Some additional context"));
    }

    [Fact]
    public void ShouldFailWhenExpectedMemberIsDefault()
    {
        Verify.ShouldFail(() =>
            new Holder(ImmutableArray.Create(1)).ShouldBeEquivalentTo(new Holder(default), "Some additional context"));
    }

    private sealed record Holder(ImmutableArray<int> Values);
}
