using Shouldly.Tests.Strings;
using Shouldly.Tests.TestHelpers;
using Xunit;

namespace Shouldly.Tests.ShouldBe.ShouldBeOneOf
{
    public class ComparerScenario
    {
        [Fact]
        public void ComparerEqualsShouldPass()
        {
            var comparison1 = new ComparibleClass() { Property = "Elephant", IgnoredProperty = "Duck" };
            var comparison2 = new ComparibleClass[]
            {
                new ComparibleClass() { Property = "Elephant", IgnoredProperty = "Dog" },
                new ComparibleClass() { Property = "Lion", IgnoredProperty = "Spider" }
            };

            comparison1.ShouldBeOneOf(comparison2, new ComparibleClassComparer());
        }

        [Fact]
        public void ComparerNotEqualsShouldFail()
        {
            var comparison1 = new ComparibleClass() { Property = "Kangaroo", IgnoredProperty = "Whale" };
            var comparison2 = new ComparibleClass[]
            {
                new ComparibleClass() { Property = "Snake", IgnoredProperty = "Platypus" },
                new ComparibleClass() { Property = "Cat", IgnoredProperty = "Ant" }
            };

            Verify.ShouldFail(() =>
comparison1.ShouldBeOneOf(comparison2, new ComparibleClassComparer(), "Some additional context"),

errorWithSource:
@"comparison1
    should be one of
[Shouldly.Tests.TestHelpers.ComparibleClass (000000), Shouldly.Tests.TestHelpers.ComparibleClass (000000)]
    but was
Shouldly.Tests.TestHelpers.ComparibleClass (000000)

Additional Info:
    Some additional context",

errorWithoutSource:
@"Shouldly.Tests.TestHelpers.ComparibleClass (000000)
    should be one of
[Shouldly.Tests.TestHelpers.ComparibleClass (000000), Shouldly.Tests.TestHelpers.ComparibleClass (000000)]
    but was not

Additional Info:
    Some additional context");
        }
    }
}
