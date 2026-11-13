using System.Numerics;
using Shouldly.Internals;
using Shouldly.Internals.Failures;

namespace Shouldly.Tests.InternalTests;

/// <summary>
/// The line a <c>ShouldBe</c> failure adds when its message doesn't otherwise show why two values are unequal (#1432),
/// tested through <see cref="ExplanationWording"/>'s own interface. The assertions only need a smoke test showing they ask it.
/// </summary>
public class ExplanationWordingTests
{
    private const string UseEquivalentTo = "use ShouldBeEquivalentTo to compare members";

    // Each case is a failed comparison and the line that explains it, or null when the message needs none. Keyed by
    // name so that each row is discovered, and reported, as its own test.
    private static readonly Dictionary<string, (Func<string?> Explain, string? Line)> Cases = new()
    {
        // Values that read differently need no explanation
        ["different numbers"] = (() => Explain(1, 2), null),
        ["a number and its text"] = (() => Explain(5, "5"), null),
        ["null actual"] = (() => Explain(null, new Named("a")), null),
        ["null expected"] = (() => Explain(new Named("a"), null), null),
        ["names that differ"] = (() => Explain(new Named("a"), new Named("b")), null),
        ["dates a tick apart"] = (() => Explain(new DateTime(1), new DateTime(2)), null),
        ["enums of different types with one name"] = (() => Explain(DayOfWeek.Monday, Weekday.Monday), null),

        // Different types
        ["different types"] = (
            () => Explain(new BigInteger(5), 5),
            "but they are different types (expected System.Int32, actual System.Numerics.BigInteger)"),
        ["different types, told apart only by a numeric suffix"] = (
            () => Explain((Int128)5, 5L),
            "but they are different types (expected System.Int64, actual System.Int128)"),
        ["different types, floating point"] = (
            () => Explain((Half)0.1, 0.1),
            "but they are different types (expected System.Double, actual System.Half)"),
        ["different generic types"] = (
            () => Explain(new Box<int>(), new Box<long[]>()),
            "but they are different types (expected Shouldly.Tests.InternalTests.ExplanationWordingTests+Box<System.Int64[]>, actual Shouldly.Tests.InternalTests.ExplanationWordingTests+Box<System.Int32>)"),

        // One type, compared by reference
        ["reference equality"] = (
            () => Explain(new Named("a"), new Named("a")),
            $"but they are different instances, and Named compares by reference; {UseEquivalentTo}"),
        ["reference equality, told apart only by a hash code"] = (
            () => Explain(new Plain(), new Plain()),
            "but they are different instances, and Plain compares by reference"),
        ["reference equality, generic type"] = (
            () => Explain(new Box<int>(), new Box<int>()),
            $"but they are different instances, and Box<Int32> compares by reference; {UseEquivalentTo}"),
        ["reference equality, exception"] = (
            () => Explain(new InvalidOperationException("x"), new InvalidOperationException("x")),
            $"but they are different instances, and InvalidOperationException compares by reference; {UseEquivalentTo}"),

        // One type, with its own equality
        ["Equals override"] = (() => Explain(new Versioned("a", 1), new Versioned("a", 2)), "but Versioned.Equals returned false"),
        ["IEquatable without an Equals override"] = (() => Explain(new NeverEquatable(), new NeverEquatable()), "but NeverEquatable.Equals returned false"),
        ["IComparable without Equals"] = (() => Explain(new Ranked("a", 1), new Ranked("a", 2)), "but Ranked.CompareTo did not return 0"),
        ["Equals that throws when asked again"] = (() => Explain(new ThrowsOnEquals(), new ThrowsOnEquals()), "but they are not equal as ThrowsOnEquals compares them"),

        // Records, tuples, anonymous types and plain structs compare each member with its own Equals
        ["record with a collection member"] = (
            () => Explain(new Order(1, ["a"]), new Order(1, ["a"])),
            $"but at Lines, they are different instances, and List<String> compares by reference; {UseEquivalentTo}"),
        ["record with collection members of different items"] = (
            () => Explain(new Order(1, ["a"]), new Order(1, ["b"])),
            """but at Lines, they differ (expected ["b"], actual ["a"])"""),
        ["record with a member its text leaves out"] = (
            () => Explain(new Hidden("bob", 30), new Hidden("bob", 31)),
            "but at Age, they differ (expected 31, actual 30)"),
        ["record with a null member"] = (
            () => Explain(new Hidden(null, 30), new Hidden("null", 30)),
            """but at Name, they differ (expected "null", actual null)"""),
        ["record within a record"] = (
            () => Explain(new Wrapper(new Order(1, ["a"])), new Wrapper(new Order(1, ["a"]))),
            $"but at Inner.Lines, they are different instances, and List<String> compares by reference; {UseEquivalentTo}"),
        ["record struct"] = (
            () => Explain(new Point(1, [1]), new Point(1, [1])),
            $"but at Tags, they are different instances, and Int32[] compares by reference; {UseEquivalentTo}"),
        ["struct without an Equals override"] = (
            () => Explain(new Raw { Id = 1, Items = [] }, new Raw { Id = 1, Items = [] }),
            $"but at Items, they are different instances, and List<Int32> compares by reference; {UseEquivalentTo}"),
        ["record whose difference no public member shows"] = (
            () => Explain(new Secretive(1), new Secretive(2)),
            "but Secretive.Equals returned false"),
        ["record with a member that can't be read"] = (
            () => Explain(new Unreadable(1), new Unreadable(2)),
            "but Unreadable.Equals returned false"),
        // A tuple's items are compared by Shouldly's equality (#1429), so a collection item is walked and numbers
        // compare across types: what differs is found inside the item.
        ["tuple with a record element"] = (
            () => Explain((1, new Order(1, ["a"])), (1, new Order(1, ["a"]))),
            $"but at Item2.Lines, they are different instances, and List<String> compares by reference; {UseEquivalentTo}"),
        // An item held in a tuple's Rest is numbered as C# numbers it.
        ["tuple that differs in its eighth item"] = (
            () => Explain((1, 2, 3, 4, 5, 6, 7, new[] { 8 }), (1, 2, 3, 4, 5, 6, 7, new[] { 9 })),
            "but at Item8, they differ (expected [9], actual [8])"),
        ["Tuple that differs in its ninth item"] = (
            () => Explain(
                new Tuple<int, int, int, int, int, int, int, Tuple<int, int[]>>(1, 2, 3, 4, 5, 6, 7, Tuple.Create(8, new[] { 9 })),
                new Tuple<int, int, int, int, int, int, int, Tuple<int, int[]>>(1, 2, 3, 4, 5, 6, 7, Tuple.Create(8, new[] { 0 }))),
            "but at Item9, they differ (expected [0], actual [9])"),
        ["tuple that differs in its sixteenth item"] = (
            () => Explain(
                (1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, new[] { 16 }),
                (1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, new[] { 17 })),
            "but at Item16, they differ (expected [17], actual [16])"),
        ["tuples of different element types"] = (
            () => Explain((1, new BigInteger(2)), (1, (Int128)2)),
            "but at Item2, they are different types (expected System.Int128, actual System.Numerics.BigInteger)"),
        ["anonymous types of different member types"] = (
            () => Explain(new { A = 1 }, new { A = 1L }),
            "but at A, they are different types (expected System.Int64, actual System.Int32)"),

        // An equality rule or the caller's comparer decides alone
        ["equality rule"] = (
            () => Explain(new IdBag(1, 1, 2), new IdBag(2, 1, 2)),
            "but the equality rule for IdBag says they are not equal"),
        ["equality rule, values that read differently"] = (() => Explain(new IdBag(1, 1), new IdBag(2, 2)), null),
        ["custom comparer"] = (
            () => Explain(new Named("a"), new Named("a"), Equality.Using(System.Collections.Generic.EqualityComparer<object>.Default)),
            "but the custom comparer says they are not equal"),
        ["custom comparer, values that read differently"] = (
            () => Explain(new Named("a"), new Named("b"), Equality.Using(System.Collections.Generic.EqualityComparer<object>.Default)),
            null),
        ["custom comparer overrides an equality rule"] = (
            () => Explain(new IdBag(1, 1), new IdBag(2, 1), Equality.Using(System.Collections.Generic.EqualityComparer<object>.Default)),
            "but the custom comparer says they are not equal"),

        // Sequences say where they first differ once that takes finding
        ["sequences, difference near the start"] = (() => Explain(Numbers(20), Numbers(20, changedAt: 9)), null),
        ["sequences, difference further on"] = (() => Explain(Numbers(20), Numbers(20, changedAt: 10)), "first difference at index 10"),
        ["sequences, actual ends early"] = (() => Explain(Numbers(15), Numbers(20)), "first difference at index 15"),
        ["sequences, expected ends early"] = (() => Explain(Numbers(20), Numbers(15)), "first difference at index 15"),
        ["sequences of strings"] = (
            () => Explain(Numbers(20).Select(n => n.ToString()), Numbers(20, changedAt: 12).Select(n => n.ToString())),
            "first difference at index 12"),
        ["memories"] = (
            () => Explain(Numbers(20).AsMemory(), Numbers(20, changedAt: 12).AsMemory()),
            "first difference at index 12"),

        // Items that read the same are explained wherever they are
        ["items that read the same"] = (
            () => Explain(new[] { new Order(1, ["a"]) }, new[] { new Order(1, ["a"]) }),
            $"first difference at index 0: at Lines, they are different instances, and List<String> compares by reference; {UseEquivalentTo}"),
        ["items of different types"] = (
            () => Explain(new object[] { 1, new BigInteger(5) }, new object[] { 1, (Int128)5 }),
            "first difference at index 1: they are different types (expected System.Int128, actual System.Numerics.BigInteger)"),
        ["items within items"] = (
            () => Explain(new object[] { new object[] { 1, (1, new BigInteger(2)) } }, new object[] { new object[] { 1, (1, (Int128)2) } }),
            "first difference at index 0: at [1].Item2, they are different types (expected System.Int128, actual System.Numerics.BigInteger)"),
        ["dictionary values"] = (
            () => Explain(new Dictionary<string, Order> { ["k"] = new(1, ["a"]) }, new Dictionary<string, Order> { ["k"] = new(1, ["a"]) }),
            $"but at [\"k\"].Lines, they are different instances, and List<String> compares by reference; {UseEquivalentTo}"),
        ["items an equality rule covers"] = (
            () => Explain(new[] { new IdBag(1, 1) }, new[] { new IdBag(2, 1) }),
            "first difference at index 0: the equality rule for IdBag says they are not equal"),
        ["items compared by a custom comparer"] = (
            () => Explain(new[] { new Named("a") }, new[] { new Named("a") }, Equality.OfSequences(Equality.Using(new NeverEqual<Named>()))),
            "first difference at index 0: the custom comparer says they are not equal"),
    };

    public static TheoryData<string> CaseNames => [.. Cases.Keys];

    [Theory]
    [MemberData(nameof(CaseNames))]
    public void Explains(string name)
    {
        var (explain, line) = Cases[name];

        explain().ShouldBe(line);
    }

    [Fact]
    public void SaysWhenTheFirstDifferenceIsPastTheItemsShown()
    {
        using (MaxEnumerableItemsScope.Of(3))
        {
            Explain(Numbers(20), Numbers(20, changedAt: 12)).ShouldBe("the first difference is after the 3 items shown");
        }
    }

    [Fact]
    public void ReadsAnInfiniteSequenceNoFurtherThanAMessageShows()
    {
        using (MaxEnumerableItemsScope.Of(3))
        {
            Explain(Forever(1), Forever(1)).ShouldBe("the first difference is after the 3 items shown");
        }
    }

    private static string? Explain(object? actual, object? expected, Equality? equality = null) =>
        ExplanationWording.Explain((equality ?? Equality<object?>.Default).DifferencesBetween(actual, expected));

    private static int[] Numbers(int count, int? changedAt = null) =>
        [.. Enumerable.Range(0, count).Select(n => n == changedAt ? -1 : n)];

    private static IEnumerable<int> Forever(int value)
    {
        while (true)
            yield return value;
    }

    private enum Weekday
    {
        Monday = 1
    }

    // With no public members, ShouldBeEquivalentTo has nothing to compare it by, so the explanation doesn't suggest it.
    private sealed class Plain;

    private sealed class Named(string name)
    {
        public string Name => name;

        public override string ToString() => name;
    }

    private sealed class Box<T>
    {
        public T? Value { get; set; }

        public override string ToString() => "box";
    }

    private sealed class Versioned(string name, int version)
    {
        private int Version { get; } = version;

        public override bool Equals(object? obj) => obj is Versioned other && other.Version == Version;

        public override int GetHashCode() => Version;

        public override string ToString() => name;
    }

#pragma warning disable CA1067
    private sealed class NeverEquatable : IEquatable<NeverEquatable>
    {
        public bool Equals(NeverEquatable? other) => false;

        public override string ToString() => "never";
    }
#pragma warning restore CA1067

    private sealed class Ranked(string name, int rank) : IComparable
    {
        private int Rank { get; } = rank;

        public int CompareTo(object? obj) => Rank.CompareTo(((Ranked)obj!).Rank);

        public override string ToString() => name;
    }

    private sealed class ThrowsOnEquals
    {
        public override bool Equals(object? obj) => throw new InvalidOperationException("Equals was called again");

        public override int GetHashCode() => 0;

        public override string ToString() => "throws";
    }

    private sealed record Order(int Id, List<string> Lines);

    private sealed record Wrapper(Order Inner);

    private sealed record Hidden(string? Name, int Age)
    {
        public override string ToString() => Name ?? "null";
    }

    private readonly record struct Point(int X, int[] Tags);

    private struct Raw
    {
        public int Id;
        public List<int> Items;

        public override string ToString() => $"Raw({Id})";
    }

    private sealed record Secretive
    {
        private readonly int _secret;

        public Secretive(int secret) => _secret = secret;

        public override string ToString() => "secretive";

        public int Reveal() => _secret;
    }

    private sealed record Unreadable
    {
        private readonly int _secret;

        public Unreadable(int secret) => _secret = secret;

        public int Secret => throw new InvalidOperationException($"{_secret} can't be read");

        public override string ToString() => "unreadable";
    }

    private sealed class NeverEqual<T> : IEqualityComparer<T>
    {
        public bool Equals(T? x, T? y) => false;

        public int GetHashCode(T obj) => 0;
    }
}
