namespace Shouldly.Tests.ShouldBe
{
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
comparison1.ShouldBe(comparison2, new ComparableClassComparer(), "Some additional context"),

errorWithSource:
@"comparison1
    should be
Shouldly.Tests.TestHelpers.ComparableClass (000000)
    but was
Shouldly.Tests.TestHelpers.ComparableClass (000000)

Additional Info:
    Some additional context",

errorWithoutSource:
@"Shouldly.Tests.TestHelpers.ComparableClass (000000)
    should be
Shouldly.Tests.TestHelpers.ComparableClass (000000)
    but was not

Additional Info:
    Some additional context");
        }
    }
}
