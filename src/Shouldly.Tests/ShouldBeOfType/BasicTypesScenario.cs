using Shouldly.Tests.TestHelpers;

namespace Shouldly.Tests.ShouldBeOfType
{
    public class BasicTypesScenario : ShouldlyShouldTestScenario
    {
        protected override void ShouldThrowAWobbly()
        {
            1.ShouldBeOfType<string>();
        }

        protected override string ChuckedAWobblyErrorMessage
        {
            get { return "1 should be of type System.String but was System.Int32"; }
        }

        protected override void ShouldPass()
        {
            1.ShouldBeOfType<int>();
        }
    }
}