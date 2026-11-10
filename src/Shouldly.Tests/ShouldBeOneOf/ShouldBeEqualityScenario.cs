namespace Shouldly.Tests.ShouldBeOneOf;

/// <summary>
/// Without a comparer, <c>ShouldBeOneOf</c> compares the value with each candidate the way <c>ShouldBe</c> does.
/// See issue #1404.
/// </summary>
[Collection(GlobalConfigCollection.Name)]
public class ShouldBeEqualityScenario
{
    [Fact]
    public void EquatableValues() =>
        new EquatableOnly(1).ShouldBeOneOf([new EquatableOnly(2), new EquatableOnly(1)]);

    [Fact]
    public void ComparableValues() =>
        new ComparableOnly(1).ShouldBeOneOf([new ComparableOnly(1)]);

    [Fact]
    public void CrossTypeNumerics() =>
        ((object)1).ShouldBeOneOf([(object)1L]);

    [Fact]
    public void CollectionValuesAreComparedStructurally() =>
        new[] { 1 }.ShouldBeOneOf([new[] { 2 }, new[] { 1 }]);

    [Fact]
    public void FloatingPointHonoursTheDefaultTolerance()
    {
        Should.Throw<ShouldAssertException>(() => (0.1 + 0.2).ShouldBeOneOf([0.3]));

        ShouldlyConfiguration.DefaultFloatingPointTolerance = 1e-9;
        try
        {
            (0.1 + 0.2).ShouldBeOneOf([0.3]);
        }
        finally
        {
            ShouldlyConfiguration.DefaultFloatingPointTolerance = 0.0;
        }
    }

    [Fact]
    public void CallsEqualsOnExpected() =>
        Should.Throw<ShouldAssertException>(() => ((Receiver)new Lenient()).ShouldBeOneOf([new Strict()]));

    [Fact]
    public void NullValue()
    {
        ((string?)null).ShouldBeOneOf(["a", null]);
        Should.Throw<ShouldAssertException>(() => ((string?)null).ShouldBeOneOf(["a"]));
    }

    [Fact]
    public void EquatableValueScenarioShouldFail()
    {
        var value = new EquatableOnly(3);
        Verify.ShouldFail(() =>
            value.ShouldBeOneOf([new EquatableOnly(1), new EquatableOnly(2)], "Some additional context"));
    }
}
