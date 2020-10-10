using Shouldly.Tests.Strings;
using Shouldly.Tests.TestHelpers;
using System.Linq;
using Xunit;

namespace Shouldly.Tests.ShouldBe.EnumerableType
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
            var comparison2 = new ComparibleClass[]
            {
                new ComparibleClass() { Property = "Elephant", IgnoredProperty = "Dog" },
                new ComparibleClass() { Property = "Lion", IgnoredProperty = "Spider" }
            };

            comparison1.ShouldBe(comparison2, new ComparibleClassComparer());
        }

        [Fact]
        public void ComparerEqualsIgnoreOrderShouldPass()
        {
            var comparison1 = new ComparibleClass[]
            {
                new ComparibleClass() { Property = "Elephant", IgnoredProperty = "Duck" },
                new ComparibleClass() { Property = "Lion", IgnoredProperty = "Whale" }
            };
            var comparison2 = new ComparibleClass[]
            {
                new ComparibleClass() { Property = "Lion", IgnoredProperty = "Spider" },
                new ComparibleClass() { Property = "Elephant", IgnoredProperty = "Dog" }
            };

            comparison1.ShouldBe(comparison2, new ComparibleClassComparer(), ignoreOrder: true);
        }

        [Fact]
        public void ComparerNotEqualsShouldFail()
        {
            var comparison1 = new ComparibleClass[]
            {
                new ComparibleClass() { Property = "Kangaroo", IgnoredProperty = "Whale" },
                new ComparibleClass() { Property = "Tiger", IgnoredProperty = "Salmon" }
            }.AsEnumerable();
            var comparison2 = new ComparibleClass[]
            {
                new ComparibleClass() { Property = "Snake", IgnoredProperty = "Platypus" },
                new ComparibleClass() { Property = "Cat", IgnoredProperty = "Ant" }
            }.AsEnumerable();

            Verify.ShouldFail(() =>
comparison1.ShouldBe(comparison2, new ComparibleClassComparer(), false, "Some additional context"),

errorWithSource:
@"comparison1
    should be
[Shouldly.Tests.TestHelpers.ComparibleClass (000000), Shouldly.Tests.TestHelpers.ComparibleClass (000000)]
    but was
[Shouldly.Tests.TestHelpers.ComparibleClass (000000), Shouldly.Tests.TestHelpers.ComparibleClass (000000)]
    difference
[*Shouldly.Tests.TestHelpers.ComparibleClass (000000)*, *Shouldly.Tests.TestHelpers.ComparibleClass (000000)*]

Additional Info:
    Some additional context",

errorWithoutSource:
@"[Shouldly.Tests.TestHelpers.ComparibleClass (000000), Shouldly.Tests.TestHelpers.ComparibleClass (000000)]
    should be
[Shouldly.Tests.TestHelpers.ComparibleClass (000000), Shouldly.Tests.TestHelpers.ComparibleClass (000000)]
    but was not
    difference
[*Shouldly.Tests.TestHelpers.ComparibleClass (000000)*, *Shouldly.Tests.TestHelpers.ComparibleClass (000000)*]

Additional Info:
    Some additional context");
        }
    }
}
