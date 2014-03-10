using Shouldly.Tests.TestHelpers;

namespace Shouldly.Tests.ShouldNotBe
{
    public class IntegerScenario : ShouldlyShouldTestScenario
    {
        protected override void ShouldPass()
        {
            1.ShouldNotBe(2);
        }

        protected override void ShouldThrowAWobbly()
        {
            const int one = 1;
            one.ShouldNotBe(1);
        }

        protected override string ChuckedAWobblyErrorMessage
        {
            get { return "one should not be 1 but was 1"; }
        }
    }
}