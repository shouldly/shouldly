namespace Shouldly.Tests.ShouldBeOfType
{
    public class ActualIsNullScenario
    {
        [Fact]
        public void ActualIsNullScenarioShouldFail()
        {
            MyThing? myThing = null;
            // ReSharper disable once ExpressionIsAlwaysNull
            Verify.ShouldFail(() =>
myThing.ShouldBeOfType<MyBase>("Some additional context"),

errorWithSource:
@"myThing
    should be of type
Shouldly.Tests.TestHelpers.MyBase
    but was
null

Additional Info:
    Some additional context",

errorWithoutSource:
@"null
    should be of type
Shouldly.Tests.TestHelpers.MyBase
    but was not

Additional Info:
    Some additional context");
        }
    }
}