using System.Collections.Immutable;

namespace Shouldly.Tests.ShouldBeNull;

// A default ImmutableArray<T> counts as null, as it does when compared and printed (#1406).
public class DefaultImmutableArrayScenario
{
    [Fact]
    public void ShouldPassForNullableHoldingDefault()
    {
        ImmutableArray<int>? actual = default(ImmutableArray<int>);
        actual.ShouldBeNull();
    }

    [Fact]
    public void ShouldPassForBoxedDefault()
    {
        IEnumerable<int> actual = default(ImmutableArray<int>);
        actual.ShouldBeNull();
    }

    [Fact]
    public void ShouldNotBeNullShouldFailForNullableHoldingDefault()
    {
        ImmutableArray<int>? actual = default(ImmutableArray<int>);
        Verify.ShouldFail(() =>
            actual.ShouldNotBeNull("Some additional context"));
    }

    [Fact]
    public void ShouldNotBeNullShouldFailForBoxedDefault()
    {
        IEnumerable<int> actual = default(ImmutableArray<int>);
        Verify.ShouldFail(() =>
            actual.ShouldNotBeNull("Some additional context"));
    }
}
