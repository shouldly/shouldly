namespace Shouldly.Tests.ShouldBe.EnumerableType;

/// <summary>
/// A <see cref="KeyValuePair{TKey, TValue}"/> is compared as its key and its value, each the way <c>ShouldBe</c>
/// compares a single value, so <c>dictionary.ShouldBe(other)</c> agrees with <c>dictionary[key].ShouldBe(value)</c>
/// and with <c>ShouldContainKeyAndValue</c>. See issue #1408.
/// </summary>
public class KeyValuePairScenario
{
    [Fact]
    public void DictionariesWithCollectionValuesAreComparedStructurally()
    {
        var actual = new Dictionary<string, int[]> { ["a"] = [1], ["b"] = [2, 3] };

        // The exact shape from the issue: these already agreed before #1408.
        actual["a"].ShouldBe([1]);
        actual.ShouldContainKeyAndValue("a", [1]);
        actual.ShouldBeEquivalentTo(new Dictionary<string, int[]> { ["a"] = [1], ["b"] = [2, 3] });

        actual.ShouldBe(new Dictionary<string, int[]> { ["a"] = [1], ["b"] = [2, 3] });
    }

    [Fact]
    public void ShouldNotBeFailsForStructurallyEqualDictionaries() =>
        Should.Throw<ShouldAssertException>(() =>
            new Dictionary<string, int[]> { ["a"] = [1] }.ShouldNotBe(new Dictionary<string, int[]> { ["a"] = [1] }));

    [Fact]
    public void ShouldNotBePassesWhenAValueDiffers() =>
        new Dictionary<string, int[]> { ["a"] = [1] }.ShouldNotBe(new Dictionary<string, int[]> { ["a"] = [2] });

    [Fact]
    public void ShouldNotBePassesWhenAKeyDiffers() =>
        new Dictionary<string, int[]> { ["a"] = [1] }.ShouldNotBe(new Dictionary<string, int[]> { ["b"] = [1] });

    [Fact]
    public void SequencesOfPairsWithCollectionValuesAreComparedStructurally()
    {
        IEnumerable<KeyValuePair<string, int[]>> actual = [new("a", [1]), new("b", [2])];

        actual.ShouldBe([new("a", [1]), new("b", [2])]);
    }

    [Fact]
    public void PairsAreComparedStructurally()
    {
        new KeyValuePair<string, int[]>("a", [1]).ShouldBe(new("a", [1]));
        new KeyValuePair<string, int[]>("a", [1]).ShouldNotBe(new("a", [2]));
    }

    [Fact]
    public void ObjectTypedPairsAreComparedStructurally()
    {
        ((object)new KeyValuePair<string, int[]>("a", [1])).ShouldBe(new KeyValuePair<string, int[]>("a", [1]));

        // Components compare as ShouldBe<object> compares them, so numerics compare across types, as they do in a
        // List<int> compared with a long[].
        ((object)new KeyValuePair<string, int>("a", 1)).ShouldBe(new KeyValuePair<string, long>("a", 1L));
    }

    [Fact]
    public void ObjectValuesCompareNumericsAcrossTypes() =>
        new Dictionary<string, object> { ["a"] = 1 }.ShouldBe(new Dictionary<string, object> { ["a"] = 1L });

    [Fact]
    public void KeysAreComparedWithTheSameComparer() =>
        new Dictionary<object, string> { [1] = "a" }.ShouldBe(new Dictionary<object, string> { [1L] = "a" });

    [Fact]
    public void NullValuesAreCompared()
    {
        new Dictionary<string, int[]?> { ["a"] = null }.ShouldBe(new Dictionary<string, int[]?> { ["a"] = null });
        new Dictionary<string, int[]?> { ["a"] = null }.ShouldNotBe(new Dictionary<string, int[]?> { ["a"] = [1] });
        new Dictionary<string, int[]?> { ["a"] = [1] }.ShouldNotBe(new Dictionary<string, int[]?> { ["a"] = null });
    }

    [Fact]
    public void ValuesWithAnEqualityRuleAreComparedByIt()
    {
        // Strange has an equality rule (its own Equals) and enumerates as empty, so only the rule tells two apart, as it does for ShouldContainKeyAndValue and dictionary[key].ShouldBe(value).
        var actual = new Dictionary<string, Strange> { ["a"] = "x" };

        actual.ShouldBe(new Dictionary<string, Strange> { ["a"] = "x" });
        actual.ShouldNotBe(new Dictionary<string, Strange> { ["a"] = "y" });
        Should.Throw<ShouldAssertException>(() => actual.ShouldContainKeyAndValue("a", "y"));
    }

    [Fact]
    public void KeysWithAnEqualityRuleAreComparedByIt() =>
        new Dictionary<Strange, int> { ["x"] = 1 }.ShouldNotBe(new Dictionary<Strange, int> { ["y"] = 1 });

    [Fact]
    public void ValuesUseTheirDeclaredTypesComparableInterface()
    {
        // Only IComparable<OnlyGenericComparable> says these are equal, as it does for the value on its own.
        var actual = new Dictionary<string, OnlyGenericComparable> { ["a"] = new(1) };
        var expected = new Dictionary<string, OnlyGenericComparable> { ["a"] = new(1) };

        actual["a"].ShouldBe(expected["a"]);
        actual.ShouldContainKeyAndValue("a", expected["a"]);
        actual.ShouldBe(expected);
    }

    [Fact]
    public void ValuesUseTheirDeclaredTypesEquatableBeforeIComparable()
    {
        // IEquatable<EquatableBeforeComparable> says unequal and is consulted before the non-generic IComparable,
        // as it is for the value on its own.
        var actual = new Dictionary<string, EquatableBeforeComparable> { ["a"] = new() };
        var expected = new Dictionary<string, EquatableBeforeComparable> { ["a"] = new() };

        actual["a"].ShouldNotBe(expected["a"]);
        Should.Throw<ShouldAssertException>(() => actual.ShouldContainKeyAndValue("a", expected["a"]));
        actual.ShouldNotBe(expected);
    }

    [Fact]
    public void ValuesUseTheirRuntimeEquatableBeforeTheirDeclaredTypesComparableInterface()
    {
        // The runtime type's IEquatable<RuntimeEquatable> says unequal and is consulted before IComparable<ComparableBase>,
        // as it is for the value on its own (#1428).
        var actual = new Dictionary<string, ComparableBase> { ["a"] = new RuntimeEquatable() };
        var expected = new Dictionary<string, ComparableBase> { ["a"] = new RuntimeEquatable() };

        actual["a"].ShouldNotBe(expected["a"]);
        Should.Throw<ShouldAssertException>(() => actual.ShouldContainKeyAndValue("a", expected["a"]));
        actual.ShouldNotBe(expected);
    }

    [Fact]
    public void KeysUseTheirDeclaredTypesComparableInterface() =>
        new Dictionary<OnlyGenericComparable, int> { [new(1)] = 1 }
            .ShouldBe(new Dictionary<OnlyGenericComparable, int> { [new(1)] = 1 });

    [Fact]
    public void NestedDictionariesAreComparedStructurally() =>
        new Dictionary<string, Dictionary<string, int[]>> { ["a"] = new() { ["b"] = [1] } }
            .ShouldBe(new Dictionary<string, Dictionary<string, int[]>> { ["a"] = new() { ["b"] = [1] } });

    [Fact]
    public void ObjectTypedDictionariesAreComparedStructurally() =>
        ((object)new Dictionary<string, int[]> { ["a"] = [1] }).ShouldBe(new Dictionary<string, int[]> { ["a"] = [1] });

    [Fact]
    public void IgnoringOrderDictionariesWithCollectionValuesAreComparedStructurally() =>
        new Dictionary<string, int[]> { ["a"] = [1], ["b"] = [2] }
            .ShouldBe(new Dictionary<string, int[]> { ["b"] = [2], ["a"] = [1] }, ignoreOrder: true);

    [Fact]
    public void ASequenceOfPairsStillEqualsADictionaryWithTheSamePairs()
    {
        // Issue #882: the container type isn't part of ShouldBe's comparison.
        var pairs = new List<KeyValuePair<string, int[]>> { new("a", [1]) };

        pairs.ShouldBe(new Dictionary<string, int[]> { ["a"] = [1] });
    }

    [Fact]
    public void ACustomComparerStillDecides() =>
        Should.Throw<ShouldAssertException>(() =>
            new Dictionary<string, int[]> { ["a"] = [1] }.ShouldBe(
                new Dictionary<string, int[]> { ["a"] = [1] },
                new ReferenceComparer(),
                ignoreOrder: false));

    [Fact]
    public void DictionaryWithCollectionValuesScenarioShouldFail()
    {
        // Only the entry whose value differs is highlighted.
        Verify.ShouldFail(() =>
            new Dictionary<string, int[]> { ["a"] = [1], ["b"] = [2] }
                .ShouldBe(new Dictionary<string, int[]> { ["a"] = [1], ["b"] = [3] }, "Some additional context"));
    }

    [Fact]
    public void DictionaryWithCollectionValuesIgnoringOrderScenarioShouldFail()
    {
        // Only the entry whose value differs is reported as missing.
        Verify.ShouldFail(() =>
            new Dictionary<string, int[]> { ["a"] = [1], ["b"] = [2] }
                .ShouldBe(new Dictionary<string, int[]> { ["b"] = [3], ["a"] = [1] }, ignoreOrder: true, "Some additional context"));
    }

    /// <summary>
    /// Equal to another instance only through <see cref="IComparable{T}"/>: no <see cref="IComparable"/>,
    /// <see cref="IEquatable{T}"/> or <see cref="object.Equals(object)"/> override.
    /// </summary>
    public sealed class OnlyGenericComparable(int value) : IComparable<OnlyGenericComparable>
    {
        private int Value { get; } = value;

        public int CompareTo(OnlyGenericComparable? other) => other is null ? 1 : Value.CompareTo(other.Value);
    }

    /// <summary>
    /// <see cref="IEquatable{T}"/> says unequal while the non-generic <see cref="IComparable"/> says equal.
    /// </summary>
#pragma warning disable CA1067 // Deliberately disagrees with object.Equals to show which interface is consulted.
    public sealed class EquatableBeforeComparable : IEquatable<EquatableBeforeComparable>, IComparable
    {
        public bool Equals(EquatableBeforeComparable? other) => false;

        public int CompareTo(object? obj) => 0;
    }
#pragma warning restore CA1067

    /// <summary>
    /// Equal to any other instance through <see cref="IComparable{T}"/>, with no equality of its own.
    /// </summary>
    public class ComparableBase : IComparable<ComparableBase>
    {
        public int CompareTo(ComparableBase? other) => 0;
    }

    /// <summary>
    /// <see cref="IEquatable{T}"/> on the runtime type says unequal, while its declared base's
    /// <see cref="IComparable{T}"/> says equal.
    /// </summary>
#pragma warning disable CA1067 // Deliberately disagrees with object.Equals to show which interface is consulted.
    public sealed class RuntimeEquatable : ComparableBase, IEquatable<RuntimeEquatable>
    {
        public bool Equals(RuntimeEquatable? other) => false;
    }
#pragma warning restore CA1067

    /// <summary>
    /// Counts two pairs equal only when their values are the same array instance, to show a caller-supplied
    /// comparer is used as-is rather than having the values walked element-wise.
    /// </summary>
    private sealed class ReferenceComparer : IEqualityComparer<KeyValuePair<string, int[]>>
    {
        public bool Equals(KeyValuePair<string, int[]> x, KeyValuePair<string, int[]> y) =>
            x.Key == y.Key && ReferenceEquals(x.Value, y.Value);

        public int GetHashCode(KeyValuePair<string, int[]> obj) => obj.Key.GetHashCode();
    }
}
