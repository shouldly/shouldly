namespace Shouldly.Tests.ShouldContain;

/// <summary>
/// Without a comparer, <c>ShouldContain</c> compares elements the way <c>ShouldBe</c> compares values, whatever the
/// collection type. See issue #1404.
/// </summary>
[Collection(GlobalConfigCollection.Name)]
public class ShouldBeEqualityScenario
{
    [Fact]
    public void EquatableElements()
    {
        new[] { new EquatableOnly(1) }.ShouldContain(new EquatableOnly(1));
        new List<EquatableOnly> { new(1) }.ShouldContain(new EquatableOnly(1));
    }

    [Fact]
    public void ComparableElements() =>
        new[] { new ComparableOnly(1) }.ShouldContain(new ComparableOnly(1));

    [Fact]
    public void CrossTypeNumerics() =>
        new object[] { 1 }.ShouldContain(1L);

    [Fact]
    public void CollectionElementsAreComparedStructurally()
    {
        new List<int[]> { new[] { 1 } }.ShouldContain([1]);
        new List<int[]> { new[] { 1, 2 } }.ShouldContain(new List<int> { 1, 2 }.ToArray());
    }

    [Fact]
    public void FloatingPointHonoursTheDefaultTolerance()
    {
        Should.Throw<ShouldAssertException>(() => new[] { 0.1 + 0.2 }.ShouldContain(0.3));

        ShouldlyConfiguration.DefaultFloatingPointTolerance = 1e-9;
        try
        {
            new[] { 0.1 + 0.2 }.ShouldContain(0.3);
        }
        finally
        {
            ShouldlyConfiguration.DefaultFloatingPointTolerance = 0.0;
        }
    }

    [Fact]
    public void CallsEqualsOnExpected() =>
        Should.Throw<ShouldAssertException>(() => new Receiver[] { new Lenient() }.ShouldContain(new Strict()));

    [Fact]
    public void NullElements()
    {
        new string?[] { "a", null }.ShouldContain((string?)null);
        Should.Throw<ShouldAssertException>(() => new string?[] { "a" }.ShouldContain((string?)null));
    }

    [Fact]
    public void SetsKeepTheirOwnComparer()
    {
        // Membership of a set is defined by its comparer, as ShouldContainKey defers to a dictionary's.
        var set = new HashSet<string>(StringComparer.OrdinalIgnoreCase) { "a" };

        set.ShouldContain("A");
    }

    [Fact]
    public void DictionaryKeysKeepTheDictionarysComparer()
    {
        new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase) { ["a"] = 1 }.Keys.ShouldContain("A");
        new SortedDictionary<string, int>(StringComparer.OrdinalIgnoreCase) { ["a"] = 1 }.Keys.ShouldContain("A");
        new SortedList<string, int>(StringComparer.OrdinalIgnoreCase) { ["a"] = 1 }.Keys.ShouldContain("A");
        new System.Collections.ObjectModel.ReadOnlyDictionary<string, int>(
            new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase) { ["a"] = 1 }).Keys.ShouldContain("A");
    }

    [Fact]
    public void EquatableElementScenarioShouldFail()
    {
        Verify.ShouldFail(() =>
            new[] { new EquatableOnly(1), new EquatableOnly(2) }.ShouldContain(new EquatableOnly(3), "Some additional context"));
    }
}
