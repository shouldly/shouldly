using Shouldly.Tests.TestHelpers;

namespace Shouldly.Tests.ShouldBe
{
    public class BoxedComparableScenario : ShouldlyShouldTestScenario
    {
        protected override void ShouldThrowAWobbly()
        {
            object a = 0;
            object b = 0.1;
            a.ShouldBe(b);
        }

        protected override string ChuckedAWobblyErrorMessage
        {
            get { return "a should be 0.1 but was 0"; }
        }

        protected override void ShouldPass()
        {
            object a = 0;
            object b = 0.0;
            a.ShouldBe(b);
        }
    }
}