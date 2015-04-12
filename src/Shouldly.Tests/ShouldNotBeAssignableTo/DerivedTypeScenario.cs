using Shouldly.Tests.TestHelpers;

namespace Shouldly.Tests.ShouldNotBeAssignableTo
{
    public class DerivedTypeScenario : ShouldlyShouldTestScenario
    {
        protected override void ShouldThrowAWobbly()
        {
            var myThing = new MyThing();
            myThing.ShouldNotBeAssignableTo<MyThing>("Some additional context");
        }

        protected override string ChuckedAWobblyErrorMessage
        {
            get { return "myThing should not be assignable to Shouldly.Tests.TestHelpers.MyThing but was Shouldly.Tests.TestHelpers.MyThing"; }
        }

        protected override void ShouldPass()
        {
            var myThing = new MyThing();
            myThing.ShouldNotBeAssignableTo<string>();
        }
    }
}