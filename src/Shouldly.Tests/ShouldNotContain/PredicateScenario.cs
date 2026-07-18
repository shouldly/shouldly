namespace Shouldly.Tests.ShouldNotContain;

public class PredicateScenario
{
    [Fact]
    public void PredicateScenarioShouldFail()
    {
        Verify.ShouldFail(() =>
            new[] { 1, 2, 3 }.ShouldNotContain(i => i < 4, "Some additional context"));
    }

    [Fact]
    public void ListsOnlyTheMatchingElements()
    {
        IEnumerable<int> values = new List<int> { 1, 2, 3, 4, 2 };
        Verify.ShouldFail(() =>
            values.ShouldNotContain(i => i == 2, "Some additional context"));
    }

    [Fact]
    public void ListsTheMatchingStrings()
    {
        var testResults = new[] { "pass", "error: file not found", "all good", "error: timeout" };
        Verify.ShouldFail(() =>
            testResults.ShouldNotContain(result => result.Contains("error"), "Some additional context"));
    }

    [Fact]
    public void ShouldPass()
    {
        new[] { 1, 2, 3 }.ShouldNotContain(i => i > 3);
    }
}