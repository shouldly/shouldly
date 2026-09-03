namespace Shouldly.Tests.Dictionaries;

public class NonDictionaryKeyValueSequences
{
    private static IEnumerable<KeyValuePair<string, int>> PairList() =>
        new List<KeyValuePair<string, int>> { new("a", 1) };

    private static Dictionary<string, int> Dictionary() =>
        new() { ["a"] = 1 };

    [Fact]
    public void ShouldContainKeyAndValueReportsAWrongValue()
    {
        var exception = Should.Throw<ShouldAssertException>(() =>
            PairList().ShouldContainKeyAndValue("a", 2));

        exception.Message.ShouldContain("a");
        exception.Message.ShouldContain("but value was");
    }

    [Fact]
    public void ShouldContainKeyAndValueReportsAMissingKey()
    {
        var exception = Should.Throw<ShouldAssertException>(() =>
            PairList().ShouldContainKeyAndValue("z", 1));

        exception.Message.ShouldContain("but the key does not exist");
    }

    [Fact]
    public void ShouldNotContainValueForKeyReportsAPresentPair()
    {
        var exception = Should.Throw<ShouldAssertException>(() =>
            PairList().ShouldNotContainValueForKey("a", 1));

        exception.Message.ShouldContain("but does");
    }

    [Fact]
    public void ShouldNotContainValueForKeyReportsAMissingKey()
    {
        var exception = Should.Throw<ShouldAssertException>(() =>
            PairList().ShouldNotContainValueForKey("z", 1));

        exception.Message.ShouldContain("but the key does not exist");
    }

    [Fact]
    public void ArrayOfPairsReportsAWrongValue()
    {
        KeyValuePair<string, int>[] pairs = [new("a", 1)];

        Should.Throw<ShouldAssertException>(() =>
            pairs.ShouldContainKeyAndValue("a", 2));
    }

    [Fact]
    public void LinqProjectionOverADictionaryReportsAWrongValue()
    {
        var exception = Should.Throw<ShouldAssertException>(() =>
            Dictionary().Where(pair => true).ShouldContainKeyAndValue("a", 999));

        exception.Message.ShouldContain("but value was");
    }

    [Fact]
    public void LinqProjectionOverADictionaryReportsAPresentPair()
    {
        Should.Throw<ShouldAssertException>(() =>
            Dictionary().Select(pair => pair).ShouldNotContainValueForKey("a", 1));
    }

    [Fact]
    public void PassingAssertionsAreUnaffected()
    {
        PairList().ShouldContainKeyAndValue("a", 1);
        PairList().ShouldNotContainValueForKey("a", 2);
        PairList().ShouldContainKey("a");
        PairList().ShouldNotContainKey("z");
    }

    [Fact]
    public void CollectionValuesStillCompareStructurallyOverAPairSequence()
    {
        IEnumerable<KeyValuePair<string, int[]>> pairs =
            new List<KeyValuePair<string, int[]>> { new("k", [1, 2, 3]) };

        pairs.ShouldContainKeyAndValue("k", [1, 2, 3]);
        Should.Throw<ShouldAssertException>(() => pairs.ShouldContainKeyAndValue("k", [1, 2, 4]));
    }

    [Fact]
    public void NullValuesAreReportedOverAPairSequence()
    {
        IEnumerable<KeyValuePair<string, string?>> pairs =
            new List<KeyValuePair<string, string?>> { new("k", null) };

        var exception = Should.Throw<ShouldAssertException>(() =>
            pairs.ShouldContainKeyAndValue("k", "x"));

        exception.Message.ShouldContain("but value was");
    }
}
