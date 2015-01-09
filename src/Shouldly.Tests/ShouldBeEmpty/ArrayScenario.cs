using Shouldly.Tests.TestHelpers;

namespace Shouldly.Tests.ShouldBeEmpty
{
    public class ArrayScenario : ShouldlyShouldTestScenario
    {
        protected override void ShouldThrowAWobbly()
        {
            new[] { 1 }.ShouldBeEmpty();
        }

        protected override string ChuckedAWobblyErrorMessage
        {
            get { return "new[] { 1 } should be empty but had 1 item and was [1]"; }
        }

        protected override void ShouldPass()
        {
            new int[0].ShouldBeEmpty();
        }
    }
}