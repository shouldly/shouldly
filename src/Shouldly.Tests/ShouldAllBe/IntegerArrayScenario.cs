using Shouldly.Tests.TestHelpers;

namespace Shouldly.Tests.ShouldAllBe
{
    public class IntegerArrayScenario : ShouldlyShouldTestScenario
    {
        protected override void ShouldThrowAWobbly()
        {
            new[] { 1, 2, 3 }.ShouldAllBe(x => x < 2);
        }

        protected override string ChuckedAWobblyErrorMessage
        {
            get { return "new[] { 1, 2, 3 } should all be an element satisfying the condition (x < 2) but does not"; }
        }

        protected override void ShouldPass()
        {
            new[] { 1, 2, 3 }.ShouldAllBe(x => x < 4);
        }
    }
}