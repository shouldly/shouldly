namespace Shouldly.Tests.ShouldBe.ShouldBeSubsetOf;

public class ComparerScenario
{
    [Fact]
    public void ComparerEqualsShouldPass()
    {
        var comparison1 = new[]
        {
            new ComparableClass { Property = "Elephant", IgnoredProperty = "Duck" }
        };
        var comparison2 = new[]
        {
            new ComparableClass { Property = "Elephant", IgnoredProperty = "Dog" },
            new ComparableClass { Property = "Lion", IgnoredProperty = "Spider" }
        };

        comparison1.ShouldBeSubsetOf(comparison2, new ComparableClassComparer());
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
            comparison1.ShouldBeSubsetOf(comparison2, new ComparableClassComparer(), "Some additional context"));
    }
}