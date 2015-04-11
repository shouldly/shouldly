using Shouldly.Tests.TestHelpers;

namespace Shouldly.Tests.ShouldBeSubsetOf
{
    public class DecimalArrayScenario : ShouldlyShouldTestScenario
    {
        protected override void ShouldThrowAWobbly()
        {
            new[] { 1m, 2m, 5m }.ShouldBeSubsetOf(new[] {2m, 3m, 4m}, "Some additional context");
        }

        protected override string ChuckedAWobblyErrorMessage
        {
            get
            {
                return "new[] { 1m, 2m, 5m } should be subset of  [2, 3, 4] but [1, 5] are outside subset" +
                       "Additional Info:" +
                       "Some additional context";
            }
        }

        protected override void ShouldPass()
        {
            new[] {1m}.ShouldBeSubsetOf(new[] {1m, 2m, 3m}, () => "Some additional context");
        }
    }
}