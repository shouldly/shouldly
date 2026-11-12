using System.Collections.Frozen;
using System.Collections.Immutable;
using Shouldly.Internals;

namespace Shouldly.Tests.InternalTests;

/// <summary>
/// .NET's own sets and sorted collections are known by type, not only by the interfaces reflection finds on them: in
/// a trimmed or NativeAOT app an interface nothing else asked of a type can be gone, and a <c>HashSet&lt;T&gt;</c>
/// would be compared in order. A test can't remove the interfaces, so these hold the lists of types to account.
/// </summary>
public class UnorderedCollectionTests
{
    public static TheoryData<Type> Sets =>
    [
        typeof(HashSet<int>),
        typeof(SortedSet<int>),
        typeof(ImmutableHashSet<int>),
        typeof(ImmutableSortedSet<int>),
        new[] { 1 }.ToFrozenSet().GetType(),
        typeof(DerivedSet)
    ];

    public static TheoryData<Type> KeepAnOrder =>
    [
        typeof(SortedSet<int>),
        typeof(SortedDictionary<string, int>),
        typeof(SortedList<string, int>),
        typeof(System.Collections.SortedList),
        typeof(System.Collections.Specialized.OrderedDictionary),
        typeof(OrderedDictionary<string, int>),
        typeof(ImmutableSortedSet<int>),
        typeof(ImmutableSortedDictionary<string, int>),
        typeof(DerivedSortedSet)
    ];

    public static TheoryData<Type> KeepNoOrder =>
    [
        typeof(HashSet<int>),
        typeof(Dictionary<string, int>),
        typeof(System.Collections.Hashtable),
        typeof(ImmutableHashSet<int>),
        typeof(ImmutableDictionary<string, int>),
        new[] { 1 }.ToFrozenSet().GetType(),
        typeof(System.Collections.Concurrent.ConcurrentDictionary<string, int>),
        typeof(DerivedSet)
    ];

    [Theory]
    [MemberData(nameof(Sets))]
    public void KnowsDotNetsSetsByType(Type type) =>
        UnorderedCollection.IsKnownSet(type).ShouldBeTrue();

    [Theory]
    [MemberData(nameof(KeepAnOrder))]
    public void ASortedOrOrderedCollectionKeepsAnOrder(Type type) =>
        UnorderedCollection.KeepsAnOrder(type).ShouldBeTrue();

    [Theory]
    [MemberData(nameof(KeepNoOrder))]
    public void AHashBasedCollectionKeepsNoOrder(Type type) =>
        UnorderedCollection.KeepsAnOrder(type).ShouldBeFalse();

    // Equality needn't be transitive. Here 1.01 equals both 1.0 and 1.02, so taking the first equal item for it
    // can leave 1.03 with none, depending on the order the items come in.
    [Theory]
    [InlineData(new[] { 1.01, 1.03 }, new[] { 1.02, 1.0 })]
    [InlineData(new[] { 1.01, 1.03 }, new[] { 1.0, 1.02 })]
    [InlineData(new[] { 1.03, 1.01 }, new[] { 1.02, 1.0 })]
    [InlineData(new[] { 1.03, 1.01 }, new[] { 1.0, 1.02 })]
    [InlineData(new[] { 1.0, 1.01, 1.02, 1.03 }, new[] { 1.01, 1.02, 1.03, 1.04 })]
    [InlineData(new[] { 1.04, 1.03, 1.02, 1.01 }, new[] { 1.0, 1.01, 1.02, 1.03 })]
    public void PairsItemsWheneverThereIsAPairing(double[] actual, double[] expected) =>
        UnorderedCollection.MatchOneForOne(actual, expected, Within(0.015)).ShouldBeTrue();

    [Theory]
    [InlineData(new[] { 1.0, 1.01, 1.02 }, new[] { 1.01, 1.02, 1.5 })]
    [InlineData(new[] { 1.0, 1.0, 2.0 }, new[] { 1.0, 2.0, 2.0 })]
    [InlineData(new[] { 1.0, 2.0 }, new[] { 1.0 })]
    [InlineData(new[] { 1.0 }, new[] { 1.0, 2.0 })]
    [InlineData(new[] { 1.0, 1.0 }, new[] { 1.0, 1.0, 1.0 })]
    public void DoesNotPairItemsWhenThereIsNoPairing(double[] actual, double[] expected) =>
        UnorderedCollection.MatchOneForOne(actual, expected, Within(0.015)).ShouldBeFalse();

    // Every pairing but the last has to move along by one, which mustn't cost a stack frame apiece.
    [Fact]
    public void PairsALongChainOfItemsThatEachEqualTheNext()
    {
        var actual = Enumerable.Range(1, 5000).Select(number => number * 0.01).ToList();
        var shifted = Enumerable.Range(2, 4999).Select(number => number * 0.01).Append(0.0).ToList();

        UnorderedCollection.MatchOneForOne(actual, shifted, Within(0.015)).ShouldBeTrue();
        UnorderedCollection.MatchOneForOne(actual, [.. shifted.Take(4999), -5.0], Within(0.015)).ShouldBeFalse();
    }

    [Fact]
    public void ReadsTheActualItemsNoFurtherThanTheFirstThatCannotBePaired()
    {
        UnorderedCollection.MatchOneForOne(Naturals(), [1.0, 2.0, 3.0], Within(0.015)).ShouldBeFalse();
        UnorderedCollection.MatchOneForOne(Naturals(), [0.0, 1.0, 2.0], Within(0.015)).ShouldBeFalse();

        static IEnumerable<double> Naturals()
        {
            for (var number = 0; ; number++)
                yield return number;
        }
    }

    private static Func<double, double, bool> Within(double tolerance) =>
        (actual, expected) => Math.Abs(actual - expected) <= tolerance;

    private sealed class DerivedSet : HashSet<int>;

    private sealed class DerivedSortedSet : SortedSet<int>;
}
