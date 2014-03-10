using Shouldly.Tests.TestHelpers;

namespace Shouldly.Tests.ShouldNotBeOfType
{
    public class BasicTypesScenario : ShouldlyShouldTestScenario
    {
        protected override void ShouldThrowAWobbly()
        {
            1.ShouldNotBeOfType<int>();
        }

        protected override string ChuckedAWobblyErrorMessage
        {
            get { return "1 should not be of type System.Int32 but was 1"; }
        }

        protected override void ShouldPass()
        {
            1.ShouldNotBeOfType<string>();
        }
    }
}