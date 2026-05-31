namespace Shouldly.Tests.ShouldBe.Span;

public class SpanShouldBeBehaviourTests
{
    [Fact]
    public void ReadOnlySpanOfIntPasses()
    {
        ReadOnlySpan<int> actual = [1, 2, 3];
        actual.ShouldBe([1, 2, 3]);
    }

    [Fact]
    public void StackAllocSpanPasses()
    {
        Span<int> actual = stackalloc int[] { 1, 2, 3 };
        actual.ShouldBe([1, 2, 3]);
    }

    [Fact]
    public void ArrayConvertsToExpectedReadOnlySpan()
    {
        Span<int> actual = [1, 2, 3];
        actual.ShouldBe(new[] { 1, 2, 3 });
    }

    [Fact]
    public void SpanShouldBeFailsWhenLengthsDiffer()
    {
        Should.Throw<ShouldAssertException>(() =>
            new[] { 1, 2, 3 }.AsSpan().ShouldBe([1, 2]));
    }

    [Fact]
    public void SpanOfComplexTypeUsesElementEquality()
    {
        // double comparison goes through Shouldly's own comparer, not bitwise SequenceEqual
        ReadOnlySpan<double> actual = [1.0, 2.0, 3.0];
        actual.ShouldBe([1.0, 2.0, 3.0]);
    }

    [Fact]
    public void ShouldNotBePassesWhenContentsDiffer()
    {
        ReadOnlySpan<char> actual = "hello".AsSpan();
        actual.ShouldNotBe("world".AsSpan());
    }

    // Regression pin: arrays must continue to bind to the IEnumerable<T> overload,
    // not the new span overloads. The ignoreOrder parameter only exists on the
    // IEnumerable<T> overload, so this both compiles and passes only if arrays
    // still resolve there.
    [Fact]
    public void ArrayStillResolvesToEnumerableOverload()
    {
        new[] { 3, 2, 1 }.ShouldBe([1, 2, 3], ignoreOrder: true);
    }
}
