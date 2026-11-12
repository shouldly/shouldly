namespace Shouldly.Tests.ShouldBeEquivalentTo;

public class DictionaryScenario
{
    [Fact]
    public void ShouldPassWhenDictionariesMatch()
    {
        var subject = new Dictionary<string, int> { ["a"] = 1, ["b"] = 2 };
        var expected = new Dictionary<string, int> { ["a"] = 1, ["b"] = 2 };

        subject.ShouldBeEquivalentTo(expected);
    }

    [Fact]
    public void ShouldPassWhenDictionariesMatchInDifferentInsertionOrder()
    {
        var subject = new Dictionary<string, int> { ["a"] = 1, ["b"] = 2 };
        var expected = new Dictionary<string, int> { ["b"] = 2, ["a"] = 1 };

        subject.ShouldBeEquivalentTo(expected);
    }

    [Fact]
    public void ShouldPassWhenDictionaryValuesAreComplex()
    {
        var subject = new Dictionary<string, FakeObject> { ["a"] = new() { Id = 1, Name = "One" } };
        var expected = new Dictionary<string, FakeObject> { ["a"] = new() { Id = 1, Name = "One" } };

        subject.ShouldBeEquivalentTo(expected);
    }

    [Fact]
    public void ShouldPassWhenDictionaryValuesAreCollections()
    {
        var subject = new Dictionary<string, List<int>> { ["a"] = [1, 2] };
        var expected = new Dictionary<string, List<int>> { ["a"] = [1, 2] };

        subject.ShouldBeEquivalentTo(expected);
    }

    [Fact]
    public void ShouldFailWhenDictionaryIsTooShort()
    {
        var subject = new Dictionary<int, int>
        {
            [1] = 2,
            [2] = 3,
            [3] = 5
        };
        var expected = new Dictionary<int, int>
        {
            [1] = 2,
            [2] = 3,
            [3] = 5,
            [4] = 8
        };

        Verify.ShouldFail(() =>
            subject.ShouldBeEquivalentTo(expected, "Some additional context"));
    }

    [Fact]
    public void ShouldFailWhenDictionaryIsTooLong()
    {
        var subject = new Dictionary<int, int>
        {
            [1] = 2,
            [2] = 3,
            [3] = 5,
            [4] = 8,
            [5] = 13
        };
        var expected = new Dictionary<int, int>
        {
            [1] = 2,
            [2] = 3,
            [3] = 5,
            [4] = 8
        };

        Verify.ShouldFail(() =>
            subject.ShouldBeEquivalentTo(expected, "Some additional context"));
    }

    [Fact]
    public void ShouldFailWhenDictionaryValueDiffers()
    {
        var subject = new Dictionary<string, int> { ["a"] = 1, ["b"] = 2 };
        var expected = new Dictionary<string, int> { ["a"] = 1, ["b"] = 3 };

        Verify.ShouldFail(() =>
            subject.ShouldBeEquivalentTo(expected, "Some additional context"));
    }

    // A dictionary and a list of its entries: the entries are matched in any order, each by its key and its value.
    [Fact]
    public void ShouldCompareADictionaryWithAListOfItsEntriesWithoutRegardToOrder()
    {
        var dictionary = new Dictionary<string, int> { ["a"] = 1, ["b"] = 2 };
        var entries = new List<KeyValuePair<string, int>> { new("b", 2), new("a", 1) };

        dictionary.ShouldBeEquivalentTo(entries);
        entries.ShouldBeEquivalentTo(dictionary);

        Should.Throw<ShouldAssertException>(() =>
            dictionary.ShouldBeEquivalentTo(new List<KeyValuePair<string, int>> { new("b", 2), new("a", 9) }));
        Should.Throw<ShouldAssertException>(() =>
            new List<KeyValuePair<string, int>> { new("b", 2), new("a", 9) }.ShouldBeEquivalentTo(dictionary));
    }

    [Fact]
    public void ShouldCompareASortedDictionaryWithAListOfItsEntriesInOrder()
    {
        var sorted = new SortedDictionary<string, int> { ["b"] = 2, ["a"] = 1 };

        sorted.ShouldBeEquivalentTo(new List<KeyValuePair<string, int>> { new("a", 1), new("b", 2) });
        sorted.ShouldBeEquivalentTo(new Dictionary<string, int> { ["b"] = 2, ["a"] = 1 });
        Should.Throw<ShouldAssertException>(() =>
            sorted.ShouldBeEquivalentTo(new List<KeyValuePair<string, int>> { new("b", 2), new("a", 1) }));
    }
}
