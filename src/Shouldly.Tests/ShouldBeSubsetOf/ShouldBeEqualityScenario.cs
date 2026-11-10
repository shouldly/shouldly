namespace Shouldly.Tests.ShouldBeSubsetOf;

/// <summary>
/// Without a comparer, <c>ShouldBeSubsetOf</c> matches elements the way <c>ShouldBe</c> compares values, rather than
/// through <c>GetHashCode</c>. See issue #1404.
/// </summary>
[Collection(GlobalConfigCollection.Name)]
public class ShouldBeEqualityScenario
{
    [Fact]
    public void EquatableElements() =>
        new List<EquatableOnly> { new(1), new(1) }.ShouldBeSubsetOf([new EquatableOnly(2), new EquatableOnly(1)]);

    [Fact]
    public void ComparableElements() =>
        new[] { new ComparableOnly(1) }.ShouldBeSubsetOf([new ComparableOnly(1)]);

    [Fact]
    public void CrossTypeNumerics() =>
        new object[] { 1, 2L }.ShouldBeSubsetOf([2, 1L, 3]);

    [Fact]
    public void CollectionElementsAreComparedStructurally() =>
        new List<int[]> { new[] { 1 } }.ShouldBeSubsetOf([new[] { 2 }, new[] { 1 }]);

    [Fact]
    public void FloatingPointHonoursTheDefaultTolerance()
    {
        Should.Throw<ShouldAssertException>(() => new[] { 0.1 + 0.2 }.ShouldBeSubsetOf([0.3]));

        ShouldlyConfiguration.DefaultFloatingPointTolerance = 1e-9;
        try
        {
            new[] { 0.1 + 0.2 }.ShouldBeSubsetOf([0.3]);
        }
        finally
        {
            ShouldlyConfiguration.DefaultFloatingPointTolerance = 0.0;
        }
    }

    [Fact]
    public void CallsEqualsOnExpected() =>
        Should.Throw<ShouldAssertException>(() => new Receiver[] { new Lenient() }.ShouldBeSubsetOf([new Strict()]));

    [Fact]
    public void NullElements()
    {
        new string?[] { null, "a" }.ShouldBeSubsetOf(["a", null]);
        Should.Throw<ShouldAssertException>(() => new string?[] { null }.ShouldBeSubsetOf(["a"]));
    }

    [Fact]
    public void EquatableElementScenarioShouldFail()
    {
        // Only the unmatched elements are reported, each once.
        Verify.ShouldFail(() =>
            new List<EquatableOnly> { new(1), new(2), new(3), new(2) }
                .ShouldBeSubsetOf([new EquatableOnly(1)], "Some additional context"));
    }

    [Fact]
    public void CrossTypeNumericScenarioShouldFail()
    {
        Verify.ShouldFail(() =>
            new object[] { 1, 2L, 3 }.ShouldBeSubsetOf([1L, 2], "Some additional context"));
    }
}
