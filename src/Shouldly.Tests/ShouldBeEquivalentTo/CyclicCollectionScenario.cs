namespace Shouldly.Tests.ShouldBeEquivalentTo;

/// <summary>
/// Regression coverage for cycles that close through a collection, array, set or dictionary node.
/// </summary>
public class CyclicCollectionScenario
{
    [Fact]
    public void ShouldPassWhenListContainsItself()
    {
        var subject = new List<object> { 1 };
        subject.Add(subject);

        var expected = new List<object> { 1 };
        expected.Add(expected);

        subject.ShouldBeEquivalentTo(expected);
    }

    [Fact]
    public void ShouldDetectDifferencesAlongsideSelfReferentialList()
    {
        var subject = new List<object> { 1 };
        subject.Add(subject);

        var expected = new List<object> { 2 };
        expected.Add(expected);

        // The cycle terminates, but the differing sibling element is still reported.
        Should.Throw<ShouldAssertException>(() => subject.ShouldBeEquivalentTo(expected));
    }

    [Fact]
    public void ShouldPassWhenTwoListsFormMutualCycle()
    {
        var subjectA = new List<object>();
        var subjectB = new List<object> { subjectA };
        subjectA.Add(subjectB);

        var expectedA = new List<object>();
        var expectedB = new List<object> { expectedA };
        expectedA.Add(expectedB);

        subjectA.ShouldBeEquivalentTo(expectedA);
    }

    [Fact]
    public void ShouldPassWhenArrayContainsItself()
    {
        var subject = new object[1];
        subject[0] = subject;

        var expected = new object[1];
        expected[0] = expected;

        subject.ShouldBeEquivalentTo(expected);
    }

    [Fact]
    public void ShouldPassWhenSetContainsItself()
    {
        var subject = new HashSet<object> { 1 };
        subject.Add(subject);

        var expected = new HashSet<object> { 1 };
        expected.Add(expected);

        subject.ShouldBeEquivalentTo(expected);
    }

    [Fact]
    public void ShouldPassWhenDictionaryValueContainsItself()
    {
        var subject = new Dictionary<string, object> { ["value"] = 1 };
        subject["self"] = subject;

        var expected = new Dictionary<string, object> { ["value"] = 1 };
        expected["self"] = expected;

        subject.ShouldBeEquivalentTo(expected);
    }

    [Fact]
    public void ShouldDetectDifferencesAlongsideSelfReferentialDictionary()
    {
        var subject = new Dictionary<string, object> { ["value"] = 1 };
        subject["self"] = subject;

        var expected = new Dictionary<string, object> { ["value"] = 2 };
        expected["self"] = expected;

        Should.Throw<ShouldAssertException>(() => subject.ShouldBeEquivalentTo(expected));
    }
}
