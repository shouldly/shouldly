using Shouldly.Tests.TestHelpers;

namespace Shouldly.Tests.ShouldBeOfType
{
    public class DerivedTypeScenario : ShouldlyShouldFailureTestScenario
    {
        protected override void ShouldThrowAWobbly()
        {
            var myThing = new MyThing();
            myThing.ShouldBeOfType<MyBase>();
        }

        protected override string ChuckedAWobblyErrorMessage
        {
            get { return "myThing should be of type Shouldly.Tests.TestHelpers.MyBase but was Shouldly.Tests.TestHelpers.MyThing"; }
        }
    }
}