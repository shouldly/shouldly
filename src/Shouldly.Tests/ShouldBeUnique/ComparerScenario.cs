using Shouldly.Tests.Strings;
using Shouldly.Tests.TestHelpers;
using System.Linq;
using Xunit;

namespace Shouldly.Tests.ShouldBe.ShouldBeUnique
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

            comparison1.ShouldBeUnique(new ComparibleClassComparer());
        }

        [Fact]
        public void ComparerNotEqualsShouldFail()
        {
            var comparison1 = new ComparibleClass[]
            {
                new ComparibleClass() { Property = "Kangaroo", IgnoredProperty = "Whale" },
                new ComparibleClass() { Property = "Kangaroo", IgnoredProperty = "Salmon" }
            };

            Verify.ShouldFail(() =>
comparison1.ShouldBeUnique(new ComparibleClassComparer(), "Some additional context"),

errorWithSource:
@"comparison1
    should be unique but
[Shouldly.Tests.TestHelpers.ComparibleClass (000000)]
    was duplicated

Additional Info:
    Some additional context",

errorWithoutSource:
@"[Shouldly.Tests.TestHelpers.ComparibleClass (000000), Shouldly.Tests.TestHelpers.ComparibleClass (000000)]
    should be unique but
[Shouldly.Tests.TestHelpers.ComparibleClass (000000)]
    was duplicated

Additional Info:
    Some additional context");
        }
    }
}
