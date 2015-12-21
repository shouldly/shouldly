using Shouldly.Tests.Strings;
using Shouldly.Tests.TestHelpers;
using Xunit;

namespace Shouldly.Tests.ShouldNotBeOfType
{
    public class DerivedTypeScenario
    {
        [Fact]
        public void DerivedTypeScenarioShouldFail()
        {
            var myThing = new MyThing();
            Verify.ShouldFail(() =>
myThing.ShouldNotBeOfType<MyThing>("Some additional context"),

errorWithSource:
@"myThing
    should not be of type
Shouldly.Tests.TestHelpers.MyThing
    but was
Shouldly.Tests.TestHelpers.MyThing (000000)

Additional Info:
    Some additional context",

errorWithoutSource:
@"Shouldly.Tests.TestHelpers.MyThing (000000)
    should not be of type
Shouldly.Tests.TestHelpers.MyThing
    but was

Additional Info:
    Some additional context");
        }

        [Fact]
        public void ShouldPass()
        {
            var myThing = new MyThing();
            myThing.ShouldNotBeOfType<MyBase>();
        }
    }
}