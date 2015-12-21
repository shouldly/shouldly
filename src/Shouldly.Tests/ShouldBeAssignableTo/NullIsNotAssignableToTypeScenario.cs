using Shouldly.Tests.Strings;
using Shouldly.Tests.TestHelpers;
using Xunit;

namespace Shouldly.Tests.ShouldBeAssignableTo
{
    // TODO I think this behavior is wrong, null is assignable to a nullable type?
    public class NullIsNotAssignableToTypeScenario
    {
        [Fact]
        public void ShouldThrowWhenNullPassedToShouldBeAssignable()
        {
            MyThing myThing = null;
            // ReSharper disable once ExpressionIsAlwaysNull
Verify.ShouldFail(() =>
            myThing.ShouldBeAssignableTo<MyBase>("Some additional context"),

errorWithSource:
@"myThing
    should be assignable to
Shouldly.Tests.TestHelpers.MyBase
    but was
null

Additional Info:
    Some additional context",

errorWithoutSource:
@"null
    should be assignable to
Shouldly.Tests.TestHelpers.MyBase
    but was not

Additional Info:
    Some additional context");
        }
    }
}