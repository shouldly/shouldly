namespace Shouldly.Tests.ShouldNotContain;

/// <summary>
/// Without a comparer, <c>ShouldNotContain</c> compares elements the way <c>ShouldBe</c> compares values, whatever
/// the collection type. See issue #1404.
/// </summary>
[Collection(GlobalConfigCollection.Name)]
public class ShouldBeEqualityScenario
{
    [Fact]
    public void EquatableElements()
    {
        Should.Throw<ShouldAssertException>(() => new[] { new EquatableOnly(1) }.ShouldNotContain(new EquatableOnly(1)));
        new[] { new EquatableOnly(1) }.ShouldNotContain(new EquatableOnly(2));
    }

    [Fact]
    public void ComparableElements() =>
        Should.Throw<ShouldAssertException>(() => new[] { new ComparableOnly(1) }.ShouldNotContain(new ComparableOnly(1)));

    [Fact]
    public void CrossTypeNumerics() =>
        Should.Throw<ShouldAssertException>(() => new object[] { 1 }.ShouldNotContain(1L));

    [Fact]
    public void CollectionElementsAreComparedStructurally()
    {
        Should.Throw<ShouldAssertException>(() => new List<int[]> { new[] { 1 } }.ShouldNotContain([1]));
        new List<int[]> { new[] { 1 } }.ShouldNotContain([2]);
    }

    [Fact]
    public void FloatingPointHonoursTheDefaultTolerance()
    {
        new[] { 0.1 + 0.2 }.ShouldNotContain(0.3);

        ShouldlyConfiguration.DefaultFloatingPointTolerance = 1e-9;
        try
        {
            Should.Throw<ShouldAssertException>(() => new[] { 0.1 + 0.2 }.ShouldNotContain(0.3));
        }
        finally
        {
            ShouldlyConfiguration.DefaultFloatingPointTolerance = 0.0;
        }
    }

    [Fact]
    public void CallsEqualsOnExpected() =>
        new Receiver[] { new Lenient() }.ShouldNotContain(new Strict());

    [Fact]
    public void SetsKeepTheirOwnComparer()
    {
        var set = new HashSet<string>(StringComparer.OrdinalIgnoreCase) { "a" };

        Should.Throw<ShouldAssertException>(() => set.ShouldNotContain("A"));
    }

    [Fact]
    public void DictionaryKeysKeepTheDictionarysComparer()
    {
        var dictionary = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase) { ["a"] = 1 };

        Should.Throw<ShouldAssertException>(() => dictionary.Keys.ShouldNotContain("A"));
    }

    [Fact]
    public void EquatableElementScenarioShouldFail()
    {
        Verify.ShouldFail(() =>
            new[] { new EquatableOnly(1), new EquatableOnly(2) }.ShouldNotContain(new EquatableOnly(2), "Some additional context"));
    }
}
