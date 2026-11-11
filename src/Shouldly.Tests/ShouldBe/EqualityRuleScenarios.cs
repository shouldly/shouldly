namespace Shouldly.Tests.ShouldBe;

/// <summary>
/// Every assertion family that compares values honours an equality rule registered with
/// <see cref="ShouldlyConfiguration.UseEquality{T}(Func{T, T, bool})"/>. <see cref="Node"/> emulates JToken, whose
/// subtypes walked as sequences compare only by shape, so these failed silently before the rule existed (#65).
/// </summary>
public class EqualityRuleScenarios
{
    private static Node A => new Leaf("a");
    private static Node B => new Leaf("b");

    [Fact]
    public void ShouldBe()
    {
        A.ShouldBe(A);
        new Branch(A, B).ShouldBe(new Branch(A, B));
        Should.Throw<ShouldAssertException>(() => A.ShouldBe(B));
        Should.Throw<ShouldAssertException>(() => new Leaf("a").ShouldBe(new Leaf("b")));
    }

    [Fact]
    public void ShouldNotBe()
    {
        A.ShouldNotBe(B);
        Should.Throw<ShouldAssertException>(() => A.ShouldNotBe(A));
    }

    [Fact]
    public void Sequences()
    {
        new List<Node> { A, B }.ShouldBe([A, B]);
        new List<Node> { A, B }.ShouldBe([B, A], ignoreOrder: true);
        Should.Throw<ShouldAssertException>(() => new List<Node> { A }.ShouldBe([B]));
        Should.Throw<ShouldAssertException>(() => new List<Node> { A }.ShouldBe([B], ignoreOrder: true));
    }

    [Fact]
    public void RuleCoveredSequenceIgnoringOrder()
    {
        // The rule (like JToken.DeepEquals) is order-sensitive, so it decides only when order matters; with
        // ignoreOrder the elements are matched in any order, each by the rule.
        Should.Throw<ShouldAssertException>(() => new Branch(A, B).ShouldBe(new Branch(B, A)));
        new Branch(A, B).ShouldBe(new Branch(B, A), ignoreOrder: true);
        Should.Throw<ShouldAssertException>(() => new Branch(A, B).ShouldBe(new Branch(A, A), ignoreOrder: true));
    }

    [Fact]
    public void DictionaryValues()
    {
        var dictionary = new Dictionary<string, Node> { ["k"] = A };

        dictionary.ShouldBe(new Dictionary<string, Node> { ["k"] = A });
        dictionary.ShouldContainKeyAndValue("k", A);
        Should.Throw<ShouldAssertException>(() => dictionary.ShouldBe(new Dictionary<string, Node> { ["k"] = B }));
        Should.Throw<ShouldAssertException>(() => dictionary.ShouldContainKeyAndValue("k", B));
    }

    [Fact]
    public void Membership()
    {
        List<Node> nodes = [A, B];

        nodes.ShouldContain(A);
        nodes.ShouldNotContain(new Leaf("c"));
        nodes.ShouldBeUnique();
        A.ShouldBeOneOf(nodes.ToArray());
        Should.Throw<ShouldAssertException>(() => nodes.ShouldContain(new Leaf("c")));
        Should.Throw<ShouldAssertException>(() => new List<Node> { A, A }.ShouldBeUnique());
    }

    [Fact]
    public void MembershipOfAValueTypeAndItsNullableForm()
    {
        // Shade's values would otherwise be hashed, which ignores the rule (Red and Crimson are equal under it).
        new[] { Shade.Red }.ShouldBeSubsetOf([Shade.Crimson]);
        new Shade?[] { Shade.Red }.ShouldBeSubsetOf([Shade.Crimson]);
        new HashSet<Shade?> { Shade.Red }.ShouldContain(Shade.Crimson);
        new HashSet<Shade> { Shade.Red }.ShouldContain(Shade.Crimson);
        Should.Throw<ShouldAssertException>(() => new Shade?[] { Shade.Red, Shade.Crimson }.ShouldBeUnique());
        Should.Throw<ShouldAssertException>(() => new Shade?[] { Shade.Blue }.ShouldBeSubsetOf([Shade.Crimson]));
    }
}
