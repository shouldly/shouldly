using Shouldly.Tests.Strings;
using Shouldly.Tests.TestHelpers;
using Xunit;

namespace Shouldly.Tests.ShouldBe.ShouldContain
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
            var comparison2 = new ComparibleClass() { Property = "Elephant", IgnoredProperty = "Dog" };

            comparison1.ShouldContain(comparison2, new ComparibleClassComparer());
        }

        [Fact]
        public void ComparerNotEqualsShouldFail()
        {
            var comparison1 = new ComparibleClass[]
            {
                new ComparibleClass() { Property = "Kangaroo", IgnoredProperty = "Whale" },
                new ComparibleClass() { Property = "Tiger", IgnoredProperty = "Salmon" }
            };
            var comparison2 = new ComparibleClass() { Property = "Snake", IgnoredProperty = "Platypus" };

            Verify.ShouldFail(() =>
comparison1.ShouldContain(comparison2, new ComparibleClassComparer(), "Some additional context"),

errorWithSource:
@"comparison1
    should contain
Shouldly.Tests.TestHelpers.ComparibleClass (000000)
    but was actually
[Shouldly.Tests.TestHelpers.ComparibleClass (000000), Shouldly.Tests.TestHelpers.ComparibleClass (000000)]

Additional Info:
    Some additional context",

errorWithoutSource:
@"[Shouldly.Tests.TestHelpers.ComparibleClass (000000), Shouldly.Tests.TestHelpers.ComparibleClass (000000)]
    should contain
Shouldly.Tests.TestHelpers.ComparibleClass (000000)
    but did not

Additional Info:
    Some additional context");
        }
    }
}
