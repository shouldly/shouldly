using Shouldly.Tests.Strings;
using Shouldly.Tests.TestHelpers;
using Xunit;

namespace Shouldly.Tests.ShouldNotBe
{
    public class ComparerScenario
    {
        [Fact]
        public void ComparerNotEqualsShouldPass()
        {
            var comparison1 = new ComparibleClass() { Property = "Kangaroo", IgnoredProperty = "Whale" };
            var comparison2 = new ComparibleClass() { Property = "Cat", IgnoredProperty = "Ant" };

            comparison1.ShouldNotBe(comparison2, new ComparibleClassComparer());
        }

        [Fact]
        public void ComparerEqualsShouldFail()
        {
            var comparison1 = new ComparibleClass() { Property = "Elephant", IgnoredProperty = "Duck" };
            var comparison2 = new ComparibleClass() { Property = "Elephant", IgnoredProperty = "Dog" };
            
            Verify.ShouldFail(() =>
comparison1.ShouldNotBe(comparison2, new ComparibleClassComparer(), "Some additional context"),

errorWithSource:
@"comparison1
    should not be
Shouldly.Tests.TestHelpers.ComparibleClass (000000)
    but was

Additional Info:
    Some additional context",

errorWithoutSource:
@"Shouldly.Tests.TestHelpers.ComparibleClass (000000)
    should not be
Shouldly.Tests.TestHelpers.ComparibleClass (000000)
    but was

Additional Info:
    Some additional context");
        }
    }
}
