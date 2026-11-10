namespace Shouldly.Tests.ShouldBeUnique;

/// <summary>
/// Without a comparer, <c>ShouldBeUnique</c> finds duplicates the way <c>ShouldBe</c> compares values, rather than
/// through <c>GetHashCode</c>. See issue #1404.
/// </summary>
[Collection(GlobalConfigCollection.Name)]
public class ShouldBeEqualityScenario
{
    [Fact]
    public void EquatableElements()
    {
        Should.Throw<ShouldAssertException>(() => new List<EquatableOnly> { new(1), new(1) }.ShouldBeUnique());
        new List<EquatableOnly> { new(1), new(2) }.ShouldBeUnique();
    }

    [Fact]
    public void ComparableElements() =>
        Should.Throw<ShouldAssertException>(() => new[] { new ComparableOnly(1), new ComparableOnly(1) }.ShouldBeUnique());

    [Fact]
    public void CrossTypeNumerics() =>
        Should.Throw<ShouldAssertException>(() => new object[] { 1, 1L }.ShouldBeUnique());

    [Fact]
    public void CollectionElementsAreComparedStructurally()
    {
        Should.Throw<ShouldAssertException>(() => new List<int[]> { new[] { 1 }, new[] { 1 } }.ShouldBeUnique());
        new List<int[]> { new[] { 1 }, new[] { 2 } }.ShouldBeUnique();
    }

    [Fact]
    public void FloatingPointHonoursTheDefaultTolerance()
    {
        new[] { 0.1 + 0.2, 0.3 }.ShouldBeUnique();

        ShouldlyConfiguration.DefaultFloatingPointTolerance = 1e-9;
        try
        {
            Should.Throw<ShouldAssertException>(() => new[] { 0.1 + 0.2, 0.3 }.ShouldBeUnique());
        }
        finally
        {
            ShouldlyConfiguration.DefaultFloatingPointTolerance = 0.0;
        }
    }

    [Fact]
    public void LargeDatasetOfAnUnhashableTypeStaysTractable() =>
        Enumerable.Range(1, 2000).Select(i => new EquatableOnly(i)).ToList().ShouldBeUnique();

    [Fact]
    public void EquatableElementScenarioShouldFail()
    {
        // Each duplicated value is reported once.
        Verify.ShouldFail(() =>
            new List<EquatableOnly> { new(1), new(2), new(1), new(3), new(2), new(1) }
                .ShouldBeUnique("Some additional context"));
    }

    [Fact]
    public void CrossTypeNumericScenarioShouldFail()
    {
        Verify.ShouldFail(() =>
            new object[] { 1, 2, 1L }.ShouldBeUnique("Some additional context"));
    }
}
