using Shouldly;
using Xunit;

namespace LangVersionCompatTests;

/// <summary>
/// Regression guard for https://github.com/shouldly/shouldly/issues/825 under <c>LangVersion 12</c>.
///
/// <c>[OverloadResolutionPriority]</c> is a no-op before C# 13, so <c>List&lt;T&gt;.ShouldBe(List&lt;T&gt;)</c>
/// and <c>T[].ShouldBe(T[])</c> bind to the scalar <c>ShouldBe&lt;T&gt;(T?, T?)</c> overload here rather than
/// the enumerable one. The elements must still be compared with their <see cref="IEquatable{T}"/>
/// implementation, so the result doesn't depend on the consumer's LangVersion.
/// </summary>
public class EquatableElementTests
{
    private static List<Equatable> List(params int[] values) => values.Select(v => new Equatable(v)).ToList();

    [Fact]
    public void Lists_of_equal_elements_are_equal()
    {
        // The exact shape from the issue.
        List(1, 2, 3).ShouldBe(List(1, 2, 3));
        List(1, 2, 3).ShouldBe(List(1, 2, 3), "elements should match");
        List(1, 2, 3).ToArray().ShouldBe(List(1, 2, 3).ToArray());
    }

    [Fact]
    public void ShouldNotBe_fails_for_lists_of_equal_elements() =>
        Should.Throw<ShouldAssertException>(() => List(1, 2, 3).ShouldNotBe(List(1, 2, 3)));

#pragma warning disable CA1067 // Deliberately no object.Equals override: that is the shape issue #825 reports.
    private sealed class Equatable(int value) : IEquatable<Equatable>
    {
        private int Value { get; } = value;

        public bool Equals(Equatable? other) => other is not null && other.Value == Value;
    }
#pragma warning restore CA1067
}
