using Shouldly.Tests.Strings;
using Shouldly.Tests.TestHelpers;
using Xunit;

namespace Shouldly.Tests.ShouldBe.ShouldNotBeOneOf
{
    public class ComparerScenario
    {
        [Fact]
        public void ComparerEqualsShouldPass()
        {
            var comparison1 = new ComparableClass { Property = "Tiger", IgnoredProperty = "Duck" };
            var comparison2 = new[]
            {
                new ComparableClass { Property = "Elephant", IgnoredProperty = "Dog" },
                new ComparableClass { Property = "Lion", IgnoredProperty = "Spider" }
            };

            comparison1.ShouldNotBeOneOf(comparison2, new ComparableClassComparer());
        }

        [Fact]
        public void ComparerNotEqualsShouldFail()
        {
            var comparison1 = new ComparableClass { Property = "Snake", IgnoredProperty = "Whale" };
            var comparison2 = new[]
            {
                new ComparableClass { Property = "Snake", IgnoredProperty = "Platypus" },
                new ComparableClass { Property = "Cat", IgnoredProperty = "Ant" }
            };

            Verify.ShouldFail(() =>
comparison1.ShouldNotBeOneOf(comparison2, new ComparableClassComparer(), "Some additional context"),

errorWithSource:
@"comparison1
    should not be one of
[Shouldly.Tests.TestHelpers.ComparableClass (000000), Shouldly.Tests.TestHelpers.ComparableClass (000000)]
    but was
Shouldly.Tests.TestHelpers.ComparableClass (000000)

Additional Info:
    Some additional context",

errorWithoutSource:
@"Shouldly.Tests.TestHelpers.ComparableClass (000000)
    should not be one of
[Shouldly.Tests.TestHelpers.ComparableClass (000000), Shouldly.Tests.TestHelpers.ComparableClass (000000)]
    but was

Additional Info:
    Some additional context");
        }
    }
}
