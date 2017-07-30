using Shouldly.Tests.Strings;
using Shouldly.Tests.TestHelpers;
using Xunit;

namespace Shouldly.Tests.ShouldBe
{
    public class ComparerScenario
    {
        [Fact]
        public void ComparerEqualsShouldPass()
        {
            var comparison1 = new ComparibleClass() { Property = "Elephant", IgnoredProperty = "Duck" };
            var comparison2 = new ComparibleClass() { Property = "Elephant", IgnoredProperty = "Dog" };

            comparison1.ShouldBe(comparison2, new ComparibleClassComparer());
        }

        [Fact]
        public void ComparerNotEqualsShouldFail()
        {
            var comparison1 = new ComparibleClass() { Property = "Kangaroo", IgnoredProperty = "Whale" };
            var comparison2 = new ComparibleClass() { Property = "Cat", IgnoredProperty = "Ant" };

            Verify.ShouldFail(() =>
comparison1.ShouldBe(comparison2, new ComparibleClassComparer(), "Some additional context"),

errorWithSource:
@"comparison1
    should be
Shouldly.Tests.TestHelpers.ComparibleClass (000000)
    but was
Shouldly.Tests.TestHelpers.ComparibleClass (000000)

Additional Info:
    Some additional context",

errorWithoutSource:
@"Shouldly.Tests.TestHelpers.ComparibleClass (000000)
    should be
Shouldly.Tests.TestHelpers.ComparibleClass (000000)
    but was not

Additional Info:
    Some additional context");
        }
    }
}
