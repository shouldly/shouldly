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

    private sealed class DerivedSet : HashSet<int>;

    private sealed class DerivedSortedSet : SortedSet<int>;
}
