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
            var comparison1 = new ComparableClass { Property = "Kangaroo", IgnoredProperty = "Whale" };
            var comparison2 = new ComparableClass { Property = "Cat", IgnoredProperty = "Ant" };

            comparison1.ShouldNotBe(comparison2, new ComparableClassComparer());
        }

        [Fact]
        public void ComparerEqualsShouldFail()
        {
            var comparison1 = new ComparableClass { Property = "Elephant", IgnoredProperty = "Duck" };
            var comparison2 = new ComparableClass { Property = "Elephant", IgnoredProperty = "Dog" };

            Verify.ShouldFail(() =>
comparison1.ShouldNotBe(comparison2, new ComparableClassComparer(), "Some additional context"),

errorWithSource:
@"comparison1
    should not be
Shouldly.Tests.TestHelpers.ComparableClass (000000)
    but was

Additional Info:
    Some additional context",

errorWithoutSource:
@"Shouldly.Tests.TestHelpers.ComparableClass (000000)
    should not be
Shouldly.Tests.TestHelpers.ComparableClass (000000)
    but was

Additional Info:
    Some additional context");
        }
    }
}
