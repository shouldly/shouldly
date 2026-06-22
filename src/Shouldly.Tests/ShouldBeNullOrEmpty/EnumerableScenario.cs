namespace Shouldly.Tests.ShouldBeNullOrEmpty;

public class EnumerableScenario
{
    [Fact]
    public void PassesForNull()
    {
        IEnumerable<int>? actual = null;
        actual.ShouldBeNullOrEmpty();
    }

    [Fact]
    public void PassesForEmpty()
    {
        Array.Empty<int>().ShouldBeNullOrEmpty();
    }

    [Fact]
    public void FailsForNonEmpty()
    {
        Verify.ShouldFail(() =>
            new[] { 1, 2, 3 }.ShouldBeNullOrEmpty("Some additional context"));
    }

    // Adding the IEnumerable<T> overload must not make calls on a string ambiguous
    // (string is IEnumerable<char>); the more specific string overload still wins.
    [Fact]
    public void StringStillBindsToStringOverload()
    {
        "".ShouldBeNullOrEmpty();
    }
}
