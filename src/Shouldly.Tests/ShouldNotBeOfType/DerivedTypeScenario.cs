namespace Shouldly.Tests.ShouldNotBeOfType;

public class DerivedTypeScenario
{
    [Fact]
    public void DerivedTypeScenarioShouldFail()
    {
        var myThing = new MyThing();
        Verify.ShouldFail(() =>
            myThing.ShouldNotBeOfType<MyThing>("Some additional context"));
    }

    [Fact]
    public void ShouldPass()
    {
        var myThing = new MyThing();
        myThing.ShouldNotBeOfType<MyBase>();
    }
}