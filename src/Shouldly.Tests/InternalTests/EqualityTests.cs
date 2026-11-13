using System.Collections.Immutable;
using Shouldly.Internals;

namespace Shouldly.Tests.InternalTests;

/// <summary>
/// Shouldly's equality rules, tested once through <see cref="Equality"/>'s own interface rather than once per
/// assertion. Each assertion family only needs a smoke test showing it asks <see cref="Equality"/>.
/// </summary>
public class EqualityTests
{
    // Each case is a comparison and whether it should find the values equal. Keyed by name so that each row is
    // discovered, and reported, as its own test.
    private static readonly Dictionary<string, (Func<bool> Compare, bool Equal)> Cases = new()
    {
        // Single values
        ["int, same"] = (() => Equal(1, 1), true),
        ["int, different"] = (() => Equal(1, 2), false),
        ["null, null"] = (() => Equal<object>(null, null), true),
        ["null actual"] = (() => Equal<object>(null, 1), false),
        ["null expected"] = (() => Equal<object>(1, null), false),
        ["string, same"] = (() => Equal("a", "a"), true),
        ["string, different case"] = (() => Equal("a", "A"), false),
        ["Guid, same"] = (() => Equal(Guid.Empty, new Guid()), true),

        // Numbers of different types compare by value, exactly (#1426, #1427)
        ["int vs long, same value"] = (() => Equal<object>(1, 1L), true),
        ["int vs double, same value"] = (() => Equal<object>(1, 1.0), true),
        ["uint.MaxValue vs -1"] = (() => Equal<object>(uint.MaxValue, -1), false),
        ["decimal 0.1 vs double 0.1"] = (() => Equal<object>(0.1m, 0.1), false),

        // A default ImmutableArray counts as null (#1406)
        ["default ImmutableArray vs null"] = (() => Equal<object>(default(ImmutableArray<int>), null), true),
        ["default ImmutableArray vs empty"] = (() => Equal<object>(default(ImmutableArray<int>), ImmutableArray<int>.Empty), false),

        // Collections are walked structurally, at any depth
        ["nested lists, same items"] = (() => Equal(NestedList(1), NestedList(1)), true),
        ["nested lists, different items"] = (() => Equal(NestedList(1), NestedList(2)), false),
        ["array vs list, same items"] = (() => Equal<IEnumerable<int>>(new[] { 1, 2 }, new List<int> { 1, 2 }), true),

        // Key/value pairs compare their key and value with the same rules (#1408)
        ["KeyValuePair, structurally equal values"] = (() => Equal(Pair("k", [1, 2]), Pair("k", [1, 2])), true),
        ["KeyValuePair, different values"] = (() => Equal(Pair("k", [1, 2]), Pair("k", [1, 3])), false),

        // IEquatable<T> is honoured even where the static type has been lost (#825)
        ["IEquatable-only elements as objects"] = (() => Equal(new List<object> { new EquatableOnly(1) }, [new EquatableOnly(1)]), true),

        // A registered equality rule decides, for the type and its subtypes, at any depth (see ModuleInitializer)
        ["rule, own Equals, same id, different items"] = (() => Equal(new IdBag(1, 1), new IdBag(1, 2)), true),
        ["rule, own Equals, different id, same items"] = (() => Equal(new IdBag(1, 1), new IdBag(2, 1)), false),
        ["rule, subtype, same leaf"] = (() => Equal<Node>(new Leaf("a"), new Leaf("a")), true),
        ["rule, subtype, different leaf (walked, both are empty)"] = (() => Equal<Node>(new Leaf("a"), new Leaf("b")), false),
        ["rule, deep equal branches"] = (() => Equal<Node>(new Branch(new Leaf("a")), new Branch(new Leaf("a"))), true),
        ["rule, different branches"] = (() => Equal<Node>(new Branch(new Leaf("a")), new Branch(new Leaf("b"))), false),
        ["rule, static subtype"] = (() => Equal(new Leaf("a"), new Leaf("b")), false),
        ["rule, whole value through the sequence overload"] = (() => SequenceEqual<Node>(new Leaf("a"), new Leaf("b")), false),
        ["rule, sequence of equal values"] = (() => SequenceEqual<Node>([new Leaf("a")], [new Leaf("a")]), true),
        ["rule, sequence of different values"] = (() => SequenceEqual<Node>([new Leaf("a")], [new Leaf("b")]), false),
        ["rule, ignoring order, permuted"] = (() => SequenceEqual<Node>([new Leaf("a"), new Leaf("b")], [new Leaf("b"), new Leaf("a")], ignoreOrder: true), true),
        ["rule, ignoring order, different"] = (() => SequenceEqual<Node>([new Leaf("a")], [new Leaf("b")], ignoreOrder: true), false),
        ["rule, whole value, permuted"] = (() => SequenceEqual<Node>(new Branch(new Leaf("a"), new Leaf("b")), new Branch(new Leaf("b"), new Leaf("a"))), false),
        ["rule, whole value ignoring order, permuted"] = (() => SequenceEqual<Node>(new Branch(new Leaf("a"), new Leaf("b")), new Branch(new Leaf("b"), new Leaf("a")), ignoreOrder: true), true),
        ["rule, whole value ignoring order, different"] = (() => SequenceEqual<Node>(new Branch(new Leaf("a")), new Branch(new Leaf("b")), ignoreOrder: true), false),
        ["rule, nested as objects"] = (() => Equal(new List<object> { new Leaf("a") }, [new Leaf("b")]), false),
        ["rule, KeyValuePair value"] = (() => Equal(new KeyValuePair<string, Node>("k", new Leaf("a")), new("k", new Leaf("b"))), false),
        ["rule, KeyValuePair value as object"] = (() => Equal(new KeyValuePair<string, object>("k", new Leaf("a")), new("k", new Leaf("b"))), false),
        ["rule, most specific wins"] = (() => Equal(new CaseInsensitiveLeaf("A"), new CaseInsensitiveLeaf("a")), true),
        ["rule, applies only when both values are its type"] = (() => Equal<Leaf>(new CaseInsensitiveLeaf("A"), new Leaf("a")), false),

        // A caller-supplied comparer decides
        ["custom comparer"] = (() => Equal("a", "A", StringComparer.OrdinalIgnoreCase), true),

        // Sequences in order
        ["sequence, same order"] = (() => SequenceEqual([1, 2, 3], [1, 2, 3]), true),
        ["sequence, different order"] = (() => SequenceEqual([1, 2, 3], [3, 2, 1]), false),
        ["sequence, actual shorter"] = (() => SequenceEqual([1, 2], [1, 2, 3]), false),
        ["sequence, actual longer"] = (() => SequenceEqual([1, 2, 3], [1, 2]), false),
        ["sequence, null vs null"] = (() => SequenceEqual<int>(null, null), true),
        ["sequence, null vs empty"] = (() => SequenceEqual<int>(null, []), false),
        ["sequence, default ImmutableArray vs null"] = (() => SequenceEqual<int>(default(ImmutableArray<int>), null), true),
        ["sequence, custom comparer"] = (() => SequenceEqual(["a"], ["A"], comparer: StringComparer.OrdinalIgnoreCase), true),

        // Sequences ignoring order match elements one for one
        ["ignoring order, permuted"] = (() => SequenceEqual([1, 2, 2], [2, 1, 2], ignoreOrder: true), true),
        ["ignoring order, different counts"] = (() => SequenceEqual([1, 1, 2], [1, 2, 2], ignoreOrder: true), false),
        ["ignoring order, missing default value"] = (() => SequenceEqual([1], [0], ignoreOrder: true), false),
        ["ignoring order, null vs null"] = (() => SequenceEqual<int>(null, null, ignoreOrder: true), true),
    };

    public static TheoryData<string> CaseNames => [.. Cases.Keys];

    [Theory]
    [MemberData(nameof(CaseNames))]
    public void Compares(string name)
    {
        var (compare, equal) = Cases[name];

        compare().ShouldBe(equal);
    }

    [Fact]
    public void CallsEqualsOnTheExpectedValue()
    {
        var actual = new SpyEquatable();
        var expected = new SpyEquatable();

        Equal(actual, expected);

        expected.EqualsCalled.ShouldBeTrue();
        actual.EqualsCalled.ShouldBeFalse();
    }

    [Fact]
    public void CallsEqualsOnTheExpectedElement()
    {
        var actual = new SpyEquatable();
        var expected = new SpyEquatable();

        SequenceEqual([actual], [expected]);

        expected.EqualsCalled.ShouldBeTrue();
        actual.EqualsCalled.ShouldBeFalse();
    }

    [Fact]
    public void HashesWithTheBclComparerOnlyWhereItAgrees()
    {
        Equality<int>.Default.HashingComparer.ShouldBeSameAs(System.Collections.Generic.EqualityComparer<int>.Default);
        Equality<string>.Default.HashingComparer.ShouldBeSameAs(System.Collections.Generic.EqualityComparer<string>.Default);
        Equality<double>.Default.HashingComparer.ShouldBeNull();
        Equality<object>.Default.HashingComparer.ShouldBeNull();
        Equality<EquatableOnly>.Default.HashingComparer.ShouldBeNull();
        Equality.WithinTolerance<decimal>(0.1m).HashingComparer.ShouldBeNull();
    }

    [Fact]
    public void HashesWithACallersComparer()
    {
        var comparer = StringComparer.OrdinalIgnoreCase;

        Equality.Using<string>(comparer).HashingComparer.ShouldBeSameAs(comparer);
    }

    // How an assertion asks Equality: by Shouldly's rules, unless a comparer of the caller's is given.
    private static bool Equal<T>(T? actual, T? expected, IEqualityComparer<T>? comparer = null) =>
        (comparer is null ? Equality<T>.Default : Equality.Using(comparer)).Equal(actual, expected);

    private static bool SequenceEqual<T>(IEnumerable<T>? actual, IEnumerable<T>? expected, bool ignoreOrder = false, IEqualityComparer<T>? comparer = null) =>
        Equality.OfSequences(comparer is null ? Equality<T>.Default : Equality.Using(comparer), ignoreOrder).Equal(actual, expected);

    private static List<object> NestedList(int item) => [new List<object> { new List<int> { item } }];

    private static KeyValuePair<string, List<int>> Pair(string key, List<int> value) => new(key, value);

    // Implements IEquatable<T> without overriding Equals(object) (CA1067).
#pragma warning disable CA1067
    private sealed class EquatableOnly(int value) : IEquatable<EquatableOnly>
    {
        public int Value { get; } = value;

        public bool Equals(EquatableOnly? other) => other?.Value == Value;
    }
#pragma warning restore CA1067

    private sealed class SpyEquatable : IEquatable<SpyEquatable>
    {
        public bool EqualsCalled { get; private set; }

        public bool Equals(SpyEquatable? other)
        {
            EqualsCalled = true;
            return true;
        }

        public override bool Equals(object? obj) => Equals(obj as SpyEquatable);

        public override int GetHashCode() => 0;
    }
}
