namespace Shouldly.Tests.ShouldBeEquivalentTo;

public class SetScenario
{
    [Fact]
    public void ShouldPassWhenSetsMatch()
    {
        var subject = new HashSet<int> { 1, 2, 3 };

        subject.ShouldBeEquivalentTo(new HashSet<int> { 1, 2, 3 });
    }

    [Fact]
    public void ShouldPassWhenSetsMatchInDifferentOrder()
    {
        var subject = new HashSet<int> { 1, 2, 3 };

        subject.ShouldBeEquivalentTo(new HashSet<int> { 3, 2, 1 });
    }

    [Fact]
    public void ShouldPassForLargeSetsInDifferentOrder()
    {
        // Exercises the greedy fast path of the order-insensitive matcher: every element matches,
        // so no augmenting-path fallback is needed even at size.
        var subject = new HashSet<int>(Enumerable.Range(0, 2000));
        var expected = new HashSet<int>(Enumerable.Range(0, 2000).Reverse());

        subject.ShouldBeEquivalentTo(expected);
    }

    [Fact]
    public void ShouldFailWhenSetIsTooShort()
    {
        var subject = new HashSet<int> { 1, 2, 3, 4 };

        Verify.ShouldFail(() =>
            subject.ShouldBeEquivalentTo(new HashSet<int> { 1, 2, 3, 4, 5 }, "Some additional context"));
    }

    [Fact]
    public void ShouldFailWhenSetIsTooLong()
    {
        var subject = new HashSet<int> { 1, 2, 3, 4, 5 };

        Verify.ShouldFail(() =>
            subject.ShouldBeEquivalentTo(new HashSet<int> { 1, 2, 3, 4 }, "Some additional context"));
    }

    [Fact]
    public void ShouldFailWhenSetsDoNotMatch()
    {
        var subject = new HashSet<int> { 1, 2, 6, 4, 3 };

        Verify.ShouldFail(() =>
            subject.ShouldBeEquivalentTo(new HashSet<int> { 1, 2, 3, 4, 5 }, "Some additional context"));
    }

    // A HashSet has no order of its own for a list's to be compared with, whichever side it is on.
    [Fact]
    public void ShouldCompareASetWithAListWithoutRegardToOrder()
    {
        new HashSet<int> { 3, 2, 1 }.ShouldBeEquivalentTo(new[] { 1, 2, 3 });
        new[] { 3, 2, 1 }.ShouldBeEquivalentTo(new HashSet<int> { 1, 2, 3 });
        new Holder { Items = new HashSet<int> { 3, 2, 1 } }.ShouldBeEquivalentTo(new Holder { Items = [1, 2, 3] });

        Should.Throw<ShouldAssertException>(() =>
            new HashSet<int> { 3, 2, 1 }.ShouldBeEquivalentTo(new[] { 1, 2, 4 }));
        Should.Throw<ShouldAssertException>(() =>
            new HashSet<int> { 2, 1 }.ShouldBeEquivalentTo(new[] { 1, 1, 2 }));
    }

    // A SortedSet does have one, which a test may be about. Against another set, it isn't compared.
    [Fact]
    public void ShouldCompareASortedSetWithAListInOrder()
    {
        var descending = new SortedSet<int>(Comparer<int>.Create((x, y) => y.CompareTo(x))) { 1, 2, 3 };

        descending.ShouldBeEquivalentTo(new[] { 3, 2, 1 });
        new[] { 3, 2, 1 }.ShouldBeEquivalentTo(descending);
        descending.ShouldBeEquivalentTo(new HashSet<int> { 1, 2, 3 });
        new HashSet<int> { 1, 2, 3 }.ShouldBeEquivalentTo(descending);

        Should.Throw<ShouldAssertException>(() => descending.ShouldBeEquivalentTo(new[] { 1, 2, 3 }));
        Should.Throw<ShouldAssertException>(() => new[] { 1, 2, 3 }.ShouldBeEquivalentTo(descending));
    }

    [Fact]
    public void ShouldFailWhenASetAndAListDoNotMatch()
    {
        var subject = new HashSet<int> { 1, 2, 3 };

        Verify.ShouldFail(() =>
            subject.ShouldBeEquivalentTo(new[] { 1, 2, 4 }, "Some additional context"));
    }

    private class Holder
    {
        public IEnumerable<int>? Items { get; set; }
    }
}
