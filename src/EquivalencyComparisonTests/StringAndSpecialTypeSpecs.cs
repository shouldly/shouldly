using Xunit;

namespace EquivalencyComparisonTests;

/// <summary>
/// String comparison semantics.
/// </summary>
public class StringSpecs
{
    [Fact]
    public void Identical_strings_are_equivalent()
    {
        Compare.Run("hello", "hello").ShouldAgree(Outcome.Pass);
    }

    [Fact]
    public void Strings_differing_in_case_are_not_equivalent()
    {
        Compare.Run("hello", "HELLO").ShouldAgree(Outcome.Fail);
    }

    [Fact]
    public void Strings_differing_in_trailing_whitespace_are_not_equivalent()
    {
        Compare.Run("hello", "hello ").ShouldAgree(Outcome.Fail);
    }
}

/// <summary>
/// Types both libraries treat as comparison leaves rather than walking their members
/// (cf. shouldly#1050 for Type, shouldly#1205 for Uri).
/// </summary>
public class SpecialTypeSpecs
{
    [Fact]
    public void Equal_uris_are_equivalent()
    {
        Compare.Run(new Uri("https://example.com/a"), new Uri("https://example.com/a")).ShouldAgree(Outcome.Pass);
    }

    [Fact]
    public void Different_uris_are_not_equivalent()
    {
        Compare.Run(new Uri("https://example.com/a"), new Uri("https://example.com/b")).ShouldAgree(Outcome.Fail);
    }

    [Fact]
    public void Uris_differing_only_in_fragment_are_equivalent_in_both()
    {
        // Uri.Equals ignores the fragment; both libraries defer to it.
        Compare.Run(new Uri("https://example.com/a#one"), new Uri("https://example.com/a#two")).ShouldAgree(Outcome.Pass);
    }

    [Fact]
    public void Equal_type_valued_members_are_equivalent()
    {
        var actual = new TypeHolder { Type = typeof(string) };
        var expected = new TypeHolder { Type = typeof(string) };

        Compare.Run(actual, expected).ShouldAgree(Outcome.Pass);
    }

    [Fact]
    public void Different_type_valued_members()
    {
        var actual = new TypeHolder { Type = typeof(string) };
        var expected = new TypeHolder { Type = typeof(int) };

        // Both treat Type as a leaf value compared with Equals (shouldly#1050).
        Compare.Run(actual, expected).ShouldAgree(Outcome.Fail);
    }

    [Fact]
    public void Objects_with_no_members()
    {
        Compare.Run(new object(), new object()).ShouldDiverge(
            shouldly: Outcome.Fail,
            fluentAssertions: Outcome.Error,
            because: "both refuse to compare zero members, differently: Shouldly's vacuous-comparison guard fails the assertion with guidance; FluentAssertions throws an InvalidOperationException");
    }

    [Fact]
    public void Equal_values_with_no_public_members_and_their_own_equality_are_equivalent()
    {
        // With no members to walk, both compare such a type by its own Equals.
        Compare.Run(new { Value = new OpaqueValue("a") }, new { Value = new OpaqueValue("a") }).ShouldAgree(Outcome.Pass);
    }

    [Fact]
    public void Different_values_with_no_public_members_and_their_own_equality_are_not_equivalent()
    {
        Compare.Run(new { Value = new OpaqueValue("a") }, new { Value = new OpaqueValue("b") }).ShouldAgree(Outcome.Fail);
    }
}
