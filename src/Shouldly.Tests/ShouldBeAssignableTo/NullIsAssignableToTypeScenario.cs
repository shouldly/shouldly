namespace Shouldly.Tests.ShouldBeAssignableTo
{
    public class NullIsAssignableToTypeScenario
    {
        [Fact]
        public void ShouldThrowWhenNullPassedToShouldBeAssignableValueType()
        {
            MyThing? myThing = null;
            // ReSharper disable once ExpressionIsAlwaysNull
            Verify.ShouldFail(() =>
            myThing.ShouldBeAssignableTo<int>("Some additional context"),

errorWithSource:
@"myThing
    should be assignable to
System.Int32
    but was
null

Additional Info:
    Some additional context",

errorWithoutSource:
@"null
    should be assignable to
System.Int32
    but was not

Additional Info:
    Some additional context");
        }

        [Fact]
        public void ShouldPass()
        {
            MyThing? myThing = null;
            myThing.ShouldBeAssignableTo<MyBase>("Some additional context");
        }
    }
}