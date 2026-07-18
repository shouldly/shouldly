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
}
