using System.Collections;

namespace Shouldly.Tests.ShouldBeEquivalentTo;

// A type that derives from a .NET collection class and adds public members is compared by its items and by those
// members, as is a grouping by its key (#1431).
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
    public void ShouldNotCompareCollectionInterfaceMembersADerivedCollectionImplements()
    {
        var subject = new SyncedList(1, 2);
        subject.ShouldBeEquivalentTo(new SyncedList(1, 2));
    }

    [Fact]
    public void ShouldCompareACollectionThatImplementsTheInterfacesItselfByItsItemsAlone()
    {
        new PagedBuffer(total: 10, capacity: 16, 1, 2).ShouldBeEquivalentTo(new PagedBuffer(total: 99, capacity: 9, 1, 2));
        new PagedBuffer(total: 10, capacity: 16).ShouldBeEquivalentTo(new PagedBuffer(total: 10, capacity: 9));
    }

    [Fact]
    public void ShouldIgnoreTheContainerTypeOfACollectionThatImplementsTheInterfacesItself()
    {
        var subject = new[] { 1, 2 };
        subject.ShouldBeEquivalentTo(new PagedBuffer(total: 10, capacity: 16, 1, 2));
    }

    [Fact]
    public void ShouldFindTheMemberOnAnActualCollectionThatImplementsTheInterfacesItself()
    {
        var subject = new PagedBuffer(total: 10, capacity: 16, 1, 2);
        subject.ShouldBeEquivalentTo(new PagedList(10, 1, 2));
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

    // Its SyncRoot (a fresh object per instance) is a member of ICollection, so it is collection plumbing, not
    // state, even though this type declares it.
    private sealed class SyncedList(params int[] items) : List<int>(items), ICollection
    {
        public object SyncRoot { get; } = new();
    }

    // Implements IEnumerable<T> itself, so nothing tells its state (TotalCount) apart from how it holds its items
    // (Capacity) or from views of them (First, which throws when there are none). None of them is compared.
    private sealed class PagedBuffer(int total, int capacity, params int[] items) : IEnumerable<int>
    {
        public int TotalCount { get; } = total;

        public int Capacity { get; } = capacity;

        public int First => items.Length > 0 ? items[0] : throw new InvalidOperationException("The buffer is empty.");

        public IEnumerator<int> GetEnumerator() => ((IEnumerable<int>)items).GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
