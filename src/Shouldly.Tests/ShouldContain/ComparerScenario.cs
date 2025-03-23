namespace Shouldly.Tests.ShouldBe.ShouldContain;

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
        var comparison2 = new ComparableClass { Property = "Elephant", IgnoredProperty = "Dog" };

        comparison1.ShouldContain(comparison2, new ComparableClassComparer());
    }

    [Fact]
    public void ComparerNotEqualsShouldFail()
    {
        var comparison1 = new[]
        {
            new ComparableClass { Property = "Kangaroo", IgnoredProperty = "Whale" },
            new ComparableClass { Property = "Tiger", IgnoredProperty = "Salmon" }
        };
        var comparison2 = new ComparableClass { Property = "Snake", IgnoredProperty = "Platypus" };

        Verify.ShouldFail(() =>
                comparison1.ShouldContain(comparison2, new ComparableClassComparer(), "Some additional context"),

            errorWithSource:
            """
            comparison1
                should contain
            Shouldly.Tests.TestHelpers.ComparableClass (000000)
                but was actually
            [Shouldly.Tests.TestHelpers.ComparableClass (000000), Shouldly.Tests.TestHelpers.ComparableClass (000000)]

            Additional Info:
                Some additional context
            """,

            errorWithoutSource:
            """
            [Shouldly.Tests.TestHelpers.ComparableClass (000000), Shouldly.Tests.TestHelpers.ComparableClass (000000)]
                should contain
            Shouldly.Tests.TestHelpers.ComparableClass (000000)
                but did not

            Additional Info:
                Some additional context
            """);
    }
}