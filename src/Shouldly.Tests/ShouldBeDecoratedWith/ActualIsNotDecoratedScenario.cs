namespace Shouldly.Tests.ShouldBeDecoratedWith;

public class ActualIsNotDecoratedScenario
{
    [Fact]
    public void ActualIsNotDecoratedScenarioShouldFail()
    {
        var myThingType = typeof(MyThing);

        // ReSharper disable once ExpressionIsAlwaysNull
        Verify.ShouldFail(() =>
            myThingType.ShouldBeDecoratedWith<UseCultureAttribute>("Some additional context"));
    }
}