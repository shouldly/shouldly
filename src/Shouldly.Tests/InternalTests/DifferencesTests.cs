using Shouldly.Internals;
using Shouldly.Tests.TestHelpers;

namespace Shouldly.Tests.InternalTests;

/// <summary>
/// What tells two values apart as an <see cref="Equality"/> compares them, tested through <see cref="Differences"/>'s
/// own interface rather than through each failure message that shows it. The wordings only need a test showing they
/// read it.
/// </summary>
public class DifferencesTests
{
    // Each case is two sequences, how they were compared, and the positions found to differ, as the actual sequence's
    // index, or as "*" for a position where only the expected one has an item. Keyed by name so that each row is
    // discovered, and reported, as its own test.
    private static readonly Dictionary<string, (Func<Differences> Find, string Marked)> InOrderCases = new()
    {
        ["one item differs"] = (() => Between([2, 2, 3], [1, 2, 3]), "0"),
        ["actual is longer"] = (() => Between([1, 2, 3], [1]), "1 2"),
        ["actual is shorter"] = (() => Between([1], [1, 2, 3]), "* *"),
        ["same items in another order"] = (() => Between([2, 3, 1], [1, 2, 3]), "0 1 2"),
        ["equal"] = (() => Between([1, 2], [1, 2]), ""),

        // Items are compared as the check compared them, never by a stricter rule of the message's own
        ["within a tolerance"] = (
            () => Equality.OfSequences(Equality.WithinTolerance<double>(0.01)).DifferencesBetween(new[] { 1.0, 2.0 }, new[] { 1.001, 2.5 }),
            "1"),
        ["ignoring case"] = (
            () => Equality.OfSequences(Equality.ForCase(Case.Insensitive)).DifferencesBetween(new[] { "A", "b" }, new[] { "a", "c" }),
            "1"),
        ["a comparer of the caller's"] = (
            () => Equality.OfSequences(Equality.Using<string>(StringComparer.OrdinalIgnoreCase)).DifferencesBetween(new[] { "A", "b" }, new[] { "a", "c" }),
            "1"),
        ["a comparer of the caller's for the whole value"] = (
            () => Equality.Using<int[]>(new NeverEqual<int[]>()).DifferencesBetween(new[] { 1, 2 }, new[] { 1, 3 }),
            "1"),
    };

    public static TheoryData<string> InOrderCaseNames => [.. InOrderCases.Keys];

    [Theory]
    [MemberData(nameof(InOrderCaseNames))]
    public void MarksThePositionsTheComparisonFoundDifferent(string name)
    {
        var (find, marked) = InOrderCases[name];

        var inOrder = find().InOrder.ShouldNotBeNull();

        string.Join(" ", inOrder.Positions
                .Select((position, index) => (position, index))
                .Where(p => p.position.Differs)
                .Select(p => p.position.HasActual ? p.index.ToString() : "*"))
            .ShouldBe(marked);
    }

    [Fact]
    public void GivesTheFirstDifferentPair()
    {
        var inOrder = Between([1, 2, 3], [1, 5, 6]).InOrder.ShouldNotBeNull();

        inOrder.FirstIndex.ShouldBe(1);
        inOrder.FirstIsPair.ShouldBeTrue();
        inOrder.FirstActual.ShouldBe(2);
        inOrder.FirstExpected.ShouldBe(5);
    }

    [Fact]
    public void WalksNoFurtherThanAMessageShows()
    {
        using (MaxEnumerableItemsScope.Of(3))
        {
            var inOrder = Between(Forever(), Forever()).InOrder.ShouldNotBeNull();

            inOrder.Positions.Count.ShouldBe(3);
            inOrder.CutShort.ShouldBeTrue();
            inOrder.FirstIndex.ShouldBeNull();
        }
    }

    [Fact]
    public void WalksInOrderOnlySequencesComparedInOrder()
    {
        Default("abc", "abd").InOrder.ShouldBeNull();
        Default(new HashSet<int> { 1 }, new[] { 2 }).InOrder.ShouldBeNull();
        Default(null, new[] { 1 }).InOrder.ShouldBeNull();
        Default(1, 2).InOrder.ShouldBeNull();
    }

    [Fact]
    public void TellsTwoSetsApartByWhatEachHas()
    {
        var unordered = Default(new HashSet<int> { 1, 2 }, new HashSet<int> { 2, 3 }).Unordered.ShouldNotBeNull();

        unordered.ByKey.ShouldBeFalse();
        unordered.Items.ShouldBe([
            new UnorderedDifference(UnorderedDifferenceKind.Missing, 3, 3, null),
            new UnorderedDifference(UnorderedDifferenceKind.Unexpected, 1, null, 1)
        ]);
    }

    [Fact]
    public void TellsTwoDictionariesApartByKey()
    {
        var unordered = Default(
                new Dictionary<string, int> { ["a"] = 1, ["b"] = 2 },
                new Dictionary<string, int> { ["a"] = 1, ["b"] = 3 })
            .Unordered.ShouldNotBeNull();

        unordered.ByKey.ShouldBeTrue();
        unordered.Items.ShouldBe([new UnorderedDifference(UnorderedDifferenceKind.ValueDiffers, "b", 3, 2)]);
    }

    [Fact]
    public void MatchesADictionarysEntriesWholeWhenACallersComparerComparedThem()
    {
        var comparedWith = Equality.OfSequences(Equality.Using<KeyValuePair<string, int>>(new NeverEqual<KeyValuePair<string, int>>()));

        var unordered = comparedWith.DifferencesBetween(
                new Dictionary<string, int> { ["a"] = 1 },
                new Dictionary<string, int> { ["a"] = 1 })
            .Unordered.ShouldNotBeNull();

        unordered.ByKey.ShouldBeFalse();
        unordered.Items.Select(item => item.Kind).ShouldBe([UnorderedDifferenceKind.Missing, UnorderedDifferenceKind.Unexpected]);
    }

    [Fact]
    public void ComparesSequencesInOrderWithoutUnorderedDifferences()
    {
        Default(new[] { 1 }, new[] { 2 }).Unordered.ShouldBeNull();
    }

    [Fact]
    public void LeavesUnpairedTheItemsAMatchInAnyOrderCouldNotPair()
    {
        var unpaired = Equality.OfSequences(Equality<int>.Default, ignoreOrder: true)
            .DifferencesBetween(new[] { 1, 2, 2 }, new[] { 2, 1, 3 })
            .Unpaired.ShouldNotBeNull();

        unpaired.OnlyInActual.ShouldBe([2]);
        unpaired.OnlyInExpected.ShouldBe([3]);
        unpaired.ActualCutShort.ShouldBeFalse();
        unpaired.ExpectedCutShort.ShouldBeFalse();
    }

    [Fact]
    public void PairsItemsWithinAToleranceAsTheCheckDoes()
    {
        // 1.0 is within the tolerance of both 1.009 and 0.991, but 1.018 only of 1.009: pairing 1.0 with 1.009 first
        // would leave 1.018 alone.
        var unpaired = Equality.OfSequences(Equality.WithinTolerance<double>(0.01), ignoreOrder: true)
            .DifferencesBetween(new[] { 1.0, 1.018 }, new[] { 1.009, 0.991 })
            .Unpaired.ShouldNotBeNull();

        unpaired.OnlyInActual.ShouldBeEmpty();
        unpaired.OnlyInExpected.ShouldBeEmpty();
    }

    [Fact]
    public void NamesTheStepOfShouldlysRulesThatComparedThem()
    {
        Default(new[] { 1 }, new[] { 2 }).Step?.Kind.ShouldBe(EqualityStepKind.Sequence);
        Default(new HashSet<int> { 1 }, new HashSet<int> { 2 }).Step?.Kind.ShouldBe(EqualityStepKind.UnorderedCollection);
        Default((1, 2), (1, 3)).Step?.Kind.ShouldBe(EqualityStepKind.Tuple);
        Default("a", "b").Step?.Kind.ShouldBe(EqualityStepKind.String);
        Default(1, 2L).Step?.Kind.ShouldBe(EqualityStepKind.Number);
        Default(new Uri("http://a"), new Uri("http://b")).Step?.Kind.ShouldBe(EqualityStepKind.OwnEquality);

        var rule = Default(new IdBag(1, 1), new IdBag(2, 1)).Step.ShouldNotBeNull();
        rule.Kind.ShouldBe(EqualityStepKind.Rule);
        rule.RuleType.ShouldBe(typeof(IdBag));
    }

    [Fact]
    public void NamesNoStepWhenNoneOfShouldlysRulesHadASay()
    {
        Default(null, 1).Step.ShouldBeNull();
        Equality.Using<int>(new NeverEqual<int>()).DifferencesBetween(1, 1).Step.ShouldBeNull();
    }

    [Fact]
    public void LeavesRulesOutWhenACallersComparerComparedTheElements()
    {
        Equality.OfSequences(Equality.Using<int>(new NeverEqual<int>())).DifferencesBetween(new IdBag(1, 1), new IdBag(1, 1))
            .Step?.Kind.ShouldBe(EqualityStepKind.Sequence);
    }

    private static Differences Between(IEnumerable<int> actual, IEnumerable<int> expected) =>
        Equality.OfSequences(Equality<int>.Default).DifferencesBetween(actual, expected);

    private static Differences Default(object? actual, object? expected) =>
        Equality<object?>.Default.DifferencesBetween(actual, expected);

    private static IEnumerable<int> Forever()
    {
        while (true)
            yield return 1;
    }

    private sealed class NeverEqual<T> : IEqualityComparer<T>
    {
        public bool Equals(T? x, T? y) => false;

        public int GetHashCode(T obj) => 0;
    }
}
