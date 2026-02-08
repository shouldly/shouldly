namespace Shouldly.Tests.ShouldBe.ShouldNotContain;

public class ComparerScenario
{
    [Fact]
    public void ComparerEqualsShouldPass()
    {
        var comparison1 = new[]
        {
            new ComparableClass { Property = "Elephant", IgnoredProperty = "Duck" },
            new ComparableClass { Property = "Lion", IgnoredProperty = "Whale" }
        };
        var comparison2 = new ComparableClass { Property = "Kangaroo", IgnoredProperty = "Dog" };

        comparison1.ShouldNotContain(comparison2, new ComparableClassComparer());
    }

    [Fact]
    public void ComparerNotEqualsShouldFail()
    {
        var comparison1 = new[]
        {
            new ComparableClass { Property = "Snake", IgnoredProperty = "Whale" },
            new ComparableClass { Property = "Tiger", IgnoredProperty = "Salmon" }
        };
        var comparison2 = new ComparableClass { Property = "Snake", IgnoredProperty = "Platypus" };

        Verify.ShouldFail(() =>
            comparison1.ShouldNotContain(comparison2, new ComparableClassComparer(), "Some additional context"));
    }
}