using Xunit;

namespace EquivalencyComparisonTests;

/// <summary>
/// Dictionary comparison semantics, mirroring FluentAssertions.Equivalency.Specs/DictionarySpecs.
/// Both libraries match dictionaries by key, insertion-order-insensitively, and recurse into
/// values (cf. shouldly#767, shouldly#1077 for the historical failures this design fixed).
/// </summary>
public class DictionarySpecs
{
    [Fact]
    public void Dictionaries_with_same_pairs_inserted_in_same_order_are_equivalent()
    {
        var actual = new Dictionary<string, int> { ["a"] = 1, ["b"] = 2 };
        var expected = new Dictionary<string, int> { ["a"] = 1, ["b"] = 2 };

        Compare.Run(actual, expected).ShouldAgree(Outcome.Pass);
    }

    [Fact]
    public void Dictionaries_with_same_pairs_inserted_in_different_order()
    {
        var actual = new Dictionary<string, int> { ["a"] = 1, ["b"] = 2 };
        var expected = new Dictionary<string, int> { ["b"] = 2, ["a"] = 1 };

        // Both match dictionaries by key, so insertion order is irrelevant.
        Compare.Run(actual, expected).ShouldAgree(Outcome.Pass);
    }

    [Fact]
    public void Dictionaries_with_a_differing_value_are_not_equivalent()
    {
        var actual = new Dictionary<string, int> { ["a"] = 1 };
        var expected = new Dictionary<string, int> { ["a"] = 2 };

        Compare.Run(actual, expected).ShouldAgree(Outcome.Fail);
    }

    [Fact]
    public void Dictionaries_with_a_missing_key_are_not_equivalent()
    {
        var actual = new Dictionary<string, int> { ["a"] = 1 };
        var expected = new Dictionary<string, int> { ["a"] = 1, ["b"] = 2 };

        Compare.Run(actual, expected).ShouldAgree(Outcome.Fail);
    }

    [Fact]
    public void Dictionaries_with_reference_typed_values_that_are_equal_but_not_the_same_instance()
    {
        var actual = new Dictionary<string, Person> { ["a"] = new() { Name = "John", Age = 30 } };
        var expected = new Dictionary<string, Person> { ["a"] = new() { Name = "John", Age = 30 } };

        // Both recurse into dictionary values (shouldly#1077).
        Compare.Run(actual, expected).ShouldAgree(Outcome.Pass);
    }

    [Fact]
    public void Dictionaries_with_collection_values_that_are_equal_but_not_the_same_instance()
    {
        var actual = new Dictionary<string, List<int>> { ["a"] = [1, 2] };
        var expected = new Dictionary<string, List<int>> { ["a"] = [1, 2] };

        // Both recurse into dictionary values, including collection values (shouldly#767).
        Compare.Run(actual, expected).ShouldAgree(Outcome.Pass);
    }
}
