using Xunit;

namespace EquivalencyComparisonTests;

/// <summary>
/// Collection comparison semantics, mirroring FluentAssertions.Equivalency.Specs/CollectionSpecs.
/// The headline difference: FluentAssertions matches collections order-insensitively by default
/// (except byte arrays); Shouldly always compares element-by-element in order.
/// </summary>
public class CollectionSpecs
{
    [Fact]
    public void Lists_with_same_elements_in_same_order_are_equivalent()
    {
        Compare.Run(new List<int> { 1, 2, 3 }, new List<int> { 1, 2, 3 }).ShouldAgree(Outcome.Pass);
    }

    [Fact]
    public void Lists_with_same_elements_in_different_order()
    {
        Compare.Run(new List<int> { 1, 2, 3 }, new List<int> { 3, 2, 1 }).ShouldDiverge(
            shouldly: Outcome.Fail,
            fluentAssertions: Outcome.Pass,
            because: "FluentAssertions matches collections order-insensitively by default; Shouldly compares element-by-element in order");
    }

    [Fact]
    public void Collection_order_inside_an_object_graph()
    {
        var actual = new OrderHolder { Values = [1, 2, 3] };
        var expected = new OrderHolder { Values = [3, 2, 1] };

        Compare.Run(actual, expected).ShouldDiverge(
            shouldly: Outcome.Fail,
            fluentAssertions: Outcome.Pass,
            because: "order-insensitivity also applies to collections nested inside object graphs in FluentAssertions");
    }

    [Fact]
    public void Lists_with_different_counts_are_not_equivalent()
    {
        Compare.Run(new List<int> { 1, 2 }, new List<int> { 1, 2, 3 }).ShouldAgree(Outcome.Fail);
    }

    [Fact]
    public void Lists_with_same_counts_but_different_duplicate_multiplicity_are_not_equivalent()
    {
        Compare.Run(new List<int> { 1, 1, 2 }, new List<int> { 1, 2, 2 }).ShouldAgree(Outcome.Fail);
    }

    [Fact]
    public void Array_vs_list_with_identical_elements()
    {
        // Both ignore the container type of collections; only the elements matter.
        Compare.Run<object>(new[] { 1, 2, 3 }, new List<int> { 1, 2, 3 }).ShouldAgree(Outcome.Pass);
    }

    [Fact]
    public void Hash_sets_with_same_elements_inserted_in_different_order()
    {
        var actual = new HashSet<int> { 1, 2, 3 };
        var expected = new HashSet<int> { 3, 2, 1 };

        // Sets have no defined order; both compare them order-insensitively.
        Compare.Run(actual, expected).ShouldAgree(Outcome.Pass);
    }

    [Fact]
    public void Empty_collections_are_equivalent()
    {
        Compare.Run(new List<int>(), new List<int>()).ShouldAgree(Outcome.Pass);
    }

    [Fact]
    public void Byte_arrays_in_different_order_are_not_equivalent_in_either()
    {
        // Byte arrays are the one collection FluentAssertions compares strictly in order by default.
        Compare.Run(new byte[] { 1, 2, 3 }, new byte[] { 3, 2, 1 }).ShouldAgree(Outcome.Fail);
    }

    [Fact]
    public void Multidimensional_arrays_with_same_flat_content_but_different_shape()
    {
        var actual = new int[2, 3] { { 1, 2, 3 }, { 4, 5, 6 } };
        var expected = new int[3, 2] { { 1, 2 }, { 3, 4 }, { 5, 6 } };

        // Both check rank and per-dimension lengths.
        Compare.Run<object>(actual, expected).ShouldAgree(Outcome.Fail);
    }

    [Fact]
    public void Lists_of_complex_objects_with_identical_values_in_order_are_equivalent()
    {
        var actual = new List<Person> { new() { Name = "A", Age = 1 }, new() { Name = "B", Age = 2 } };
        var expected = new List<Person> { new() { Name = "A", Age = 1 }, new() { Name = "B", Age = 2 } };

        Compare.Run(actual, expected).ShouldAgree(Outcome.Pass);
    }

    [Fact]
    public void Lists_of_complex_objects_with_a_differing_member_are_not_equivalent()
    {
        var actual = new List<Person> { new() { Name = "A", Age = 1 } };
        var expected = new List<Person> { new() { Name = "A", Age = 2 } };

        Compare.Run(actual, expected).ShouldAgree(Outcome.Fail);
    }

    [Fact]
    public void Lists_of_complex_objects_in_different_order()
    {
        var actual = new List<Person> { new() { Name = "A", Age = 1 }, new() { Name = "B", Age = 2 } };
        var expected = new List<Person> { new() { Name = "B", Age = 2 }, new() { Name = "A", Age = 1 } };

        Compare.Run(actual, expected).ShouldDiverge(
            shouldly: Outcome.Fail,
            fluentAssertions: Outcome.Pass,
            because: "FluentAssertions matches complex-typed collection items order-insensitively by structural equivalence");
    }

    [Fact]
    public void Null_collection_vs_empty_collection_is_not_equivalent_in_either()
    {
        Compare.Run<List<int>?>(null, []).ShouldAgree(Outcome.Fail);
    }
}
