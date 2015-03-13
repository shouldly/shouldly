using Shouldly.Tests.TestHelpers;

namespace Shouldly.Tests.ShouldContain
{
    public class EmptyArrayScenario : ShouldlyShouldFailureTestScenario
    {
        protected override void ShouldThrowAWobbly()
        {
            new int[0].ShouldContain(1);
        }

        protected override string ChuckedAWobblyErrorMessage
        {
            get { return "new int[0] should contain 1 but does not"; }
        }
    }
}