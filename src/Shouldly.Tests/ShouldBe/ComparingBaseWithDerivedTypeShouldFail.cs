using Shouldly.Tests.TestHelpers;

namespace Shouldly.Tests.ShouldBe
{
    public class ComparingBaseWithDerivedTypeShouldFail : ShouldlyShouldFailureTestScenario
    {
        protected override void ShouldThrowAWobbly()
        {
            new MyBase().ShouldBe(new MyThing());
        }

        protected override string ChuckedAWobblyErrorMessage
        {
            get { return "new MyBase() should be Shouldly.Tests.TestHelpers.MyThing but was Shouldly.Tests.TestHelpers.MyBase"; }
        }
    }
}