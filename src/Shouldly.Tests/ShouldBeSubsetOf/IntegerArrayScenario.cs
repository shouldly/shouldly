using Shouldly.Tests.TestHelpers;

namespace Shouldly.Tests.ShouldBeSubsetOf
{
    public class IntegerArrayScenario : ShouldlyShouldTestScenario
    {
        protected override void ShouldThrowAWobbly()
        {
            new[] { 1, 2, 5 }.ShouldBeSubsetOf(new[] {2, 3, 4}, "Some additional context");
        }

        protected override string ChuckedAWobblyErrorMessage
        {
            get
            {
                return "new[] { 1, 2, 5 } should be subset of  [2, 3, 4] but [1, 5] are outside subset" +
                       "Additional Info:" +
                       "Some additional context";
            }
        }

        protected override void ShouldPass()
        {
            new[] {1}.ShouldBeSubsetOf(new[] {1, 2, 3});
        }
    }
}