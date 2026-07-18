using Xunit;

namespace EquivalencyComparisonTests;

/// <summary>
/// Value type, struct, and primitive semantics, mirroring FluentAssertions.Equivalency.Specs/StructSpecs
/// and the primitive handling in BasicSpecs.
/// </summary>
public class ValueTypeSpecs
{
    [Fact]
    public void Structs_with_identical_members_are_equivalent()
    {
        Compare.Run(new Point { X = 1, Y = 2 }, new Point { X = 1, Y = 2 }).ShouldAgree(Outcome.Pass);
    }

    [Fact]
    public void Structs_with_differing_members_are_not_equivalent()
    {
        Compare.Run(new Point { X = 1, Y = 2 }, new Point { X = 1, Y = 3 }).ShouldAgree(Outcome.Fail);
    }

    [Fact]
    public void Equal_integers_are_equivalent()
    {
        Compare.Run(42, 42).ShouldAgree(Outcome.Pass);
    }

    [Fact]
    public void Different_integers_are_not_equivalent()
    {
        Compare.Run(42, 43).ShouldAgree(Outcome.Fail);
    }

    [Fact]
    public void Int_vs_long_with_the_same_numeric_value()
    {
        // Both treat numeric types holding the same value as equivalent (lossless cross-numeric equality).
        Compare.Run<object>(1, 1L).ShouldAgree(Outcome.Pass);
    }

    [Fact]
    public void Decimals_differing_only_in_scale_are_equivalent()
    {
        Compare.Run(1.0m, 1.00m).ShouldAgree(Outcome.Pass);
    }

    [Fact]
    public void NaN_is_equivalent_to_NaN()
    {
        Compare.Run(double.NaN, double.NaN).ShouldAgree(Outcome.Pass);
    }

    [Fact]
    public void Equal_guids_are_equivalent()
    {
        var guid = Guid.NewGuid();
        Compare.Run(guid, new Guid(guid.ToString())).ShouldAgree(Outcome.Pass);
    }

    [Fact]
    public void Equal_date_times_are_equivalent()
    {
        Compare.Run(new DateTime(2026, 7, 11), new DateTime(2026, 7, 11)).ShouldAgree(Outcome.Pass);
    }

    [Fact]
    public void Date_times_differing_by_one_tick_are_not_equivalent()
    {
        var now = new DateTime(2026, 7, 11, 12, 0, 0);
        Compare.Run(now, now.AddTicks(1)).ShouldAgree(Outcome.Fail);
    }

    [Fact]
    public void Date_times_with_same_ticks_but_different_kind_are_equivalent_in_both()
    {
        // DateTime.Equals ignores Kind; both libraries defer to it.
        var utc = new DateTime(2026, 7, 11, 12, 0, 0, DateTimeKind.Utc);
        var local = new DateTime(2026, 7, 11, 12, 0, 0, DateTimeKind.Local);

        Compare.Run(utc, local).ShouldAgree(Outcome.Pass);
    }

    [Fact]
    public void Date_time_offsets_with_same_instant_but_different_offsets()
    {
        var one = new DateTimeOffset(2026, 7, 11, 12, 0, 0, TimeSpan.Zero);
        var two = new DateTimeOffset(2026, 7, 11, 14, 0, 0, TimeSpan.FromHours(2));

        // DateTimeOffset.Equals compares the instant only; both libraries defer to it.
        Compare.Run(one, two).ShouldAgree(Outcome.Pass);
    }

    [Fact]
    public void Nullable_int_with_value_is_equivalent_to_plain_int()
    {
        int? actual = 5;
        Compare.Run<object>(actual, 5).ShouldAgree(Outcome.Pass);
    }
}
