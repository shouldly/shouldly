using Xunit;

namespace EquivalencyComparisonTests;

/// <summary>
/// Cyclic reference and recursion-depth semantics, mirroring
/// FluentAssertions.Equivalency.Specs/CyclicReferencesSpecs and the max-depth behavior in SelfReferenceSpecs.
/// </summary>
public class RecursionSpecs
{
    [Fact]
    public void Cyclic_graphs_with_identical_shape()
    {
        var actual = new Node { Name = "root" };
        actual.Next = actual;
        var expected = new Node { Name = "root" };
        expected.Next = expected;

        Compare.Run(actual, expected).ShouldDiverge(
            shouldly: Outcome.Pass,
            fluentAssertions: Outcome.Fail,
            because: "Shouldly tracks already-compared pairs and treats cycles as equivalent; FluentAssertions fails on cyclic references by default (IgnoringCyclicReferences is opt-in)");
    }

    [Fact]
    public void Two_node_cycles_with_a_differing_member_are_not_equivalent_in_either()
    {
        var actualA = new Node { Name = "a" };
        var actualB = new Node { Name = "b", Next = actualA };
        actualA.Next = actualB;

        var expectedA = new Node { Name = "a" };
        var expectedB = new Node { Name = "different", Next = expectedA };
        expectedA.Next = expectedB;

        Compare.Run(actualA, expectedA).ShouldAgree(Outcome.Fail);
    }

    [Fact]
    public void Deep_acyclic_graphs_beyond_ten_levels()
    {
        Compare.Run(BuildChain(15), BuildChain(15)).ShouldDiverge(
            shouldly: Outcome.Pass,
            fluentAssertions: Outcome.Fail,
            because: "FluentAssertions caps recursion at 10 levels by default (AllowingInfiniteRecursion is opt-in); Shouldly has no depth limit");
    }

    private static Node BuildChain(int depth)
    {
        var root = new Node { Name = "level0" };
        var current = root;
        for (var i = 1; i < depth; i++)
        {
            current.Next = new Node { Name = $"level{i}" };
            current = current.Next;
        }

        return root;
    }
}
