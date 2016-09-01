using Shouldly.Tests.TestHelpers;
using Xunit;

namespace Shouldly.Tests.ShouldBeGreaterOrEqualTo
{
    public class CustomObjectScenario
    {
        [Fact]
        public void CompareCustom()
        {
            var customA = new Custom { Val = 1 };
            var customB = new Custom { Val = 1 };
            var comparer = new CustomComparer<Custom>();
            customA.ShouldBeGreaterThanOrEqualTo(customB, comparer);
        }
    }
}
