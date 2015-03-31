using Shouldly.Tests.TestHelpers;

namespace Shouldly.Tests.ShouldBeSubsetOf
{
    public class FloatArrayScenario : ShouldlyShouldTestScenario
    {
        protected override void ShouldThrowAWobbly()
        {
            new[] { 1f, 2f, 5f }.ShouldBeSubsetOf(new[] { 2f, 3f, 4f });
        }

        protected override string ChuckedAWobblyErrorMessage
        {
            get { return "new[] { 1f, 2f, 5f } should be subset of  [2, 3, 4] but [1, 5] are outside subset"; }
        }

        protected override void ShouldPass()
        {
            new[] { 1f }.ShouldBeSubsetOf(new[] { 1f, 2f, 3f });
        }
    }
}