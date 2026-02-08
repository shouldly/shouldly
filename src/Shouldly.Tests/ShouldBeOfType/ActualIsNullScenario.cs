namespace Shouldly.Tests.ShouldBeOfType;

public class ActualIsNullScenario
{
    [Fact]
    public void ActualIsNullScenarioShouldFail()
    {
        MyThing? myThing = null;
        // ReSharper disable once ExpressionIsAlwaysNull
        Verify.ShouldFail(() =>
            myThing.ShouldBeOfType<MyBase>("Some additional context"));
    }
}