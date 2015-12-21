using Shouldly.Tests.Strings;
using Shouldly.Tests.TestHelpers;
using Xunit;

namespace Shouldly.Tests.ShouldBeOfType
{
    public class DerivedTypeScenario
    {
        [Fact]
        public void DerivedTypeScenarioShouldFail()
        {
            var myThing = new MyThing();
            // ReSharper disable once ExpressionIsAlwaysNull
            Verify.ShouldFail(() =>
myThing.ShouldBeOfType<MyBase>("Some additional context"),

errorWithSource:
@"myThing
    should be of type
Shouldly.Tests.TestHelpers.MyBase
    but was
Shouldly.Tests.TestHelpers.MyThing

Additional Info:
    Some additional context",

errorWithoutSource:
@"Shouldly.Tests.TestHelpers.MyThing (000000)
    should be of type
Shouldly.Tests.TestHelpers.MyBase
    but was
Shouldly.Tests.TestHelpers.MyThing

Additional Info:
    Some additional context");
        }
    }
}