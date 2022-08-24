namespace Shouldly.Tests.ShouldNotBeAssignableTo
{
    public class DerivedTypeScenario
    {
        [Fact]
        public void DerivedTypeScenarioShouldFail()
        {
            var myThing = new MyThing();
            Verify.ShouldFail(() =>
myThing.ShouldNotBeAssignableTo<MyThing>("Some additional context"),

errorWithSource:
@"myThing
    should not be assignable to
Shouldly.Tests.TestHelpers.MyThing
    but was
Shouldly.Tests.TestHelpers.MyThing (000000)

Additional Info:
    Some additional context",

errorWithoutSource:
@"Shouldly.Tests.TestHelpers.MyThing (000000)
    should not be assignable to
Shouldly.Tests.TestHelpers.MyThing
    but was

Additional Info:
    Some additional context");
        }

        [Fact]
        public void ShouldPass()
        {
            var myThing = new MyThing();
            myThing.ShouldNotBeAssignableTo<string>();
        }
    }
}