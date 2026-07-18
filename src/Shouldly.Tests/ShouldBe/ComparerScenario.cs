namespace Shouldly.Tests.ShouldBe;

public class ComparerScenario
{
    [Fact]
    public void ComparerEqualsShouldPass()
    {
        var comparison1 = new ComparableClass { Property = "Elephant", IgnoredProperty = "Duck" };
        var comparison2 = new ComparableClass { Property = "Elephant", IgnoredProperty = "Dog" };

        comparison1.ShouldBe(comparison2, new ComparableClassComparer());
    }

    [Fact]
    public void ComparerNotEqualsShouldFail()
    {
        var comparison1 = new ComparableClass { Property = "Kangaroo", IgnoredProperty = "Whale" };
        var comparison2 = new ComparableClass { Property = "Cat", IgnoredProperty = "Ant" };

        Verify.ShouldFail(() =>
            comparison1.ShouldBe(comparison2, new ComparableClassComparer(), "Some additional context"));
    }
}