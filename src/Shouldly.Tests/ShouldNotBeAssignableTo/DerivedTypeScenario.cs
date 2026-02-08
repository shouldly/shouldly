namespace Shouldly.Tests.ShouldNotBeAssignableTo;

public class DerivedTypeScenario
{
    [Fact]
    public void DerivedTypeScenarioShouldFail()
    {
        var myThing = new MyThing();
        Verify.ShouldFail(() =>
            myThing.ShouldNotBeAssignableTo<MyThing>("Some additional context"));
    }

    [Fact]
    public void ShouldPass()
    {
        var myThing = new MyThing();
        myThing.ShouldNotBeAssignableTo<string>();
    }
}