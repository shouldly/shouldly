using Shouldly.Tests.TestHelpers;

namespace Shouldly.Tests.ShouldBeAssignableTo
{
    public class DerivedTypeScenario : ShouldlyShouldTestScenario
    {
        protected override void ShouldThrowAWobbly()
        {
            var myThing = new MyThing();
            myThing.ShouldBeAssignableTo<string>(() => "Some additional context");
        }

        protected override string ChuckedAWobblyErrorMessage
        {
            get
            {
                return "myThing should be assignable to System.String but was Shouldly.Tests.TestHelpers.MyThing" +
                       "Additional Info: " +
                       "Some additional context";
            }
        }

        protected override void ShouldPass()
        {
            var myThing = new MyThing();
            myThing.ShouldBeAssignableTo<MyBase>(() => "Some additional context");
        }
    }
}