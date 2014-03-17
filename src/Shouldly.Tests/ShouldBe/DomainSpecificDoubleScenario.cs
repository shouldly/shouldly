using Shouldly.Tests.TestHelpers;

namespace Shouldly.Tests.ShouldBe
{
    public class DomainSpecificDoubleScenario : ShouldlyShouldTestScenario
    {
        protected override void ShouldThrowAWobbly()
        {
            object a = 1;
            DomainSpecificDouble min = 0;
            min.ShouldBe(a);
        }

        protected override string ChuckedAWobblyErrorMessage
        {
            get { return "min should be 1 but was 0"; }
        }

        protected override void ShouldPass()
        {
            object a = 0d;
            DomainSpecificDouble min = 0;
            min.ShouldBe(a);
        }
    }
}