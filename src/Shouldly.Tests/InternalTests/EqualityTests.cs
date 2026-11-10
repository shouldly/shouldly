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
        ["int, same"] = (() => Equality.Equal(1, 1), true),
        ["int, different"] = (() => Equality.Equal(1, 2), false),
        ["null, null"] = (() => Equality.Equal<object>(null, null), true),
        ["null actual"] = (() => Equality.Equal<object>(null, 1), false),
        ["null expected"] = (() => Equality.Equal<object>(1, null), false),
        ["string, same"] = (() => Equality.Equal("a", "a"), true),
        ["string, different case"] = (() => Equality.Equal("a", "A"), false),
        ["Guid, same"] = (() => Equality.Equal(Guid.Empty, new Guid()), true),

        // Numbers of different types compare by value, exactly (#1426, #1427)
        ["int vs long, same value"] = (() => Equality.Equal<object>(1, 1L), true),
        ["int vs double, same value"] = (() => Equality.Equal<object>(1, 1.0), true),
        ["uint.MaxValue vs -1"] = (() => Equality.Equal<object>(uint.MaxValue, -1), false),
        ["decimal 0.1 vs double 0.1"] = (() => Equality.Equal<object>(0.1m, 0.1), false),

        // A default ImmutableArray counts as null (#1406)
        ["default ImmutableArray vs null"] = (() => Equality.Equal<object>(default(ImmutableArray<int>), null), true),
        ["default ImmutableArray vs empty"] = (() => Equality.Equal<object>(default(ImmutableArray<int>), ImmutableArray<int>.Empty), false),

        // Collections are walked structurally, at any depth
        ["nested lists, same items"] = (() => Equality.Equal(NestedList(1), NestedList(1)), true),
        ["nested lists, different items"] = (() => Equality.Equal(NestedList(1), NestedList(2)), false),
        ["array vs list, same items"] = (() => Equality.Equal<IEnumerable<int>>(new[] { 1, 2 }, new List<int> { 1, 2 }), true),

        // Key/value pairs compare their key and value with the same rules (#1408)
        ["KeyValuePair, structurally equal values"] = (() => Equality.Equal(Pair("k", [1, 2]), Pair("k", [1, 2])), true),
        ["KeyValuePair, different values"] = (() => Equality.Equal(Pair("k", [1, 2]), Pair("k", [1, 3])), false),

        // IEquatable<T> is honoured even where the static type has been lost (#825)
        ["IEquatable-only elements as objects"] = (() => Equality.Equal(new List<object> { new EquatableOnly(1) }, [new EquatableOnly(1)]), true),

        // A registered equality rule decides, for the type and its subtypes, at any depth (see ModuleInitializer)
        ["rule, own Equals, same id, different items"] = (() => Equality.Equal(new IdBag(1, 1), new IdBag(1, 2)), true),
        ["rule, own Equals, different id, same items"] = (() => Equality.Equal(new IdBag(1, 1), new IdBag(2, 1)), false),
        ["rule, subtype, same leaf"] = (() => Equality.Equal<Node>(new Leaf("a"), new Leaf("a")), true),
        ["rule, subtype, different leaf (walked, both are empty)"] = (() => Equality.Equal<Node>(new Leaf("a"), new Leaf("b")), false),
        ["rule, deep equal branches"] = (() => Equality.Equal<Node>(new Branch(new Leaf("a")), new Branch(new Leaf("a"))), true),
        ["rule, different branches"] = (() => Equality.Equal<Node>(new Branch(new Leaf("a")), new Branch(new Leaf("b"))), false),
        ["rule, static subtype"] = (() => Equality.Equal(new Leaf("a"), new Leaf("b")), false),
        ["rule, whole value through the sequence overload"] = (() => Equality.SequenceEqual<Node>(new Leaf("a"), new Leaf("b")), false),
        ["rule, sequence of equal values"] = (() => Equality.SequenceEqual<Node>([new Leaf("a")], [new Leaf("a")]), true),
        ["rule, sequence of different values"] = (() => Equality.SequenceEqual<Node>([new Leaf("a")], [new Leaf("b")]), false),
        ["rule, ignoring order, permuted"] = (() => Equality.SequenceEqual<Node>([new Leaf("a"), new Leaf("b")], [new Leaf("b"), new Leaf("a")], ignoreOrder: true), true),
        ["rule, ignoring order, different"] = (() => Equality.SequenceEqual<Node>([new Leaf("a")], [new Leaf("b")], ignoreOrder: true), false),
        ["rule, nested as objects"] = (() => Equality.Equal(new List<object> { new Leaf("a") }, [new Leaf("b")]), false),
        ["rule, KeyValuePair value"] = (() => Equality.Equal(new KeyValuePair<string, Node>("k", new Leaf("a")), new("k", new Leaf("b"))), false),
        ["rule, KeyValuePair value as object"] = (() => Equality.Equal(new KeyValuePair<string, object>("k", new Leaf("a")), new("k", new Leaf("b"))), false),
        ["rule, most specific wins"] = (() => Equality.Equal(new CaseInsensitiveLeaf("A"), new CaseInsensitiveLeaf("a")), true),
        ["rule, applies only when both values are its type"] = (() => Equality.Equal<Leaf>(new CaseInsensitiveLeaf("A"), new Leaf("a")), false),

        // A caller-supplied comparer decides
        ["custom comparer"] = (() => Equality.Equal("a", "A", StringComparer.OrdinalIgnoreCase), true),

        // Sequences in order
        ["sequence, same order"] = (() => Equality.SequenceEqual([1, 2, 3], [1, 2, 3]), true),
        ["sequence, different order"] = (() => Equality.SequenceEqual([1, 2, 3], [3, 2, 1]), false),
        ["sequence, actual shorter"] = (() => Equality.SequenceEqual([1, 2], [1, 2, 3]), false),
        ["sequence, actual longer"] = (() => Equality.SequenceEqual([1, 2, 3], [1, 2]), false),
        ["sequence, null vs null"] = (() => Equality.SequenceEqual<int>(null, null), true),
        ["sequence, null vs empty"] = (() => Equality.SequenceEqual<int>(null, []), false),
        ["sequence, default ImmutableArray vs null"] = (() => Equality.SequenceEqual<int>(default(ImmutableArray<int>), null), true),
        ["sequence, custom comparer"] = (() => Equality.SequenceEqual(["a"], ["A"], comparer: StringComparer.OrdinalIgnoreCase), true),

        // Sequences ignoring order match elements one for one
        ["ignoring order, permuted"] = (() => Equality.SequenceEqual([1, 2, 2], [2, 1, 2], ignoreOrder: true), true),
        ["ignoring order, different counts"] = (() => Equality.SequenceEqual([1, 1, 2], [1, 2, 2], ignoreOrder: true), false),
        ["ignoring order, missing default value"] = (() => Equality.SequenceEqual([1], [0], ignoreOrder: true), false),
        ["ignoring order, null vs null"] = (() => Equality.SequenceEqual<int>(null, null, ignoreOrder: true), true),

        // Tolerance
        ["double within tolerance"] = (() => Equality.Equal(1.0, 1.05, Equality.Within(0.1)), true),
        ["double outside tolerance"] = (() => Equality.Equal(1.0, 1.2, Equality.Within(0.1)), false),
        ["double at tolerance boundary"] = (() => Equality.Equal(1.0, 1.5, Equality.Within(0.5)), true),
        ["float sequence within tolerance"] = (() => Equality.SequenceEqual([1f, 2f], [1.05f, 2.05f], comparer: Equality.Within(0.1)), true),
        ["decimal within tolerance"] = (() => Equality.Equal(1.0m, 1.05m, Equality.Within(0.1m)), true),
        ["decimal outside tolerance"] = (() => Equality.Equal(1.0m, 1.2m, Equality.Within(0.1m)), false),

        // Case
        ["case insensitive"] = (() => Equality.Equal("a", "A", Equality.For(Case.Insensitive)), true),
        ["case sensitive"] = (() => Equality.Equal("a", "A", Equality.For(Case.Sensitive)), false),
        ["case insensitive sequence"] = (() => Equality.SequenceEqual(["a", "b"], ["A", "B"], comparer: Equality.For(Case.Insensitive)), true),
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

        Equality.Equal(actual, expected);

        expected.EqualsCalled.ShouldBeTrue();
        actual.EqualsCalled.ShouldBeFalse();
    }

    [Fact]
    public void CallsEqualsOnTheExpectedElement()
    {
        var actual = new SpyEquatable();
        var expected = new SpyEquatable();

        Equality.SequenceEqual([actual], [expected]);

        expected.EqualsCalled.ShouldBeTrue();
        actual.EqualsCalled.ShouldBeFalse();
    }

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
