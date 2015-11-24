using Shouldly.Tests.TestHelpers;

namespace Shouldly.Tests.ShouldBeSubsetOf
{
    public class FloatArrayScenario : ShouldlyShouldTestScenario
    {
        protected override void ShouldThrowAWobbly()
        {
            new[] { 1f, 2f, 5f }.ShouldBeSubsetOf(new[] {2f, 3f, 4f}, "Some additional context");
        }

        protected override string ChuckedAWobblyErrorMessage
        {
            get
            {
                return "new[] { 1f, 2f, 5f } should be subset of  [2f, 3f, 4f] but [1f, 5f] are outside subset" +
                       "Additional Info:" +
                       "Some additional context";
            }
        }

        protected override void ShouldPass()
        {
            new[] {1f}.ShouldBeSubsetOf(new[] {1f, 2f, 3f});
        }
    }
}