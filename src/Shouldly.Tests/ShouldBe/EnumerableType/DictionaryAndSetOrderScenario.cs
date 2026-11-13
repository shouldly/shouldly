using System.Collections.Immutable;
using System.Collections.ObjectModel;

namespace Shouldly.Tests.ShouldBe.EnumerableType;

/// <summary>
/// <c>ShouldBe</c> compares dictionaries and sets without regard to the order they enumerate in, which is an
/// implementation detail of the collection: when both sides are dictionaries or sets, and when one is and keeps no
/// order of its own to compare with a list's. A dictionary's entries are matched by key and their values compared,
/// as <c>ShouldBeEquivalentTo</c> does. See issue #1430.
/// </summary>
public class DictionaryAndSetOrderScenario
{
    [Fact]
    public void DictionariesInDifferentOrderAreEqual() =>
        new Dictionary<string, int> { ["a"] = 1, ["b"] = 2 }
            .ShouldBe(new Dictionary<string, int> { ["b"] = 2, ["a"] = 1 });

    [Fact]
    public void ADictionaryEqualsASortedDictionaryWithTheSameEntries() =>
        new Dictionary<string, int> { ["b"] = 2, ["a"] = 1 }
            .ShouldBe(new SortedDictionary<string, int> { ["a"] = 1, ["b"] = 2 });

    [Fact]
    public void ReadOnlyAndImmutableDictionariesAreComparedByKey()
    {
        var actual = new Dictionary<string, int> { ["b"] = 2, ["a"] = 1 };

        actual.ShouldBe(new ReadOnlyDictionary<string, int>(new Dictionary<string, int> { ["a"] = 1, ["b"] = 2 }));
        actual.ShouldBe(ImmutableSortedDictionary.CreateRange(new Dictionary<string, int> { ["a"] = 1, ["b"] = 2 }));
    }

    [Fact]
    public void AReadOnlyDictionaryThatIsNoOtherKindOfDictionaryIsComparedByKey() =>
        new OnlyReadOnlyDictionary(("b", 2), ("a", 1))
            .ShouldBe(new OnlyReadOnlyDictionary(("a", 1), ("b", 2)));

    [Fact]
    public void SetsInDifferentOrderAreEqual() =>
        new HashSet<int> { 2, 1 }.ShouldBe(new HashSet<int> { 1, 2 });

    [Fact]
    public void AHashSetEqualsASortedSetWithTheSameElements() =>
        new HashSet<int> { 3, 1, 2 }.ShouldBe(new SortedSet<int> { 1, 2, 3 });

    [Fact]
    public void ObjectTypedDictionariesAndSetsAreComparedWithoutOrder()
    {
        ((object)new Dictionary<string, int> { ["a"] = 1, ["b"] = 2 })
            .ShouldBe(new Dictionary<string, int> { ["b"] = 2, ["a"] = 1 });
        ((object)new HashSet<int> { 2, 1 }).ShouldBe(new HashSet<int> { 1, 2 });
    }

    [Fact]
    public void DictionariesNestedInASequenceAreComparedByKey() =>
        new List<Dictionary<string, int>> { new() { ["a"] = 1, ["b"] = 2 } }
            .ShouldBe([new() { ["b"] = 2, ["a"] = 1 }]);

    [Fact]
    public void DictionariesAndSetsNestedInADictionaryAreComparedWithoutOrder()
    {
        new Dictionary<string, Dictionary<string, int>> { ["x"] = new() { ["a"] = 1, ["b"] = 2 } }
            .ShouldBe(new Dictionary<string, Dictionary<string, int>> { ["x"] = new() { ["b"] = 2, ["a"] = 1 } });
        new Dictionary<string, HashSet<int>> { ["x"] = [2, 1] }
            .ShouldBe(new Dictionary<string, HashSet<int>> { ["x"] = [1, 2] });
    }

    [Fact]
    public void KeysAreComparedTheWayShouldBeComparesValues() =>
        // Numerics compare across types, as they do for values, so the key 1 matches the key 1L.
        new Dictionary<object, string> { [1] = "a", [2] = "b" }
            .ShouldBe(new Dictionary<object, string> { [2L] = "b", [1L] = "a" });

    [Fact]
    public void ADictionarysOwnKeyComparerIsNotUsed() =>
        // As with the values, ShouldBe compares the keys by its own rules, not by either dictionary's comparer.
        new Dictionary<string, int> { ["A"] = 1 }
            .ShouldNotBe(new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase) { ["a"] = 1 });

    [Fact]
    public void ShouldNotBeFailsForDictionariesInDifferentOrder() =>
        Should.Throw<ShouldAssertException>(() =>
            new Dictionary<string, int> { ["a"] = 1, ["b"] = 2 }
                .ShouldNotBe(new Dictionary<string, int> { ["b"] = 2, ["a"] = 1 }));

    [Fact]
    public void ShouldNotBeFailsForSetsInDifferentOrder() =>
        Should.Throw<ShouldAssertException>(() =>
            new HashSet<int> { 2, 1 }.ShouldNotBe(new HashSet<int> { 1, 2 }));

    [Fact]
    public void DictionariesWithDifferentContentsAreNotEqual()
    {
        var actual = new Dictionary<string, int> { ["a"] = 1, ["b"] = 2 };

        actual.ShouldNotBe(new Dictionary<string, int> { ["b"] = 3, ["a"] = 1 });
        actual.ShouldNotBe(new Dictionary<string, int> { ["b"] = 2 });
        actual.ShouldNotBe(new Dictionary<string, int> { ["b"] = 2, ["a"] = 1, ["c"] = 3 });
        actual.ShouldNotBe(new Dictionary<string, int> { ["b"] = 2, ["c"] = 1 });
    }

    [Fact]
    public void SetsWithDifferentElementsAreNotEqual()
    {
        new HashSet<int> { 2, 1 }.ShouldNotBe(new HashSet<int> { 1, 3 });
        new HashSet<int> { 2, 1 }.ShouldNotBe(new HashSet<int> { 1 });
    }

    // A HashSet has no order of its own for a list's to be compared with, whichever side it is on.
    [Fact]
    public void ASetHasNoOrderToCompareWithAList()
    {
        new HashSet<int> { 3, 2, 1 }.ShouldBe([1, 2, 3]);
        new HashSet<int> { 3, 2, 1 }.ShouldBe(new[] { 1, 2, 3 });
        new List<int> { 2, 1 }.ShouldBe(new HashSet<int> { 1, 2 });
        new HashSet<int> { 3, 2, 1 }.Where(_ => true).ShouldBe(new HashSet<int> { 1, 2, 3 });

        new HashSet<int> { 1, 2 }.ShouldNotBe<IEnumerable<int>>(new[] { 1, 2, 3 });
        new HashSet<int> { 1, 2 }.ShouldNotBe<IEnumerable<int>>(new[] { 1, 1, 2 });
        new HashSet<int> { 1, 2 }.ShouldNotBe<IEnumerable<int>>(new[] { 1, 3 });
    }

    [Fact]
    public void ADictionaryHasNoOrderToCompareWithAListOfItsEntries()
    {
        // Issue #882
        new Dictionary<string, int> { ["a"] = 1, ["b"] = 2 }
            .ShouldBe([new("b", 2), new("a", 1)]);
        new List<KeyValuePair<string, int>> { new("b", 2), new("a", 1) }
            .ShouldBe(new Dictionary<string, int> { ["a"] = 1, ["b"] = 2 });

        new Dictionary<string, int> { ["a"] = 1, ["b"] = 2 }
            .ShouldNotBe<IEnumerable<KeyValuePair<string, int>>>(new List<KeyValuePair<string, int>> { new("b", 2), new("a", 9) });
    }

    [Fact]
    public void ADictionaryEqualsASetOfItsEntries() =>
        new Dictionary<string, int> { ["a"] = 1, ["b"] = 2 }
            .ShouldBe(new HashSet<KeyValuePair<string, int>> { new("b", 2), new("a", 1) });

    [Fact]
    public void ASetInsideAListHasNoOrderToCompareWithAList() =>
        new List<IEnumerable<int>> { new HashSet<int> { 2, 1 } }
            .ShouldBe(new List<IEnumerable<int>> { new[] { 1, 2 } });

    // A sorted or ordered collection does have an order of its own, which a test may be about, so against a list it
    // is compared in order. Against another dictionary or set, it isn't.
    [Fact]
    public void ASortedSetIsComparedInOrderWithAList()
    {
        var descending = new SortedSet<int>(Comparer<int>.Create((x, y) => y.CompareTo(x))) { 1, 2, 3 };

        descending.ShouldBe([3, 2, 1]);
        descending.ShouldNotBe<IEnumerable<int>>(new[] { 1, 2, 3 });
        new[] { 1, 2, 3 }.ShouldNotBe<IEnumerable<int>>(descending);
        ImmutableSortedSet.Create(1, 2, 3).ShouldNotBe<IEnumerable<int>>(new[] { 3, 2, 1 });

        descending.ShouldBe(new HashSet<int> { 1, 2, 3 });
        descending.ShouldBe(new SortedSet<int> { 1, 2, 3 });
    }

    [Fact]
    public void ASortedOrOrderedDictionaryIsComparedInOrderWithAList()
    {
        new SortedDictionary<string, int> { ["b"] = 2, ["a"] = 1 }
            .ShouldBe([new("a", 1), new("b", 2)]);
        new SortedDictionary<string, int> { ["b"] = 2, ["a"] = 1 }
            .ShouldNotBe<IEnumerable<KeyValuePair<string, int>>>(new List<KeyValuePair<string, int>> { new("b", 2), new("a", 1) });
        new SortedList<string, int> { ["b"] = 2, ["a"] = 1 }
            .ShouldNotBe<IEnumerable<KeyValuePair<string, int>>>(new List<KeyValuePair<string, int>> { new("b", 2), new("a", 1) });
#if NET9_0_OR_GREATER
        new OrderedDictionary<string, int> { ["b"] = 2, ["a"] = 1 }
            .ShouldNotBe<IEnumerable<KeyValuePair<string, int>>>(new List<KeyValuePair<string, int>> { new("a", 1), new("b", 2) });
#endif

        new SortedDictionary<string, int> { ["b"] = 2, ["a"] = 1 }
            .ShouldBe(new Dictionary<string, int> { ["b"] = 2, ["a"] = 1 });
    }

    [Fact]
    public void SetAndListShouldFail() =>
        Verify.ShouldFail(() =>
            new HashSet<int> { 1, 2, 3 }.ShouldBe([1, 2, 4], "Some additional context"));

    [Fact]
    public void DictionaryAndListOfEntriesShouldFail() =>
        Verify.ShouldFail(() =>
            new Dictionary<string, int> { ["a"] = 1, ["b"] = 2 }
                .ShouldBe([new("a", 1), new("b", 20), new("c", 3)], "Some additional context"));

    [Fact]
    public void ACustomComparerComparesTheElementsOfSetsInAnyOrder() =>
        new HashSet<string> { "b", "a" }.ShouldBe(new HashSet<string> { "A", "B" }, StringComparer.OrdinalIgnoreCase);

    // Under this comparer 1.01 equals both 1.0 and 1.02, which don't equal each other. Whichever comes first, it
    // must be left for 1.03 to be paired with 1.02.
    [Fact]
    public void ItemsThatEqualMoreThanOneOtherArePairedSoThatEveryOneMatches()
    {
        var within = new Within(0.015);

        new HashSet<double> { 1.01, 1.03 }.ShouldBe(new[] { 1.02, 1.0 }, within);
        new HashSet<double> { 1.01, 1.03 }.ShouldBe(new[] { 1.0, 1.02 }, within);
        new[] { 1.02, 1.0 }.ShouldBe(new HashSet<double> { 1.01, 1.03 }, within);
        new HashSet<double> { 1.01, 1.03 }.ShouldBe(new HashSet<double> { 1.02, 1.0 }, within);
        new[] { 1.01, 1.03 }.ShouldBe(new[] { 1.02, 1.0 }, within, ignoreOrder: true);

        Should.Throw<ShouldAssertException>(() =>
            new HashSet<double> { 1.0, 1.01, 1.02 }.ShouldBe(new[] { 1.01, 1.02, 1.5 }, within));
    }

    // The message is written from what the assertion read, not by reading the sequence again.
    // The message pairs the items as the check did: 1.01 and 1.03 each have a partner, so neither is reported.
    [Fact]
    public void ItemsThatEqualMoreThanOneOtherShouldFail() =>
        Verify.ShouldFail(() =>
            new HashSet<double> { 1.01, 1.03, 5 }.ShouldBe([1.02, 1.0, 6], new Within(0.015), customMessage: "Some additional context"));

    [Fact]
    public void ItemsThatEqualMoreThanOneOtherIgnoringOrderShouldFail() =>
        Verify.ShouldFail(() =>
            new[] { 1.01, 1.03, 5 }.ShouldBe([1.02, 1.0, 6], new Within(0.015), ignoreOrder: true, "Some additional context"));

    [Fact]
    public void ASequenceThatCanOnlyBeReadOnceIsReadOnceAgainstASet()
    {
        new SinglePass<int>(3, 2, 1).ShouldBe(new HashSet<int> { 1, 2, 3 });
        new HashSet<int> { 1, 2, 3 }.ShouldBe(new SinglePass<int>(3, 2, 1));

        Should.Throw<ShouldAssertException>(() => new SinglePass<int>(1, 2, 3).ShouldBe(new HashSet<int> { 1, 2, 4 }))
            .Message.ShouldBe(Should.Throw<ShouldAssertException>(() => new[] { 1, 2, 3 }.ShouldBe(new HashSet<int> { 1, 2, 4 })).Message
                .Replace("new[] { 1, 2, 3 }", "new SinglePass<int>(1, 2, 3)"));
        Should.Throw<ShouldAssertException>(() => new HashSet<int> { 1, 2, 4 }.ShouldBe(new SinglePass<int>(1, 2, 3)))
            .Message.ShouldContain("3 is missing");
        Should.Throw<ShouldAssertException>(() =>
                new SinglePass<KeyValuePair<string, int>>(new KeyValuePair<string, int>("a", 1))
                    .ShouldBe(new Dictionary<string, int> { ["a"] = 2 }))
            .Message.ShouldContain("""["a"] should be 2 but was 1""");
    }

    [Fact]
    public void SinglePassSequenceAndSetShouldFail() =>
        Verify.ShouldFail(() =>
            new SinglePass<int>(1, 2, 3).ShouldBe(new HashSet<int> { 1, 2, 4 }, "Some additional context"));

    // The check reads the sequence one item past the end of the set, and the message no further than it shows.
    [Fact]
    public void ASequenceThatNeverEndsIsNotASet()
    {
        var message = Should.Throw<ShouldAssertException>(() => new GuardedSequence().ShouldBe(new HashSet<int> { 1, 2, 3 })).Message;

        message.ShouldNotContain("is missing");
    }

    [Fact]
    public void ASetIsNotASequenceThatNeverEnds()
    {
        var naturals = new GuardedSequence();

        Should.Throw<ShouldAssertException>(() => new HashSet<int> { 1, 2, 3 }.ShouldBe(naturals));

        // The check read one item past the end of the set. The message reads on as far as it shows.
        naturals.ElementsRead.ShouldBe(ShouldlyConfiguration.MaxEnumerableItemsInMessages + 1);

        Should.Throw<ShouldAssertException>(() => ((object)new HashSet<int> { 1, 2, 3 }).ShouldBe(new GuardedSequence()));
        Should.Throw<ShouldAssertException>(() => new[] { 1, 2, 3 }.ShouldBe(new GuardedSequence(), ignoreOrder: true));
        Should.Throw<ShouldAssertException>(() =>
            new HashSet<string> { "1", "2" }.ShouldBe(new GuardedSequence().Select(number => number.ToString()), Case.Insensitive));
    }

    [Fact]
    public void ASetIsNotASequenceThatNeverEndsWithinATolerance()
    {
        Should.Throw<ShouldAssertException>(() =>
            new HashSet<double> { 1, 2, 3 }.ShouldBe(new GuardedSequence().Select(number => (double)number), 0.1));
        Should.Throw<ShouldAssertException>(() =>
            new GuardedSequence().Select(number => (double)number).ShouldBe(new HashSet<double> { 1, 2, 3 }, 0.1));
        Should.Throw<ShouldAssertException>(() =>
            new HashSet<float> { 1, 2, 3 }.ShouldBe(new GuardedSequence().Select(number => (float)number), 0.1));
        Should.Throw<ShouldAssertException>(() =>
            new HashSet<decimal> { 1, 2, 3 }.ShouldBe(new GuardedSequence().Select(number => (decimal)number), 0.1m));
    }

    // A sequence is kept for the message only as far as the message shows it. What a longer one lacks can't be
    // told from that, so the message doesn't say.
    [Fact]
    public void ASequenceLongerThanAMessageShowsHasNoDifferenceSection()
    {
        using var scope = MaxEnumerableItemsScope.Of(3);

        var message = Should.Throw<ShouldAssertException>(() =>
            Enumerable.Range(0, 5).Where(_ => true).ShouldBe(new HashSet<int> { 1, 2, 3, 4, 5 })).Message;

        message.ShouldNotContain("difference");
    }

    [Fact]
    public void ACaseOptionComparesTheStringsOfSetsInAnyOrder()
    {
        new HashSet<string> { "b", "a" }.ShouldBe(new HashSet<string> { "A", "B" }, Case.Insensitive);
        new HashSet<string> { "b", "a" }.ShouldBe(new HashSet<string> { "a", "b" }, Case.Sensitive);
        Should.Throw<ShouldAssertException>(() =>
            new HashSet<string> { "b", "a" }.ShouldBe(new HashSet<string> { "A", "B" }, Case.Sensitive));
    }

    [Fact]
    public void SetOfStringsWithACaseOptionShouldFail() =>
        // Only what differs once case is ignored is reported.
        Verify.ShouldFail(() =>
            new HashSet<string> { "b", "A" }.ShouldBe(new HashSet<string> { "a", "c" }, Case.Insensitive, "Some additional context"));

    [Fact]
    public void SetOfStringsWithACaseOptionIgnoringOrderShouldFail() =>
        Verify.ShouldFail(() =>
            new HashSet<string> { "b", "A" }.ShouldBe(new HashSet<string> { "a", "c" }, Case.Insensitive, ignoreOrder: true, "Some additional context"));

    [Fact]
    public void AToleranceComparesTheNumbersOfSetsInAnyOrder()
    {
        new HashSet<double> { 2.0, 1.0 }.ShouldBe(new HashSet<double> { 1.001, 2.001 }, 0.01);
        new HashSet<float> { 2f, 1f }.ShouldBe(new HashSet<float> { 1.001f, 2.001f }, 0.01);
        new HashSet<decimal> { 2m, 1m }.ShouldBe(new HashSet<decimal> { 1.001m, 2.001m }, 0.01m);
    }

    [Fact]
    public void ATolerancePairsTheNumbersOfSetsSoThatEveryOneMatches() =>
        // 1.01 is within the tolerance of both 1.0 and 1.02, so pairing it with 1.02 would leave 1.03 unmatched.
        new HashSet<double> { 1.03, 1.01 }.ShouldBe(new HashSet<double> { 1.02, 1.0 }, 0.015);

    [Fact]
    public void ATolerancePairsSetsOneForOne()
    {
        Should.Throw<ShouldAssertException>(() =>
            new HashSet<double> { 1.0, 2.0 }.ShouldBe(new HashSet<double> { 1.0, 3.0 }, 0.01));
        Should.Throw<ShouldAssertException>(() =>
            new HashSet<double> { 1.0, 1.005 }.ShouldBe(new HashSet<double> { 1.0 }, 0.01));
    }

    [Fact]
    public void SetWithAToleranceShouldFail() =>
        Verify.ShouldFail(() =>
            new HashSet<double> { 4.0, 2.0, 1.0 }.ShouldBe(new HashSet<double> { 1.001, 2.001, 3.001 }, 0.01, "Some additional context"));

    [Fact]
    public void DictionaryValueDiffersShouldFail() =>
        Verify.ShouldFail(() =>
            new Dictionary<string, int> { ["a"] = 1, ["b"] = 2, ["c"] = 3 }
                .ShouldBe(new Dictionary<string, int> { ["c"] = 3, ["b"] = 5, ["a"] = 1 }, "Some additional context"));

    [Fact]
    public void DictionaryKeysDifferShouldFail() =>
        Verify.ShouldFail(() =>
            new Dictionary<string, int> { ["a"] = 1, ["b"] = 2, ["d"] = 4 }
                .ShouldBe(new Dictionary<string, int> { ["c"] = 3, ["b"] = 5, ["a"] = 1 }, "Some additional context"));

    [Fact]
    public void ObjectTypedDictionaryValueDiffersShouldFail() =>
        Verify.ShouldFail(() =>
            ((object)new Dictionary<string, int> { ["a"] = 1, ["b"] = 2 })
                .ShouldBe(new Dictionary<string, int> { ["b"] = 3, ["a"] = 1 }, "Some additional context"));

    [Fact]
    public void SetShouldFail() =>
        Verify.ShouldFail(() =>
            new HashSet<int> { 4, 2, 1 }.ShouldBe(new HashSet<int> { 1, 2, 3 }, "Some additional context"));

    [Fact]
    public void DictionaryValuesThatReadTheSameShouldFail() =>
        // The message says why two values that read the same are unequal, and for which key.
        Verify.ShouldFail(() =>
            new Dictionary<string, UncomparableClass> { ["a"] = new("x") }
                .ShouldBe(new Dictionary<string, UncomparableClass> { ["a"] = new("x") }, "Some additional context"));

    /// <summary>
    /// A dictionary that is only an <see cref="IReadOnlyDictionary{TKey, TValue}"/>: neither
    /// <see cref="IDictionary{TKey, TValue}"/> nor <see cref="System.Collections.IDictionary"/>.
    /// </summary>
    private sealed class Within(double tolerance) : IEqualityComparer<double>
    {
        public bool Equals(double x, double y) => Math.Abs(x - y) <= tolerance;

        public int GetHashCode(double obj) => 0;
    }

    private sealed class OnlyReadOnlyDictionary(params (string Key, int Value)[] entries) : IReadOnlyDictionary<string, int>
    {
        private readonly Dictionary<string, int> _entries = entries.ToDictionary(entry => entry.Key, entry => entry.Value);

        public int this[string key] => _entries[key];

        public IEnumerable<string> Keys => entries.Select(entry => entry.Key);

        public IEnumerable<int> Values => entries.Select(entry => entry.Value);

        public int Count => entries.Length;

        public bool ContainsKey(string key) => _entries.ContainsKey(key);

        public bool TryGetValue(string key, out int value) => _entries.TryGetValue(key, out value);

        // In the order the entries were given, so two of them can hold the same entries in different orders.
        public IEnumerator<KeyValuePair<string, int>> GetEnumerator() =>
            entries.Select(entry => new KeyValuePair<string, int>(entry.Key, entry.Value)).GetEnumerator();

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
