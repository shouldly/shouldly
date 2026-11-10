namespace Shouldly.Tests.ShouldBeEquivalentTo;

/// <summary>
/// Numeric leaves of different types are equivalent only when they hold the same mathematical value; neither side is
/// rounded to the other's type first (#1427).
/// </summary>
// LeavesHonourTheDefaultTolerance mutates the global ShouldlyConfiguration.DefaultFloatingPointTolerance, so the class
// runs in the non-parallel Global configuration collection to keep that change from leaking into other test classes.
[Collection(GlobalConfigCollection.Name)]
public class CrossTypeNumericScenario
{
    [Fact]
    public void LongIsNotEquivalentToTheDoubleItRoundsTo() =>
        Should.Throw<ShouldAssertException>(() =>
            new { V = 9007199254740993L }.ShouldBeEquivalentTo(new { V = 9007199254740992.0 }));

    [Fact]
    public void DecimalIsNotEquivalentToTheDoubleItRoundsTo()
    {
        Should.Throw<ShouldAssertException>(() =>
            new { V = 0.1000000000000000001m }.ShouldBeEquivalentTo(new { V = 0.1 }));
        Should.Throw<ShouldAssertException>(() =>
            new { V = 0.1m }.ShouldBeEquivalentTo(new { V = 0.1 }));
    }

    [Fact]
    public void FloatIsNotEquivalentToADifferentDouble() =>
        Should.Throw<ShouldAssertException>(() =>
            new { V = 0.1f }.ShouldBeEquivalentTo(new { V = 0.1 }));

    [Fact]
    public void SameValuesOfDifferentTypesAreEquivalent()
    {
        new { V = 9007199254740992L }.ShouldBeEquivalentTo(new { V = 9007199254740992.0 });
        new { V = 0.5m }.ShouldBeEquivalentTo(new { V = 0.5f });
        new { V = 16777216 }.ShouldBeEquivalentTo(new { V = 16777216f });
        new { V = 1.5f }.ShouldBeEquivalentTo(new { V = 1.5 });
        new object[] { 1, 2L, 3m }.ShouldBeEquivalentTo(new object[] { 1.0, 2f, 3.0 });
    }

    [Fact]
    public void LeavesHonourTheDefaultTolerance()
    {
        ShouldlyConfiguration.DefaultFloatingPointTolerance = 1e-9;
        try
        {
            new { V = 0.1m }.ShouldBeEquivalentTo(new { V = 0.1 });
            new { V = 9007199254740993L }.ShouldBeEquivalentTo(new { V = 9007199254740992.0 });
        }
        finally
        {
            ShouldlyConfiguration.DefaultFloatingPointTolerance = 0.0;
        }
    }
}
