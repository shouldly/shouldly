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
        var comparison2 = new[]
        {
            new ComparableClass { Property = "Elephant", IgnoredProperty = "Dog" },
            new ComparableClass { Property = "Lion", IgnoredProperty = "Spider" }
        };

        comparison1.ShouldBe(comparison2, new ComparableClassComparer());
    }

    [Fact]
    public void ComparerEqualsIgnoreOrderShouldPass()
    {
        var comparison1 = new[]
        {
            new ComparableClass { Property = "Elephant", IgnoredProperty = "Duck" },
            new ComparableClass { Property = "Lion", IgnoredProperty = "Whale" }
        };
        var comparison2 = new[]
        {
            new ComparableClass { Property = "Lion", IgnoredProperty = "Spider" },
            new ComparableClass { Property = "Elephant", IgnoredProperty = "Dog" }
        };

        comparison1.ShouldBe(comparison2, new ComparableClassComparer(), ignoreOrder: true);
    }

    [Fact]
    public void ComparerNotEqualsShouldFail()
    {
        var comparison1 = new[]
        {
            new ComparableClass { Property = "Kangaroo", IgnoredProperty = "Whale" },
            new ComparableClass { Property = "Tiger", IgnoredProperty = "Salmon" }
        }.AsEnumerable();
        var comparison2 = new[]
        {
            new ComparableClass { Property = "Snake", IgnoredProperty = "Platypus" },
            new ComparableClass { Property = "Cat", IgnoredProperty = "Ant" }
        }.AsEnumerable();

        Verify.ShouldFail(() =>
                comparison1.ShouldBe(comparison2, new ComparableClassComparer(), false, "Some additional context"),

            errorWithSource:
            @"comparison1
    should be
[Shouldly.Tests.TestHelpers.ComparableClass (000000), Shouldly.Tests.TestHelpers.ComparableClass (000000)]
    but was
[Shouldly.Tests.TestHelpers.ComparableClass (000000), Shouldly.Tests.TestHelpers.ComparableClass (000000)]
    difference
[*Shouldly.Tests.TestHelpers.ComparableClass (000000)*, *Shouldly.Tests.TestHelpers.ComparableClass (000000)*]

Additional Info:
    Some additional context",

            errorWithoutSource:
            @"[Shouldly.Tests.TestHelpers.ComparableClass (000000), Shouldly.Tests.TestHelpers.ComparableClass (000000)]
    should be
[Shouldly.Tests.TestHelpers.ComparableClass (000000), Shouldly.Tests.TestHelpers.ComparableClass (000000)]
    but was not
    difference
[*Shouldly.Tests.TestHelpers.ComparableClass (000000)*, *Shouldly.Tests.TestHelpers.ComparableClass (000000)*]

Additional Info:
    Some additional context");
    }
}