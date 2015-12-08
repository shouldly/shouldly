using Shouldly.Tests.Strings;
using Shouldly.Tests.TestHelpers;
using Xunit;

namespace Shouldly.Tests.ShouldBeAssignableTo
{
    public class DerivedTypeScenario
    {
        [Fact]
        public void DerivedTypeScenarioShouldFail()
        {
            var myThing = new MyThing();
            Verify.ShouldFail(() =>
myThing.ShouldBeAssignableTo<string>("Some additional context"),

errorWithSource:
@"myThing
    should be assignable to
System.String
    but was
Shouldly.Tests.TestHelpers.MyThing

Additional Info:
    Some additional context",

errorWithoutSource:
@"Shouldly.Tests.TestHelpers.MyThing
    should be assignable to
System.String
    but was not

Additional Info:
    Some additional context");
        }

        [Fact]
        public void ShouldPass()
        {
            var myThing = new MyThing();
            myThing.ShouldBeAssignableTo<MyBase>();
        }
    }
}