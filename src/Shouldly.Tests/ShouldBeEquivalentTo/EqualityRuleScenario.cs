namespace Shouldly.Tests.ShouldBeEquivalentTo;

// Two values that an equality rule covers are compared by the rule, whole, wherever they sit in the graph, as
// ShouldBe compares them. The rules used here are registered in ModuleInitializer, as a consumer registers theirs.
public class EqualityRuleScenario
{
    // Node stands in for Newtonsoft.Json's JToken: walked as a sequence, any two leaves look alike.
    [Fact]
    public void ShouldPassWhenTheRuleSaysTheValuesAreEqual()
    {
        new Branch(new Leaf("a"), new Leaf("b")).ShouldBeEquivalentTo(new Branch(new Leaf("a"), new Leaf("b")));
        new Leaf("a").ShouldBeEquivalentTo(new Leaf("a"));
    }

    [Fact]
    public void ShouldFailWhenTheRuleSaysTheValuesDiffer()
    {
        var subject = new Branch(new Leaf("a"), new Leaf("b"));
        Verify.ShouldFail(() =>
            subject.ShouldBeEquivalentTo(new Branch(new Leaf("a"), new Leaf("c")), "Some additional context"));
    }

    [Fact]
    public void ShouldFailWhenAMemberTheRuleCoversDiffers()
    {
        var subject = new Envelope { Id = 1, Payload = new Leaf("a"), Parts = [new Leaf("x"), new Leaf("y")] };
        Verify.ShouldFail(() =>
            subject.ShouldBeEquivalentTo(
                new Envelope { Id = 1, Payload = new Leaf("b"), Parts = [new Leaf("x"), new Leaf("z")] },
                "Some additional context"));
    }

    [Fact]
    public void ShouldPassWhenMembersTheRuleCoversAreEqual()
    {
        var subject = new Envelope { Id = 1, Payload = new Leaf("a"), Parts = [new Leaf("x"), new Leaf("y")] };

        subject.ShouldBeEquivalentTo(new Envelope { Id = 1, Payload = new Leaf("a"), Parts = [new Leaf("x"), new Leaf("y")] });
        subject.ShouldBeEquivalentTo(new { Payload = (Node)new Leaf("a") });
    }

    [Fact]
    public void ShouldCompareTheItemsOfACollectionByTheRule()
    {
        var subject = new Dictionary<string, Node> { ["k"] = new Leaf("a") };

        subject.ShouldBeEquivalentTo(new Dictionary<string, Node> { ["k"] = new Leaf("a") });
        Should.Throw<ShouldAssertException>(() =>
            subject.ShouldBeEquivalentTo(new Dictionary<string, Node> { ["k"] = new Leaf("b") }));

        new List<Node> { new Leaf("a"), new Leaf("b") }.ShouldBeEquivalentTo(
            new List<Node> { new Leaf("b"), new Leaf("a") },
            new EquivalencyOptions { IgnoreOrder = true });
        Should.Throw<ShouldAssertException>(() =>
            new List<Node> { new Leaf("a"), new Leaf("b") }.ShouldBeEquivalentTo(
                new List<Node> { new Leaf("b"), new Leaf("c") },
                new EquivalencyOptions { IgnoreOrder = true }));
    }

    [Fact]
    public void ShouldUseTheMostSpecificRule()
    {
        new CaseInsensitiveLeaf("A").ShouldBeEquivalentTo(new CaseInsensitiveLeaf("a"));
        Should.Throw<ShouldAssertException>(() => new Leaf("A").ShouldBeEquivalentTo(new Leaf("a")));
    }

    // Strange has no public members, so without its rule there is nothing of it to compare.
    [Fact]
    public void ShouldCompareAValueWithNoPublicMembersByItsRule()
    {
        Strange one = "one";
        Strange sameAsOne = "one";
        Strange two = "two";

        one.ShouldBeEquivalentTo(sameAsOne);
        new { Id = 1, Value = one }.ShouldBeEquivalentTo(new { Id = 1, Value = sameAsOne });
        Should.Throw<ShouldAssertException>(() => one.ShouldBeEquivalentTo(two));
        Should.Throw<ShouldAssertException>(() => new { Id = 1, Value = one }.ShouldBeEquivalentTo(new { Id = 1, Value = two }));
    }

    [Fact]
    public void ShouldCompareAValueTypeByItsRule()
    {
        new { Colour = Shade.Red }.ShouldBeEquivalentTo(new { Colour = Shade.Crimson });
        new { Colour = (Shade?)Shade.Red }.ShouldBeEquivalentTo(new { Colour = (Shade?)Shade.Crimson });
        Should.Throw<ShouldAssertException>(() =>
            new { Colour = Shade.Red }.ShouldBeEquivalentTo(new { Colour = Shade.Blue }));
    }

    // What a rule costs: IdBag's says two bags with the same Id are equal, so their items are not compared.
    [Fact]
    public void ShouldLeaveWhatTheRuleIgnoresUncompared()
    {
        new IdBag(1, 1, 2, 3).ShouldBeEquivalentTo(new IdBag(1, 7, 8, 9));
        new { Bag = new IdBag(1, 1, 2, 3) }.ShouldBeEquivalentTo(new { Bag = new IdBag(1, 7, 8, 9) });
        Should.Throw<ShouldAssertException>(() => new IdBag(1, 1, 2, 3).ShouldBeEquivalentTo(new IdBag(2, 1, 2, 3)));
    }

    // The options are for what is walked, and a value its rule decides isn't.
    [Fact]
    public void ShouldNotApplyTheOptionsInsideAValueTheRuleDecides()
    {
        Should.Throw<ShouldAssertException>(() =>
            new Branch(new Leaf("a"), new Leaf("b")).ShouldBeEquivalentTo(
                new Branch(new Leaf("b"), new Leaf("a")),
                new EquivalencyOptions { IgnoreOrder = true }));

        var ignoringId = new EquivalencyOptions();
        ignoringId.MembersToIgnore.Add(nameof(IdBag.Id));
        Should.Throw<ShouldAssertException>(() =>
            new IdBag(1, 1, 2, 3).ShouldBeEquivalentTo(new IdBag(2, 1, 2, 3), ignoringId));
    }

    // A rule is for two values of its type. With only one, the values are compared as they would be without it.
    [Fact]
    public void ShouldWalkTheValuesWhenTheRuleCoversOnlyOneOfThem()
    {
        new IdBag(1, 1, 2, 3).ShouldBeEquivalentTo(new[] { 1, 2, 3 });
        Should.Throw<ShouldAssertException>(() => new IdBag(1, 1, 2, 3).ShouldBeEquivalentTo(new[] { 1, 2, 4 }));
    }

    private class Envelope
    {
        public int Id { get; set; }
        public Node? Payload { get; set; }
        public List<Node> Parts { get; set; } = [];
    }
}
