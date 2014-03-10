using Shouldly.Tests.TestHelpers;

namespace Shouldly.Tests.ShouldBe.WithTollerance
{
    public class EnumerableOfDecimalScenario : ShouldlyShouldTestScenario
    {
        protected override void ShouldThrowAWobbly()
        {
            var firstSet = new[] { 1.23m, 2.34m, 3.45001m };
            var secondSet = new[] { 1.4301m, 2.34m, 3.45m };
            firstSet.ShouldBe(secondSet, 0.1m);
        }

        protected override string ChuckedAWobblyErrorMessage
        {
            get { return "firstSet should be [1.4301, 2.34, 3.45] but was [1.23, 2.34, 3.45001] difference [*1.23*, 2.34, *3.45001*]"; }
        }

        protected override void ShouldPass()
        {
            var firstSet = new[] { 1.23m, 2.34m, 3.45001m };
            var secondSet = new[] { 1.2301m, 2.34m, 3.45m };
            firstSet.ShouldBe(secondSet, 0.01m);
        }
    }
}