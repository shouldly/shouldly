using Shouldly.Tests.TestHelpers;

namespace Shouldly.Tests.ShouldBeSubsetOf
{
    public class DecimalArrayScenario : ShouldlyShouldTestScenario
    {
        protected override void ShouldThrowAWobbly()
        {
            new[] {1m}.ShouldBeSubsetOf(new[] {2m, 3m, 4m}, () => "Some additional context");
        }

        protected override string ChuckedAWobblyErrorMessage
        {
            get
            {
                return "new[] { 1m } should be subset of  [2, 3, 4] but does not" +
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