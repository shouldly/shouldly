namespace Shouldly.Tests.ShouldBeOfType;

public class DerivedTypeScenario
{
    [Fact]
    public void DerivedTypeScenarioShouldFail()
    {
        var myThing = new MyThing();
        // ReSharper disable once ExpressionIsAlwaysNull
        Verify.ShouldFail(() =>
            myThing.ShouldBeOfType<MyBase>("Some additional context"));
    }
}