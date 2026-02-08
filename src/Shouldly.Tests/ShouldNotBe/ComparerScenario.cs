namespace Shouldly.Tests.ShouldNotBe;

public class ComparerScenario
{
    [Fact]
    public void ComparerNotEqualsShouldPass()
    {
        var comparison1 = new ComparableClass { Property = "Kangaroo", IgnoredProperty = "Whale" };
        var comparison2 = new ComparableClass { Property = "Cat", IgnoredProperty = "Ant" };

        comparison1.ShouldNotBe(comparison2, new ComparableClassComparer());
    }

    [Fact]
    public void ComparerEqualsShouldFail()
    {
        var comparison1 = new ComparableClass { Property = "Elephant", IgnoredProperty = "Duck" };
        var comparison2 = new ComparableClass { Property = "Elephant", IgnoredProperty = "Dog" };

        Verify.ShouldFail(() =>
            comparison1.ShouldNotBe(comparison2, new ComparableClassComparer(), "Some additional context"));
    }
}