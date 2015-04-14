using Shouldly.Tests.TestHelpers;

namespace Shouldly.Tests.ShouldBeAssignableTo
{
    // TODO I think this behavior is wrong, null is assignable to a nullable type?
    public class NullIsNotAssignableToTypeScenario : ShouldlyShouldFailureTestScenario
    {
        protected override void ShouldThrowAWobbly()
        {
            MyThing myThing = null;
            // ReSharper disable once ExpressionIsAlwaysNull
            myThing.ShouldBeAssignableTo<MyBase>("Some additional context");
        }

        protected override string ChuckedAWobblyErrorMessage
        {
            get
            {
                return "myThing should be assignable to Shouldly.Tests.TestHelpers.MyBase but was null" +
                       "Additional Info: " +
                       "Some additional context";
            }
        }
    }
}