namespace Shouldly.Tests.ShouldBeAssignableTo;

public class DerivedTypeScenario
{
    [Fact]
    public void DerivedTypeScenarioShouldFail()
    {
        var myThing = new MyThing();
        Verify.ShouldFail(() =>
            myThing.ShouldBeAssignableTo<string>("Some additional context"));
    }

    [Fact]
    public void ShouldPass()
    {
        var myThing = new MyThing();
        myThing.ShouldBeAssignableTo<MyBase>();
    }
}