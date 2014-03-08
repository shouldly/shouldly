namespace Shouldly.Tests.ShouldNotBeAssignableTo
{
    public class DerivedTypeScenario : ShouldlyShouldTestScenario
    {
        protected override void ShouldThrowAWobbly()
        {
            var myThing = new MyThing();
            myThing.ShouldNotBeAssignableTo<MyThing>();
        }

        protected override string ChuckedAWobblyErrorMessage
        {
            get { return "myThing should not be assignable to Shouldly.Tests.MyThing but was Shouldly.Tests.MyThing"; }
        }

        protected override void ShouldPass()
        {
            var myThing = new MyThing();
            myThing.ShouldNotBeAssignableTo<string>();
        }
    }
}