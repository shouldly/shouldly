namespace Shouldly.Tests.ShouldBeSupersetOf;

public class ComparerScenario
{
    [Fact]
    public void ShouldPassWithComparer()
    {
        new[] { "A", "b", "C" }.ShouldBeSupersetOf(["a", "B"], StringComparer.OrdinalIgnoreCase);
    }

    [Fact]
    public void ComparerNotEqualsShouldFail()
    {
        var comparison1 = new[]
        {
            new ComparableClass { Property = "Kangaroo", IgnoredProperty = "Whale" }
        };
        var comparison2 = new[]
        {
            new ComparableClass { Property = "Snake", IgnoredProperty = "Platypus" },
            new ComparableClass { Property = "Cat", IgnoredProperty = "Ant" }
        };

        Verify.ShouldFail(() =>
            comparison1.ShouldBeSupersetOf(comparison2, new ComparableClassComparer(), "Some additional context"));
    }
}
