using Shouldly.Tests.Strings;
using Shouldly.Tests.TestHelpers;
using Xunit;

namespace Shouldly.Tests.ShouldBe.ShouldNotContain
{
    public class ComparerScenario
    {
        [Fact]
        public void ComparerEqualsShouldPass()
        {
            var comparison1 = new ComparibleClass[]
            {
                new ComparibleClass() { Property = "Elephant", IgnoredProperty = "Duck" },
                new ComparibleClass() { Property = "Lion", IgnoredProperty = "Whale" }
            };
            var comparison2 = new ComparibleClass() { Property = "Kangaroo", IgnoredProperty = "Dog" };

            comparison1.ShouldNotContain(comparison2, new ComparibleClassComparer());
        }

        [Fact]
        public void ComparerNotEqualsShouldFail()
        {
            var comparison1 = new ComparibleClass[]
            {
                new ComparibleClass() { Property = "Snake", IgnoredProperty = "Whale" },
                new ComparibleClass() { Property = "Tiger", IgnoredProperty = "Salmon" }
            };
            var comparison2 = new ComparibleClass() { Property = "Snake", IgnoredProperty = "Platypus" };

            Verify.ShouldFail(() =>
comparison1.ShouldNotContain(comparison2, new ComparibleClassComparer(), "Some additional context"),

errorWithSource:
@"comparison1
    should not contain
Shouldly.Tests.TestHelpers.ComparibleClass (000000)
    but was actually
[Shouldly.Tests.TestHelpers.ComparibleClass (000000), Shouldly.Tests.TestHelpers.ComparibleClass (000000)]

Additional Info:
    Some additional context",

errorWithoutSource:
@"[Shouldly.Tests.TestHelpers.ComparibleClass (000000), Shouldly.Tests.TestHelpers.ComparibleClass (000000)]
    should not contain
Shouldly.Tests.TestHelpers.ComparibleClass (000000)
    but did

Additional Info:
    Some additional context");
        }
    }
}
