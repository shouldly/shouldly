namespace Shouldly.Tests.Dictionaries;

using static DictionaryTestData;

/// <summary>
/// The dictionary value assertions compare the value the same way <c>ShouldBe</c> does, rather than
/// with <see cref="object.Equals(object, object)"/>. See issue #1325.
/// </summary>
// FloatingPointValuesHonourTheDefaultTolerance mutates the global ShouldlyConfiguration.DefaultFloatingPointTolerance,
// so the class runs in the non-parallel Global configuration collection to keep that change from leaking into
// floating-point assertions in other test classes.
[Collection(GlobalConfigCollection.Name)]
public class DictionaryValueEquality
{
    [Fact]
    public void CollectionValuesAreComparedStructurally()
    {
        var dictionary = new Dictionary<string, int[]> { ["key"] = [1, 2, 3] };

        // A different array instance with identical contents.
        dictionary.ShouldContainKeyAndValue("key", [1, 2, 3]);
        Should.Throw<ShouldAssertException>(() => dictionary.ShouldNotContainValueForKey("key", [1, 2, 3]));
    }

    [Fact]
    public void CollectionValuesWithDifferentContentsAreNotEqual()
    {
        var dictionary = new Dictionary<string, int[]> { ["key"] = [1, 2, 3] };

        dictionary.ShouldNotContainValueForKey("key", [1, 2, 4]);
        Should.Throw<ShouldAssertException>(() => dictionary.ShouldContainKeyAndValue("key", [1, 2, 4]));
    }

    [Fact]
    public void CollectionValuesOfDifferentContainerTypesAreEqual()
    {
        var dictionary = new Dictionary<string, IEnumerable<int>> { ["key"] = new[] { 1, 2, 3 } };

        dictionary.ShouldContainKeyAndValue("key", new List<int> { 1, 2, 3 });
        Should.Throw<ShouldAssertException>(() => dictionary.ShouldNotContainValueForKey("key", new List<int> { 1, 2, 3 }));
    }

    [Fact]
    public void NestedCollectionValuesAreComparedStructurally()
    {
        var dictionary = new Dictionary<string, int[][]> { ["key"] = [[1, 2], [3]] };

        dictionary.ShouldContainKeyAndValue("key", [[1, 2], [3]]);
        dictionary.ShouldNotContainValueForKey("key", [[1, 2], [4]]);
    }

    [Fact]
    public void FloatingPointValuesHonourTheDefaultTolerance()
    {
        var dictionary = new Dictionary<string, double> { ["key"] = 1.0 };

        dictionary.ShouldNotContainValueForKey("key", 1.05);

        ShouldlyConfiguration.DefaultFloatingPointTolerance = 0.1;
        try
        {
            dictionary.ShouldContainKeyAndValue("key", 1.05);
        }
        finally
        {
            ShouldlyConfiguration.DefaultFloatingPointTolerance = 0.0;
        }
    }

    [Fact]
    public void ReferenceTypesWithoutAnEqualsOverrideAreStillComparedByReference()
    {
        // Nothing about this change makes the comparison member-wise — that is ShouldBeEquivalentTo's job.
        ClassDictionary().ShouldContainKeyAndValue(ThingKey, ThingValue);
        ClassDictionary().ShouldNotContainValueForKey(ThingKey, new());
    }

    /// <summary>
    /// <see cref="Strange"/> emulates JToken: implicitly convertible from a string, <see cref="IEnumerable{T}"/>,
    /// and registered in <see cref="ShouldlyConfiguration.CompareAsObjectTypes"/> so that it is compared with its
    /// own Equals rather than being walked as an (empty) enumerable. The dictionary value assertions honour that
    /// registration for the same reason ShouldBe does.
    /// </summary>
    [Fact]
    public void CompareAsObjectTypesIsHonoured()
    {
        var dictionary = new Dictionary<string, Strange> { ["key"] = new() };

        dictionary.ShouldNotContainValueForKey("key", "string");
        Should.Throw<ShouldAssertException>(() => dictionary.ShouldContainKeyAndValue("key", "string"));

        var populated = new Dictionary<string, Strange> { ["key"] = "string" };
        populated.ShouldContainKeyAndValue("key", "string");
    }

    [Fact]
    public void StringValuesAreComparedOrdinally()
    {
        StringDictionary().ShouldContainKeyAndValue("Foo", "Bar");
        StringDictionary().ShouldNotContainValueForKey("Foo", "bar");
    }

    /// <summary>
    /// A limitation shared with <c>ShouldBe</c>, pinned here so the divergence is deliberate rather than
    /// discovered: the elements of a collection value are compared without their static element type, so an
    /// element implementing <see cref="IEquatable{T}"/> without an <see cref="object.Equals(object)"/> override
    /// (CA1067) falls back to reference equality. <c>ShouldBe</c> on the value itself has the element type
    /// statically and honours it; it loses the same way one level deeper, on nested collections.
    /// </summary>
    [Fact]
    public void ElementsOfACollectionValueDoNotHonourIEquatableWithoutAnEqualsOverride()
    {
        var element = new EquatableWithoutEqualsOverride(1);
        var dictionary = new Dictionary<string, EquatableWithoutEqualsOverride[]> { ["key"] = [element] };
        EquatableWithoutEqualsOverride[] equalButNotIdentical = [new(1)];

        dictionary.ShouldNotContainValueForKey("key", equalButNotIdentical);

        // The same elements do compare equal where the element type is statically known...
        dictionary["key"].ShouldBe(equalButNotIdentical);

        // ...and where the value is the equatable itself rather than an element of one.
        new Dictionary<string, EquatableWithoutEqualsOverride> { ["key"] = element }
            .ShouldContainKeyAndValue("key", new(1));
    }

    [Fact]
    public void MissingKeyStillFailsBothAssertions()
    {
        var dictionary = new Dictionary<string, int[]> { ["key"] = [1, 2, 3] };

        Should.Throw<ShouldAssertException>(() => dictionary.ShouldContainKeyAndValue("missing", [1, 2, 3]));
        Should.Throw<ShouldAssertException>(() => dictionary.ShouldNotContainValueForKey("missing", [1, 2, 3]));
    }
}

#pragma warning disable CA1067 // Deliberately no object.Equals override — that shape is what this file pins.
/// <summary>
/// Implements <see cref="IEquatable{T}"/> without overriding <see cref="object.Equals(object)"/>, so a typed
/// comparer and the object comparer disagree about two instances carrying the same value.
/// </summary>
file class EquatableWithoutEqualsOverride(int value) : IEquatable<EquatableWithoutEqualsOverride>
{
    private int Value { get; } = value;

    public bool Equals(EquatableWithoutEqualsOverride? other) => other is not null && other.Value == Value;
}
#pragma warning restore CA1067
