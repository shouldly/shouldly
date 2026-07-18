using Xunit;

namespace EquivalencyComparisonTests;

/// <summary>
/// Record and record-struct semantics, mirroring FluentAssertions.Equivalency.Specs/RecordSpecs.
/// </summary>
public class RecordSpecs
{
    [Fact]
    public void Records_with_identical_values_are_equivalent()
    {
        Compare.Run(new PersonRecord("John", 30), new PersonRecord("John", 30)).ShouldAgree(Outcome.Pass);
    }

    [Fact]
    public void Records_with_differing_values_are_not_equivalent()
    {
        Compare.Run(new PersonRecord("John", 30), new PersonRecord("Jane", 30)).ShouldAgree(Outcome.Fail);
    }

    [Fact]
    public void Record_structs_with_identical_values_are_equivalent()
    {
        Compare.Run(new PointRecordStruct(1, 2), new PointRecordStruct(1, 2)).ShouldAgree(Outcome.Pass);
    }

    [Fact]
    public void Record_structs_with_differing_values_are_not_equivalent()
    {
        Compare.Run(new PointRecordStruct(1, 2), new PointRecordStruct(1, 3)).ShouldAgree(Outcome.Fail);
    }
}

/// <summary>
/// Enum semantics, mirroring FluentAssertions.Equivalency.Specs/EnumSpecs.
/// </summary>
public class EnumSpecs
{
    [Fact]
    public void Equal_enum_values_are_equivalent()
    {
        Compare.Run(Color.Red, Color.Red).ShouldAgree(Outcome.Pass);
    }

    [Fact]
    public void Different_enum_values_are_not_equivalent()
    {
        Compare.Run(Color.Red, Color.Green).ShouldAgree(Outcome.Fail);
    }

    [Fact]
    public void Different_enum_types_with_the_same_underlying_value()
    {
        Compare.Run<object>(Color.Red, Shade.Crimson).ShouldDiverge(
            shouldly: Outcome.Fail,
            fluentAssertions: Outcome.Pass,
            because: "FluentAssertions compares enums by underlying numeric value by default; Shouldly requires identical runtime types");
    }

    [Fact]
    public void Enum_vs_its_underlying_integer_value_is_not_equivalent_in_either()
    {
        Compare.Run<object>(Color.Red, 1).ShouldAgree(Outcome.Fail);
    }
}
