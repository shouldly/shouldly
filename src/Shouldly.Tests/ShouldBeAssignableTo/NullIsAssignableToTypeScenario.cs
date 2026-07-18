namespace Shouldly.Tests.ShouldBeAssignableTo;

public class NullIsAssignableToTypeScenario
{
    [Fact]
    public void ShouldThrowWhenNullPassedToShouldBeAssignableValueType()
    {
        MyThing? myThing = null;
        // ReSharper disable once ExpressionIsAlwaysNull
        Verify.ShouldFail(() =>
            myThing.ShouldBeAssignableTo<int>("Some additional context"));
    }

    [Fact]
    public void ShouldPassWithNullReferenceType()
    {
        MyThing? myThing = null;
        myThing.ShouldBeAssignableTo<MyBase>("Some additional context");
    }

    [Fact]
    public void ShouldPassWithNullValueType()
    {
        int? myInt = null;
        myInt.ShouldBeAssignableTo<int?>("Some additional context");
    }
}