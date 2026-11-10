namespace Shouldly.Tests.ShouldNotBeOneOf;

/// <summary>
/// Without a comparer, <c>ShouldNotBeOneOf</c> compares the value with each candidate the way <c>ShouldBe</c>
/// does. See issue #1404.
/// </summary>
[Collection(GlobalConfigCollection.Name)]
public class ShouldBeEqualityScenario
{
    [Fact]
    public void EquatableValues()
    {
        Should.Throw<ShouldAssertException>(() => new EquatableOnly(1).ShouldNotBeOneOf([new EquatableOnly(1)]));
        new EquatableOnly(1).ShouldNotBeOneOf([new EquatableOnly(2)]);
    }

    [Fact]
    public void ComparableValues() =>
        Should.Throw<ShouldAssertException>(() => new ComparableOnly(1).ShouldNotBeOneOf([new ComparableOnly(1)]));

    [Fact]
    public void CrossTypeNumerics() =>
        Should.Throw<ShouldAssertException>(() => ((object)1).ShouldNotBeOneOf([(object)1L]));

    [Fact]
    public void CollectionValuesAreComparedStructurally() =>
        Should.Throw<ShouldAssertException>(() => new[] { 1 }.ShouldNotBeOneOf([new[] { 1 }]));

    [Fact]
    public void FloatingPointHonoursTheDefaultTolerance()
    {
        (0.1 + 0.2).ShouldNotBeOneOf([0.3]);

        ShouldlyConfiguration.DefaultFloatingPointTolerance = 1e-9;
        try
        {
            Should.Throw<ShouldAssertException>(() => (0.1 + 0.2).ShouldNotBeOneOf([0.3]));
        }
        finally
        {
            ShouldlyConfiguration.DefaultFloatingPointTolerance = 0.0;
        }
    }

    [Fact]
    public void CallsEqualsOnExpected() =>
        ((Receiver)new Lenient()).ShouldNotBeOneOf([new Strict()]);

    [Fact]
    public void EquatableValueScenarioShouldFail()
    {
        var value = new EquatableOnly(2);
        Verify.ShouldFail(() =>
            value.ShouldNotBeOneOf([new EquatableOnly(1), new EquatableOnly(2)], "Some additional context"));
    }
}
