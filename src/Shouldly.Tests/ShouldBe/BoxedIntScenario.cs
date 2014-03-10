using Shouldly.Tests.TestHelpers;

namespace Shouldly.Tests.ShouldBe
{
    public class BoxedIntScenario : ShouldlyShouldFailureTestScenario
    {
        protected override void ShouldThrowAWobbly()
        {
            ((object) 12).ShouldBe("string");
        }

        protected override string ChuckedAWobblyErrorMessage
        {
            get { return "((object)12) should be \"string\" but was 12"; }
        }
    }
}