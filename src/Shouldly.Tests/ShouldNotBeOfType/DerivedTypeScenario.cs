using Shouldly.Tests.TestHelpers;

namespace Shouldly.Tests.ShouldNotBeOfType
{
    public class DerivedTypeScenario : ShouldlyShouldTestScenario
    {
        protected override void ShouldThrowAWobbly()
        {
            var myThing = new MyThing();
            myThing.ShouldNotBeOfType<MyThing>("Some additional context");
        }

        protected override string ChuckedAWobblyErrorMessage
        {
            get
            {
                return
                    "myThing should not be of type Shouldly.Tests.TestHelpers.MyThing but was Shouldly.Tests.TestHelpers.MyThing" +
                    "Additional Info: " +
                    "Some additional context";
            }
        }

        protected override void ShouldPass()
        {
            var myThing = new MyThing();
            myThing.ShouldNotBeOfType<MyBase>();
        }
    }
}