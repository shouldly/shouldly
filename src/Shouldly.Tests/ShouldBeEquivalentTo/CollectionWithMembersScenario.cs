using System.Collections;

namespace Shouldly.Tests.ShouldBeEquivalentTo;

// A collection type that declares its own public members is compared by its items and by those members (#1431).
public class CollectionWithMembersScenario
{
    [Fact]
    public void ShouldPassWhenItemsAndMembersMatch()
    {
        var subject = new PagedList(10, 1, 2);
        subject.ShouldBeEquivalentTo(new PagedList(10, 1, 2));
    }

    [Fact]
    public void ShouldFailWhenAMemberOfAListTypeDiffers()
    {
        var subject = new PagedList(10, 1, 2);
        Verify.ShouldFail(() =>
            subject.ShouldBeEquivalentTo(new PagedList(99, 1, 2), "Some additional context"));
    }

    [Fact]
    public void ShouldReportBothItemAndMemberDifferences()
    {
        var subject = new PagedList(10, 1, 2);
        Verify.ShouldFail(() =>
            subject.ShouldBeEquivalentTo(new PagedList(99, 1, 3), "Some additional context"));
    }

    [Fact]
    public void ShouldFailWhenTheKeyOfAGroupingDiffers()
    {
        var subject = new Group(new[] { 1, 2 }.GroupBy(_ => "odd").Single());
        Verify.ShouldFail(() =>
            subject.ShouldBeEquivalentTo(new Group(new[] { 1, 2 }.GroupBy(_ => "even").Single()), "Some additional context"));
    }

    [Fact]
    public void ShouldPassWhenGroupingsHaveTheSameKeyAndItems()
    {
        var subject = new[] { 1, 2 }.GroupBy(_ => "odd").Single();
        subject.ShouldBeEquivalentTo(new[] { 1, 2 }.GroupBy(_ => "odd").Single());
    }

    [Fact]
    public void ShouldFailWhenAMemberOfADictionaryTypeDiffers()
    {
        var subject = new Headers("v1") { ["a"] = 1 };
        Verify.ShouldFail(() =>
            subject.ShouldBeEquivalentTo(new Headers("v2") { ["a"] = 1 }, "Some additional context"));
    }

    [Fact]
    public void ShouldFailWhenAMemberOfASetTypeDiffers()
    {
        var subject = new TaggedSet("red", 1, 2);
        Verify.ShouldFail(() =>
            subject.ShouldBeEquivalentTo(new TaggedSet("blue", 2, 1), "Some additional context"));
    }

    [Fact]
    public void ShouldFailWhenAMemberOfANestedCollectionDiffers()
    {
        var subject = new Page(new PagedList(10, 1, 2));
        Verify.ShouldFail(() =>
            subject.ShouldBeEquivalentTo(new Page(new PagedList(99, 1, 2)), "Some additional context"));
    }

    [Fact]
    public void ShouldFailWhenTheActualCollectionLacksTheMember()
    {
        var subject = new List<int> { 1, 2 };
        Verify.ShouldFail(() =>
            subject.ShouldBeEquivalentTo(new PagedList(10, 1, 2), "Some additional context"));
    }

    [Fact]
    public void ShouldIgnoreMembersTheExpectationDoesNotHave()
    {
        var subject = new PagedList(10, 1, 2);
        subject.ShouldBeEquivalentTo(new List<int> { 1, 2 });
    }

    [Fact]
    public void ShouldPassWhenTheDifferingMemberIsIgnored()
    {
        var subject = new PagedList(10, 1, 2);
        subject.ShouldBeEquivalentTo(
            new PagedList(99, 1, 2),
            new EquivalencyOptions { MembersToIgnore = { nameof(PagedList.TotalCount) } });
    }

    [Fact]
    public void ShouldNotCompareTheMembersOfFrameworkCollections()
    {
        var subject = new List<int>(capacity: 10) { 1, 2 };
        subject.ShouldBeEquivalentTo(new List<int>(capacity: 99) { 1, 2 });
        new Dictionary<string, int>(StringComparer.Ordinal) { ["a"] = 1 }
            .ShouldBeEquivalentTo(new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase) { ["a"] = 1 });
    }

    [Fact]
    public void ShouldNotCompareCollectionInterfaceMembersACustomCollectionImplements()
    {
        var subject = new CustomCollection(1, 2);
        subject.ShouldBeEquivalentTo(new CustomCollection(1, 2));
    }

    private sealed class PagedList(int total, params int[] items) : List<int>(items)
    {
        public int TotalCount { get; } = total;
    }

    private sealed class Headers(string version) : Dictionary<string, int>
    {
        public string Version { get; } = version;
    }

    private sealed class TaggedSet(string tag, params int[] items) : HashSet<int>(items)
    {
        public string Tag { get; } = tag;
    }

    private sealed record Page(PagedList Items);

    private sealed record Group(IGrouping<string, int> Items);

    // Implements ICollection itself, so its SyncRoot (a fresh object per instance) and the other members it
    // implements are collection plumbing, not state.
    private sealed class CustomCollection(params int[] items) : ICollection
    {
        public int Count => items.Length;

        public bool IsSynchronized => false;

        public object SyncRoot { get; } = new();

        public void CopyTo(Array array, int index) => items.CopyTo(array, index);

        public IEnumerator GetEnumerator() => items.GetEnumerator();
    }
}
