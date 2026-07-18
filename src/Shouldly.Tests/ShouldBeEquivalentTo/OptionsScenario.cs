namespace Shouldly.Tests.ShouldBeEquivalentTo;

public class OptionsScenario
{
    [Fact]
    public void ShouldPassWhenOrderDiffersAndIgnoreOrderIsSet()
    {
        var subject = new List<FakeObject>
        {
            new() { Id = 1, Name = "One" },
            new() { Id = 2, Name = "Two" }
        };
        var expected = new List<FakeObject>
        {
            new() { Id = 2, Name = "Two" },
            new() { Id = 1, Name = "One" }
        };

        subject.ShouldBeEquivalentTo(expected, new EquivalencyOptions { IgnoreOrder = true });
    }

    [Fact]
    public void ShouldPassWhenNestedCollectionOrderDiffersAndIgnoreOrderIsSet()
    {
        var subject = new FakeObject { Id = 1, Colors = ["red", "blue"] };
        var expected = new FakeObject { Id = 1, Colors = ["blue", "red"] };

        subject.ShouldBeEquivalentTo(expected, new EquivalencyOptions { IgnoreOrder = true });
    }

    [Fact]
    public void ShouldFailWhenOrderDiffersAndIgnoreOrderIsNotSet()
    {
        var subject = new List<int> { 1, 2, 3 };
        var expected = new List<int> { 3, 2, 1 };

        Verify.ShouldFail(() =>
            subject.ShouldBeEquivalentTo(expected, "Some additional context"));
    }

    [Fact]
    public void ShouldFailWhenElementIsMissingWithIgnoreOrder()
    {
        var subject = new List<int> { 1, 2, 6 };
        var expected = new List<int> { 1, 2, 3 };

        Verify.ShouldFail(() =>
            subject.ShouldBeEquivalentTo(expected, new EquivalencyOptions { IgnoreOrder = true }, "Some additional context"));
    }

    [Fact]
    public void ShouldMatchAcrossHeterogeneousElementsWithIgnoreOrder()
    {
        // The looser {Id=1} expectation is equivalent to both actual elements, but the stricter
        // {Id=1,Name="x"} expectation is equivalent to only one of them. A greedy first-match would
        // let {Id=1} claim that element and then report the stricter expectation as missing;
        // maximum bipartite matching pairs them so a complete matching is found.
        var subject = new object[]
        {
            new { Id = 1, Name = "x" },
            new { Id = 1, Name = "y" }
        };
        var expected = new object[]
        {
            new { Id = 1 },
            new { Id = 1, Name = "x" }
        };

        subject.ShouldBeEquivalentTo(expected, new EquivalencyOptions { IgnoreOrder = true });
    }

    [Fact]
    public void ShouldPassWhenDifferingMemberIsIgnored()
    {
        var subject = new FakeObject { Id = 1, Name = "One" };
        var expected = new FakeObject { Id = 1, Name = "Completely different" };

        var options = new EquivalencyOptions { MembersToIgnore = { "Name" } };

        subject.ShouldBeEquivalentTo(expected, options);
    }

    [Fact]
    public void ShouldIgnoreMembersAnywhereInTheGraph()
    {
        var subject = new FakeObject { Id = 1, Child = new() { Id = 2, Name = "child" } };
        var expected = new FakeObject { Id = 1, Child = new() { Id = 2, Name = "different child" } };

        var options = new EquivalencyOptions { MembersToIgnore = { "Name" } };

        subject.ShouldBeEquivalentTo(expected, options);
    }

    [Fact]
    public void ShouldFailWhenNonIgnoredMemberDiffers()
    {
        var subject = new FakeObject { Id = 1, Name = "One" };
        var expected = new FakeObject { Id = 2, Name = "Completely different" };

        var options = new EquivalencyOptions { MembersToIgnore = { "Name" } };

        Verify.ShouldFail(() =>
            subject.ShouldBeEquivalentTo(expected, options, "Some additional context"));
    }

    [Fact]
    public void ShouldFailWhenIgnoringEveryMemberOfAType()
    {
        // Ignoring every member would make the assertion vacuously pass; the guard fails instead.
        var subject = new SingleMember { Name = "One" };
        var expected = new SingleMember { Name = "Two" };

        var options = new EquivalencyOptions { MembersToIgnore = { "Name" } };

        Verify.ShouldFail(() =>
            subject.ShouldBeEquivalentTo(expected, options, "Some additional context"));
    }

    public class SingleMember
    {
        public string? Name { get; set; }
    }
}
