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
    public void ValuesOfCompareAsObjectTypesAreComparedWithTheirEquals()
    {
        // Strange is registered in ShouldlyConfiguration.CompareAsObjectTypes and enumerates as empty, so only its
        // Equals tells two apart, as it does for ShouldContainKeyAndValue and dictionary[key].ShouldBe(value).
        var actual = new Dictionary<string, Strange> { ["a"] = "x" };

        actual.ShouldBe(new Dictionary<string, Strange> { ["a"] = "x" });
        actual.ShouldNotBe(new Dictionary<string, Strange> { ["a"] = "y" });
        Should.Throw<ShouldAssertException>(() => actual.ShouldContainKeyAndValue("a", "y"));
    }

    [Fact]
    public void KeysOfCompareAsObjectTypesAreComparedWithTheirEquals() =>
        new Dictionary<Strange, int> { ["x"] = 1 }.ShouldNotBe(new Dictionary<Strange, int> { ["y"] = 1 });

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
