using System.Collections.Immutable;

namespace Shouldly.Tests.ShouldBe;

// A default ImmutableArray<T> wraps a null array and throws when enumerated, so Shouldly treats it like null (#1406).
public class DefaultImmutableArrayScenarios
{
    private static readonly ImmutableArray<int> DefaultArray = default;

    [Fact]
    public void DefaultShouldBeDefaultShouldPass()
    {
        DefaultArray.ShouldBe(default);
    }

    [Fact]
    public void DefaultShouldBeDefaultIgnoringOrderShouldPass()
    {
        DefaultArray.ShouldBe(default, ignoreOrder: true);
    }

    [Fact]
    public void DefaultShouldBeDefaultWithComparerShouldPass()
    {
        DefaultArray.ShouldBe(default, EqualityComparer<int>.Default);
    }

    [Fact]
    public void DefaultShouldBeDefaultThroughScalarOverloadShouldPass()
    {
        DefaultArray.ShouldBe<ImmutableArray<int>>(default);
    }

    [Fact]
    public void BoxedDefaultShouldBeBoxedDefaultShouldPass()
    {
        ((object)DefaultArray).ShouldBe((object)default(ImmutableArray<int>));
    }

    [Fact]
    public void NestedDefaultsShouldPass()
    {
        new[] { DefaultArray }.ShouldBe([default]);
    }

    [Fact]
    public void DefaultShouldBeEmptyShouldFail()
    {
        Verify.ShouldFail(() =>
            DefaultArray.ShouldBe(ImmutableArray<int>.Empty, "Some additional context"));
    }

    [Fact]
    public void DefaultShouldBeEmptyIgnoringOrderShouldFail()
    {
        Verify.ShouldFail(() =>
            DefaultArray.ShouldBe(ImmutableArray<int>.Empty, ignoreOrder: true, "Some additional context"));
    }

    [Fact]
    public void DefaultShouldBeItemsWithComparerShouldFail()
    {
        Verify.ShouldFail(() =>
            DefaultArray.ShouldBe(ImmutableArray.Create(1, 2), EqualityComparer<int>.Default, customMessage: "Some additional context"));
    }

    [Fact]
    public void DefaultShouldBeItemsWithComparerIgnoringOrderShouldFail()
    {
        Verify.ShouldFail(() =>
            DefaultArray.ShouldBe(ImmutableArray.Create(1, 2), EqualityComparer<int>.Default, ignoreOrder: true, "Some additional context"));
    }

    [Fact]
    public void DefaultShouldBeEmptyThroughScalarOverloadShouldFail()
    {
        Verify.ShouldFail(() =>
            DefaultArray.ShouldBe<ImmutableArray<int>>(ImmutableArray<int>.Empty, "Some additional context"));
    }

    [Fact]
    public void ItemsShouldBeDefaultShouldFail()
    {
        Verify.ShouldFail(() =>
            ImmutableArray.Create(1).ShouldBe(default, "Some additional context"));
    }

    [Fact]
    public void BoxedDefaultShouldBeBoxedItemsShouldFail()
    {
        Verify.ShouldFail(() =>
            ((object)DefaultArray).ShouldBe((object)ImmutableArray.Create(1), "Some additional context"));
    }

    [Fact]
    public void NestedDefaultShouldBeEmptyShouldFail()
    {
        Verify.ShouldFail(() =>
            new[] { ImmutableArray.Create(1), DefaultArray }.ShouldBe([ImmutableArray.Create(1), ImmutableArray<int>.Empty], "Some additional context"));
    }

    [Fact]
    public void DefaultShouldNotBeEmptyShouldPass()
    {
        DefaultArray.ShouldNotBe(ImmutableArray<int>.Empty);
    }

    [Fact]
    public void DefaultShouldNotBeDefaultShouldFail()
    {
        Verify.ShouldFail(() =>
            DefaultArray.ShouldNotBe(default, "Some additional context"));
    }
}
