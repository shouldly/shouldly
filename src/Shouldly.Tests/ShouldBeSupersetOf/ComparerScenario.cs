namespace Shouldly.Tests.ShouldBe.ShouldBeSupersetOf;

public class ComparerScenario
{
    [Fact]
    public void ComparerEqualsShouldPass()
    {
        var comparison1 = new[]
        {
            new ComparableClass { Property = "Elephant", IgnoredProperty = "Dog" },
            new ComparableClass { Property = "Lion", IgnoredProperty = "Spider" }
        };
        var comparison2 = new[]
        {
            new ComparableClass { Property = "Elephant", IgnoredProperty = "Duck" }
        };

        comparison1.ShouldBeSupersetOf(comparison2, new ComparableClassComparer());
    }

    [Fact]
    public void ComparerNotEqualsShouldFail()
    {
        var comparison1 = new[]
        {
            new ComparableClass { Property = "Snake", IgnoredProperty = "Platypus" },
            new ComparableClass { Property = "Cat", IgnoredProperty = "Ant" }
        };
        var comparison2 = new[]
        {
            new ComparableClass { Property = "Kangaroo", IgnoredProperty = "Whale" }
        };

        Verify.ShouldFail(() =>
                comparison1.ShouldBeSupersetOf(comparison2, new ComparableClassComparer(), "Some additional context"),

            errorWithSource:
            """
            comparison1
                should be superset of
            [Shouldly.Tests.TestHelpers.ComparableClass (000000)]
                but
            [Shouldly.Tests.TestHelpers.ComparableClass (000000)]
                is outside superset

            Additional Info:
                Some additional context
            """,

            errorWithoutSource:
            """
            [Shouldly.Tests.TestHelpers.ComparableClass (000000), Shouldly.Tests.TestHelpers.ComparableClass (000000)]
                should be superset of
            [Shouldly.Tests.TestHelpers.ComparableClass (000000)]
                but
            [Shouldly.Tests.TestHelpers.ComparableClass (000000)]
                is outside superset

            Additional Info:
                Some additional context
            """);
    }
}