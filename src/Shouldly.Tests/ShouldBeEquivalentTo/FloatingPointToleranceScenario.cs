using Shouldly.Tests.TestHelpers;

namespace Shouldly.Tests.ShouldBeEquivalentTo;

/// <summary>
/// ShouldBeEquivalentTo's leaf comparison honours <see cref="ShouldlyConfiguration.DefaultFloatingPointTolerance"/>
/// the way ShouldBe does (#1409).
/// </summary>
// These tests mutate the global ShouldlyConfiguration.DefaultFloatingPointTolerance, so the class runs in the
// non-parallel Global configuration collection to keep that change from leaking into floating-point assertions
// in other test classes.
[Collection(GlobalConfigCollection.Name)]
public class FloatingPointToleranceScenario
{
    [Fact]
    public void DoublesWithinTheDefaultToleranceAreEquivalent()
    {
        WithDefaultTolerance(1e-9, () => (0.1 + 0.2).ShouldBeEquivalentTo(0.3));
    }

    [Fact]
    public void DoublesAreComparedExactlyWithoutADefaultTolerance()
    {
        Should.Throw<ShouldAssertException>(() => (0.1 + 0.2).ShouldBeEquivalentTo(0.3));
    }

    [Fact]
    public void FloatsWithinTheDefaultToleranceAreEquivalent()
    {
        WithDefaultTolerance(0.01, () => 1.0f.ShouldBeEquivalentTo(1.005f));
    }

    [Fact]
    public void FloatAndDoubleWithinTheDefaultToleranceAreEquivalent()
    {
        WithDefaultTolerance(0.01, () => ((object)1.0f).ShouldBeEquivalentTo(1.005d));
    }

    [Fact]
    public void FloatAndIntegerAreComparedAsDoubles()
    {
        // 16777217 isn't representable as a float. Converting both sides to float, as Numerics does for
        // float-vs-integer, would make these equal; equivalency keeps its lossless widening to double.
        Should.Throw<ShouldAssertException>(() => ((object)16777217).ShouldBeEquivalentTo(16777216f));
    }

    [Fact]
    public void CollectionElementsWithinTheDefaultToleranceAreEquivalent()
    {
        WithDefaultTolerance(1e-9, () =>
        {
            new[] { 0.1 + 0.2 }.ShouldBeEquivalentTo(new[] { 0.3 });
            new List<double> { 0.1 + 0.2, 1.0 }.ShouldBeEquivalentTo(new[] { 1.0, 0.3 }, new EquivalencyOptions { IgnoreOrder = true });
        });
    }

    [Fact]
    public void MembersWithinTheDefaultToleranceAreEquivalent()
    {
        WithDefaultTolerance(1e-9, () =>
            new Measurement { Value = 0.1 + 0.2 }.ShouldBeEquivalentTo(new Measurement { Value = 0.3 }));
    }

    [Fact]
    public void RecordMembersWithinTheDefaultToleranceAreEquivalent()
    {
        // Records (and tuples, below) are walked member-wise rather than compared with their own Equals, so their
        // members honour the tolerance too (unlike ShouldBe, which uses the record's Equals).
        WithDefaultTolerance(1e-9, () =>
            new MeasurementRecord(0.1 + 0.2).ShouldBeEquivalentTo(new MeasurementRecord(0.3)));
    }

    [Fact]
    public void TupleItemsWithinTheDefaultToleranceAreEquivalent()
    {
        WithDefaultTolerance(1e-9, () => (1, 0.1 + 0.2).ShouldBeEquivalentTo((1, 0.3)));
    }

    [Fact]
    public void DictionaryValuesWithinTheDefaultToleranceAreEquivalent()
    {
        WithDefaultTolerance(1e-9, () =>
            new Dictionary<string, double> { ["key"] = 0.1 + 0.2 }
                .ShouldBeEquivalentTo(new Dictionary<string, double> { ["key"] = 0.3 }));
    }

    [Fact]
    public void DictionaryKeysWithinTheDefaultToleranceMatch()
    {
        // Keys are matched by equivalence, not by the dictionary's own comparer, so they honour the tolerance too.
        WithDefaultTolerance(1e-9, () =>
            new Dictionary<double, string> { [0.1 + 0.2] = "value" }
                .ShouldBeEquivalentTo(new Dictionary<double, string> { [0.3] = "value" }));
    }

    [Fact]
    public void NaNIsStillEquivalentToNaN()
    {
        WithDefaultTolerance(1e-9, () => double.NaN.ShouldBeEquivalentTo(double.NaN));
    }

    [Fact]
    public void DecimalsIgnoreTheDefaultTolerance()
    {
        // As with ShouldBe, the default tolerance only applies to floating-point values.
        WithDefaultTolerance(0.1, () =>
            Should.Throw<ShouldAssertException>(() => 1.0m.ShouldBeEquivalentTo(1.05m)));
    }

    [Fact]
    public void ShouldFailWhenOutsideTheDefaultTolerance()
    {
        var subject = new Measurement { Value = 1.5 };
        WithDefaultTolerance(0.1, () =>
            Verify.ShouldFail(() =>
                subject.ShouldBeEquivalentTo(new Measurement { Value = 1.0 }, "Some additional context")));
    }

    private static void WithDefaultTolerance(double tolerance, Action action)
    {
        var previous = ShouldlyConfiguration.DefaultFloatingPointTolerance;
        ShouldlyConfiguration.DefaultFloatingPointTolerance = tolerance;
        try
        {
            action();
        }
        finally
        {
            ShouldlyConfiguration.DefaultFloatingPointTolerance = previous;
        }
    }

    private class Measurement
    {
        public double Value { get; set; }
    }

    private record MeasurementRecord(double Value);
}
