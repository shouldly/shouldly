using Shouldly.Tests.TestHelpers;

namespace Shouldly.Tests.ShouldNotBeEmpty
{
    public class ArrayScenario : ShouldlyShouldTestScenario
    {
        protected override void ShouldThrowAWobbly()
        {
            new int[0].ShouldNotBeEmpty();
        }

        protected override string ChuckedAWobblyErrorMessage
        {
            get { return "new int[0] should not be empty but was "; }
        }

        protected override void ShouldPass()
        {
            new []{1}.ShouldNotBeEmpty();
        }
    }
}