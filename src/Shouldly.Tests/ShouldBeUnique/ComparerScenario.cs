namespace Shouldly.Tests.ShouldBe.ShouldBeUnique;

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

        comparison1.ShouldBeUnique(new ComparableClassComparer());
    }

    [Fact]
    public void ComparerNotEqualsShouldFail()
    {
        var comparison1 = new[]
        {
            new ComparableClass { Property = "Kangaroo", IgnoredProperty = "Whale" },
            new ComparableClass { Property = "Kangaroo", IgnoredProperty = "Salmon" }
        };

        Verify.ShouldFail(() =>
            comparison1.ShouldBeUnique(new ComparableClassComparer(), "Some additional context"));
    }
}