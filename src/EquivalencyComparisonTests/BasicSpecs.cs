using Xunit;

namespace EquivalencyComparisonTests;

/// <summary>
/// Core object-graph comparison semantics, mirroring FluentAssertions.Equivalency.Specs/BasicSpecs
/// and MemberMatchingSpecs.
/// </summary>
public class BasicSpecs
{
    [Fact]
    public void Same_type_with_identical_member_values_is_equivalent()
    {
        var actual = new Person { Name = "John", Age = 30 };
        var expected = new Person { Name = "John", Age = 30 };

        Compare.Run(actual, expected).ShouldAgree(Outcome.Pass);
    }

    [Fact]
    public void Same_type_with_a_differing_member_value_is_not_equivalent()
    {
        var actual = new Person { Name = "John", Age = 30 };
        var expected = new Person { Name = "Jane", Age = 30 };

        Compare.Run(actual, expected).ShouldAgree(Outcome.Fail);
    }

    [Fact]
    public void Different_types_with_identical_shape_and_values()
    {
        var actual = new Person { Name = "John", Age = 30 };
        var expected = new Customer { Name = "John", Age = 30 };

        // Both compare structurally by the expectation's members; runtime type identity is not required.
        Compare.Run<object>(actual, expected).ShouldAgree(Outcome.Pass);
    }

    [Fact]
    public void Anonymous_type_expectation_with_all_members_matching()
    {
        var actual = new Person { Name = "John", Age = 30 };
        var expected = new { Name = "John", Age = 30 };

        Compare.Run<object>(actual, expected).ShouldAgree(Outcome.Pass);
    }

    [Fact]
    public void Anonymous_type_expectation_selecting_a_subset_of_members()
    {
        var actual = new Person { Name = "John", Age = 30 };
        var expected = new { Name = "John" };

        // Subset semantics: only members present on the expectation are compared.
        Compare.Run<object>(actual, expected).ShouldAgree(Outcome.Pass);
    }

    [Fact]
    public void Null_is_equivalent_to_null()
    {
        Compare.Run<Person?>(null, null).ShouldAgree(Outcome.Pass);
    }

    [Fact]
    public void Null_actual_is_not_equivalent_to_an_instance()
    {
        Compare.Run<Person?>(null, new Person()).ShouldAgree(Outcome.Fail);
    }

    [Fact]
    public void Instance_is_not_equivalent_to_null_expectation()
    {
        Compare.Run<Person?>(new Person(), null).ShouldAgree(Outcome.Fail);
    }

    [Fact]
    public void Same_reference_is_equivalent_to_itself()
    {
        var person = new Person { Name = "John", Age = 30 };

        Compare.Run(person, person).ShouldAgree(Outcome.Pass);
    }

    [Fact]
    public void Nested_objects_with_identical_values_are_equivalent()
    {
        var actual = new PersonWithAddress { Name = "John", Address = new() { Street = "1 Main St", City = "Springfield" } };
        var expected = new PersonWithAddress { Name = "John", Address = new() { Street = "1 Main St", City = "Springfield" } };

        Compare.Run(actual, expected).ShouldAgree(Outcome.Pass);
    }

    [Fact]
    public void Nested_objects_with_a_differing_leaf_value_are_not_equivalent()
    {
        var actual = new PersonWithAddress { Name = "John", Address = new() { Street = "1 Main St", City = "Springfield" } };
        var expected = new PersonWithAddress { Name = "John", Address = new() { Street = "2 Main St", City = "Springfield" } };

        Compare.Run(actual, expected).ShouldAgree(Outcome.Fail);
    }

    [Fact]
    public void Differing_private_fields_are_ignored_by_both()
    {
        var actual = new FieldHolder("same", "one");
        var expected = new FieldHolder("same", "two");

        Compare.Run(actual, expected).ShouldAgree(Outcome.Pass);
    }

    [Fact]
    public void Differing_private_properties_are_ignored_by_both()
    {
        var actual = new PrivatePropertyHolder("same", "one");
        var expected = new PrivatePropertyHolder("same", "two");

        Compare.Run(actual, expected).ShouldAgree(Outcome.Pass);
    }

    [Fact]
    public void Differing_public_fields_are_compared_by_both()
    {
        var actual = new FieldHolder("one", "same");
        var expected = new FieldHolder("two", "same");

        Compare.Run(actual, expected).ShouldAgree(Outcome.Fail);
    }

    [Fact]
    public void Types_with_indexers()
    {
        var actual = new IndexerHolder { Name = "same" };
        var expected = new IndexerHolder { Name = "same" };

        // Both skip indexers (there is no general way to enumerate their values).
        Compare.Run(actual, expected).ShouldAgree(Outcome.Pass);
    }

    [Fact]
    public void Custom_Equals_reporting_equal_when_members_differ()
    {
        var actual = new EqualsById { Id = 1, Name = "John" };
        var expected = new EqualsById { Id = 1, Name = "Jane" };

        Compare.Run(actual, expected).ShouldDiverge(
            shouldly: Outcome.Fail,
            fluentAssertions: Outcome.Pass,
            because: "FluentAssertions uses a type's Equals override by default (value semantics); Shouldly always walks members of reference types");
    }

    [Fact]
    public void Custom_Equals_reporting_not_equal_when_members_are_identical()
    {
        var actual = new EqualsNever { Name = "John" };
        var expected = new EqualsNever { Name = "John" };

        Compare.Run(actual, expected).ShouldDiverge(
            shouldly: Outcome.Pass,
            fluentAssertions: Outcome.Fail,
            because: "FluentAssertions honors the Equals override even when it reports inequality; Shouldly ignores Equals on reference types and compares members");
    }
}
