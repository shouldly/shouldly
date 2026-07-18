using Xunit;

namespace EquivalencyComparisonTests;

/// <summary>
/// Declared-type vs runtime-type semantics, mirroring FluentAssertions.Equivalency.Specs and
/// the subject of shouldly#1094: both libraries select members from the declared
/// (compile-time) type of a member by default.
/// </summary>
public class InheritanceSpecs
{
    [Fact]
    public void Members_declared_as_base_type_holding_derived_instances_with_differing_derived_members()
    {
        var actual = new PetOwner { Pet = new Dog { Name = "Rex", Breed = "Lab" } };
        var expected = new PetOwner { Pet = new Dog { Name = "Rex", Breed = "Poodle" } };

        // Since shouldly#1094, Shouldly selects members from the declared type Animal like
        // FluentAssertions, so the derived-only Breed member is not compared.
        Compare.Run(actual, expected).ShouldAgree(Outcome.Pass);
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
            because: "the static type Animal drives FluentAssertions' member selection at the root; Shouldly's object-typed API cannot see the static type, so the root falls back to the expectation's runtime type Dog");
    }
}
