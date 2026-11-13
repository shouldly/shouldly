namespace Shouldly.Tests.ShouldBe;

/// <summary>
/// A tuple (<see cref="ValueTuple{T1, T2}"/>, <see cref="Tuple{T1, T2}"/> and their other arities) is compared item by
/// item, each the way <c>ShouldBe</c> compares a single value, as a <see cref="KeyValuePair{TKey, TValue}"/> is, rather
/// than with the tuple's own <c>Equals</c>, which compares a collection-typed item by reference. See issue #1429.
/// </summary>
// FloatingPointItemsHonourTheDefaultTolerance mutates the global ShouldlyConfiguration.DefaultFloatingPointTolerance, so
// the class runs in the non-parallel Global configuration collection to keep that change from leaking into other
// test classes.
[Collection(GlobalConfigCollection.Name)]
public class TupleScenario
{
    [Fact]
    public void ValueTuplesWithCollectionItemsAreComparedStructurally()
    {
        (1, new[] { 1 }).ShouldBe((1, new[] { 1 }));
        (1, new[] { 1 }).ShouldNotBe((1, new[] { 2 }));
    }

    [Fact]
    public void TuplesWithCollectionItemsAreComparedStructurally()
    {
        Tuple.Create(1, new[] { 1 }).ShouldBe(Tuple.Create(1, new[] { 1 }));
        Tuple.Create(1, new[] { 1 }).ShouldNotBe(Tuple.Create(1, new[] { 2 }));
    }

    [Fact]
    public void ShouldNotBeFailsForStructurallyEqualTuples() =>
        Should.Throw<ShouldAssertException>(() =>
            (1, new List<string> { "a" }).ShouldNotBe((1, new List<string> { "a" })));

    [Fact]
    public void NamedTupleItemsAreComparedStructurally() =>
        (Id: 1, Lines: new List<string> { "a" }).ShouldBe((Id: 1, Lines: new List<string> { "a" }));

    [Fact]
    public void ObjectTypedTuplesCompareNumericsAcrossTypes()
    {
        // Items compare as ShouldBe<object> compares them, as a KeyValuePair's key and value do.
        ((object)(1, 2)).ShouldBe((object)(1L, 2L));
        ((object)Tuple.Create(1, 2)).ShouldBe((object)Tuple.Create(1L, 2L));
        ((object)(1, 2)).ShouldNotBe((object)(1L, 3L));
    }

    [Fact]
    public void TuplesOfDifferentAritiesAreUnequal() =>
        ((object)(1, 2)).ShouldNotBe((object)(1, 2, 3));

    [Fact]
    public void AValueTupleIsNotATuple() =>
        ((object)(1, 2)).ShouldNotBe((object)Tuple.Create(1, 2));

    [Fact]
    public void ItemsPastTheSeventhAreComparedStructurally()
    {
        // The eighth item onwards lives in the tuple's Rest.
        (1, 2, 3, 4, 5, 6, 7, new[] { 8 }).ShouldBe((1, 2, 3, 4, 5, 6, 7, new[] { 8 }));
        (1, 2, 3, 4, 5, 6, 7, new[] { 8 }).ShouldNotBe((1, 2, 3, 4, 5, 6, 7, new[] { 9 }));
        Tuple.Create(1, 2, 3, 4, 5, 6, 7, new[] { 8 }).ShouldBe(Tuple.Create(1, 2, 3, 4, 5, 6, 7, new[] { 8 }));
        Tuple.Create(1, 2, 3, 4, 5, 6, 7, new[] { 8 }).ShouldNotBe(Tuple.Create(1, 2, 3, 4, 5, 6, 7, new[] { 9 }));
    }

    [Fact]
    public void ItemsPastTheFourteenthAreComparedStructurally()
    {
        // The fifteenth item onwards lives in the Rest of the tuple's Rest.
        (1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, new[] { 16 })
            .ShouldBe((1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, new[] { 16 }));
        (1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, new[] { 16 })
            .ShouldNotBe((1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, new[] { 17 }));
    }

    [Fact]
    public void TuplesOfDifferentAritiesPastTheSeventhAreUnequal()
    {
        // Every tuple of eight items or more has the one generic definition, and differs only in its Rest.
        ((object)(1, 2, 3, 4, 5, 6, 7, 8)).ShouldNotBe((object)(1, 2, 3, 4, 5, 6, 7, 8, 9));
        ((object)Tuple.Create(1, 2, 3, 4, 5, 6, 7, 8))
            .ShouldNotBe(new Tuple<int, int, int, int, int, int, int, Tuple<int, int>>(1, 2, 3, 4, 5, 6, 7, Tuple.Create(8, 9)));
    }

    [Fact]
    public void ADefaultTupleWhoseRestIsNotATupleIsCompared()
    {
        default(ValueTuple<int, int, int, int, int, int, int, int>)
            .ShouldBe(default(ValueTuple<int, int, int, int, int, int, int, int>));
    }

    [Fact]
    public void NestedTuplesAreComparedStructurally()
    {
        (1, (2, new[] { 3 })).ShouldBe((1, (2, new[] { 3 })));
        (1, (2, new[] { 3 })).ShouldNotBe((1, (2, new[] { 4 })));
    }

    [Fact]
    public void TuplesInsidePairsAndPairsInsideTuplesAreComparedStructurally()
    {
        new Dictionary<string, (int, int[])> { ["a"] = (1, [1]) }
            .ShouldBe(new Dictionary<string, (int, int[])> { ["a"] = (1, [1]) });
        (1, new KeyValuePair<string, int[]>("a", [1])).ShouldBe((1, new KeyValuePair<string, int[]>("a", [1])));
    }

    [Fact]
    public void SequencesOfTuplesAreComparedStructurally()
    {
        List<(int, int[])> actual = [(1, [1]), (2, [2])];

        actual.ShouldBe([(1, [1]), (2, [2])]);
        actual.ShouldBe([(2, [2]), (1, [1])], ignoreOrder: true);
        actual.ShouldContain((2, new[] { 2 }));
    }

    [Fact]
    public void NullItemsAreCompared()
    {
        (1, (int[]?)null).ShouldBe((1, (int[]?)null));
        (1, (int[]?)null).ShouldNotBe((1, new[] { 1 }));
        (1, new[] { 1 }).ShouldNotBe((1, (int[]?)null));
    }

    [Fact]
    public void ItemsWithAnEqualityRuleAreComparedByIt()
    {
        // Strange has an equality rule (its own Equals) and enumerates as empty, so only the rule tells two apart.
        (1, (Strange)"x").ShouldBe((1, (Strange)"x"));
        (1, (Strange)"x").ShouldNotBe((1, (Strange)"y"));
    }

    [Fact]
    public void ItemsUseTheirDeclaredTypesComparableInterface() =>
        // Only IComparable<OnlyGenericComparable> says these are equal, as it does for the item on its own.
        (1, new OnlyGenericComparable(1)).ShouldBe((1, new OnlyGenericComparable(1)));

    [Fact]
    public void ItemsAreComparedByTheirRuntimeTypesAsObjectElementsAre()
    {
        // The judge's IEquatable<Animal> says unequal and its IEquatable<Dog> says equal. Held as an object, the dog
        // is compared by the most derived of the two, and a tuple item declared as an Animal is compared the same way.
        var judge = new Judge();

        new List<object> { new Dog() }.ShouldBe([judge]);
        ((Animal)new Dog(), 1).ShouldBe((judge, 1));
        new KeyValuePair<string, Animal>("a", new Dog()).ShouldBe(new KeyValuePair<string, Animal>("a", judge));
    }

    [Fact]
    public void FloatingPointItemsHonourTheDefaultTolerance()
    {
        // As the item on its own does: a tuple's own Equals would compare it exactly.
        ShouldlyConfiguration.DefaultFloatingPointTolerance = 1e-9;
        try
        {
            (1, 0.1 + 0.2).ShouldBe((1, 0.3));
            Tuple.Create(1, 0.1 + 0.2).ShouldBe(Tuple.Create(1, 0.3));
            new List<(int, double)> { (1, 0.1 + 0.2) }.ShouldBe([(1, 0.3)]);
            (1, 0.1 + 0.2).ShouldNotBe((1, 0.31));
        }
        finally
        {
            ShouldlyConfiguration.DefaultFloatingPointTolerance = 0.0;
        }

        (1, 0.1 + 0.2).ShouldNotBe((1, 0.3));
    }

    [Fact]
    public void ACustomComparerStillDecides() =>
        Should.Throw<ShouldAssertException>(() =>
            (1, new[] { 1 }).ShouldBe((1, new[] { 1 }), new ReferenceComparer()));

    [Fact]
    public void RecordsStillCompareWithTheirOwnEquals() =>
        // A record's Equals is its own to define, so its collection member is still compared by reference: the
        // failure message points at ShouldBeEquivalentTo (#1432).
        new Order(1, ["a"]).ShouldNotBe(new Order(1, ["a"]));

    [Fact]
    public void TupleWithCollectionItemsScenarioShouldFail()
    {
        Verify.ShouldFail(() =>
            (1, new[] { 1, 2 }).ShouldBe((1, new[] { 1, 3 }), "Some additional context"));
    }

    [Fact]
    public void SequenceOfTuplesScenarioShouldFail()
    {
        // Only the tuple whose item differs is highlighted.
        List<(int, int[])> actual = [(1, [1]), (2, [2])];

        Verify.ShouldFail(() =>
            actual.ShouldBe([(1, [1]), (2, [3])], "Some additional context"));
    }

    private sealed record Order(int Id, List<string> Lines);

    /// <summary>
    /// Equal to another instance only through <see cref="IComparable{T}"/>: no <see cref="IComparable"/>,
    /// <see cref="IEquatable{T}"/> or <see cref="object.Equals(object)"/> override.
    /// </summary>
    private sealed class OnlyGenericComparable(int value) : IComparable<OnlyGenericComparable>
    {
        private int Value { get; } = value;

        public int CompareTo(OnlyGenericComparable? other) => other is null ? 1 : Value.CompareTo(other.Value);
    }

    private class Animal;

    private class Dog : Animal;

#pragma warning disable CA1067 // Deliberately disagrees with object.Equals to show which interface is consulted.
    private sealed class Judge : Dog, IEquatable<Animal>, IEquatable<Dog>
    {
        public bool Equals(Animal? other) => false;

        public bool Equals(Dog? other) => true;
    }
#pragma warning restore CA1067

    private sealed class ReferenceComparer : IEqualityComparer<(int, int[])>
    {
        public bool Equals((int, int[]) x, (int, int[]) y) => x.Item1 == y.Item1 && ReferenceEquals(x.Item2, y.Item2);

        public int GetHashCode((int, int[]) obj) => obj.Item1;
    }
}
