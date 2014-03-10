using Shouldly.Tests.TestHelpers;

namespace Shouldly.Tests.ShouldBeAssignableTo
{
    public class DerivedTypeScenario : ShouldlyShouldTestScenario
    {
        protected override void ShouldThrowAWobbly()
        {
            var myThing = new MyThing();
            myThing.ShouldBeAssignableTo<string>();
        }

        protected override string ChuckedAWobblyErrorMessage
        {
            get { return "myThing should be assignable to System.String but was Shouldly.Tests.TestHelpers.MyThing"; }
        }

        protected override void ShouldPass()
        {
            var myThing = new MyThing();
            myThing.ShouldBeAssignableTo<MyBase>();
        }
    }
}