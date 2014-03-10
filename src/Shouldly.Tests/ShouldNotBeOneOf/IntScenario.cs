using Shouldly.Tests.TestHelpers;

namespace Shouldly.Tests.ShouldNotBeOneOf
{
    public class IntScenario : ShouldlyShouldTestScenario
    {
        protected override void ShouldThrowAWobbly()
        {
            1.ShouldNotBeOneOf(1, 2, 3);
        }

        protected override string ChuckedAWobblyErrorMessage
        {
            get { return "1 should not be one of [1, 2, 3] but was 1"; }
        }

        protected override void ShouldPass()
        {
            1.ShouldNotBeOneOf(4, 5, 6);
        }
    }
}