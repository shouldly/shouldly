using Xunit;

namespace EquivalencyComparisonTests;

/// <summary>
/// Declared-type vs runtime-type semantics, mirroring FluentAssertions.Equivalency.Specs:
/// FluentAssertions selects members from the declared (compile-time) type by default, and
/// Shouldly from the type of the expected value itself.
/// </summary>
public class InheritanceSpecs
{
    [Fact]
    public void Members_declared_as_base_type_holding_derived_instances_with_differing_derived_members()
    {
        var actual = new PetOwner { Pet = new Dog { Name = "Rex", Breed = "Lab" } };
        var expected = new PetOwner { Pet = new Dog { Name = "Rex", Breed = "Poodle" } };

        Compare.Run(actual, expected).ShouldDiverge(
            shouldly: Outcome.Fail,
            fluentAssertions: Outcome.Pass,
            because: "FluentAssertions selects members from the declared type Animal, so the derived-only Breed is never compared; Shouldly compares the expected value by the members of its own type, Dog, so a difference in a member the declared type doesn't have can't pass unnoticed");
    }

    [Fact]
    public void Members_declared_as_base_type_with_differing_base_members_are_not_equivalent_in_either()
    {
        var actual = new PetOwner { Pet = new Dog { Name = "Rex", Breed = "Lab" } };
        var expected = new PetOwner { Pet = new Dog { Name = "Fido", Breed = "Lab" } };

        Compare.Run(actual, expected).ShouldAgree(Outcome.Fail);
    }

    [Fact]
    public void Root_objects_compared_through_a_base_typed_reference_with_differing_derived_members()
    {
        Animal actual = new Dog { Name = "Rex", Breed = "Lab" };
        Animal expected = new Dog { Name = "Rex", Breed = "Poodle" };

        Compare.Run(actual, expected).ShouldDiverge(
            shouldly: Outcome.Fail,
            fluentAssertions: Outcome.Pass,
            because: "the static type Animal drives FluentAssertions' member selection at the root; Shouldly compares the expected value by the members of its own type, Dog, at the root as everywhere else");
    }
}
