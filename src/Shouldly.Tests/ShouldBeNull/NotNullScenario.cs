using Shouldly.Tests.Strings;
using Xunit;

namespace Shouldly.Tests.ShouldBeNull
{
    public class NotNullScenario
    {
        [Fact]
        public void NotNullScenarioShouldFail()
        {
            string myNullRef = null;
            // ReSharper disable once ExpressionIsAlwaysNull
            Verify.ShouldFail(() =>
myNullRef.ShouldNotBeNull("Some additional context"),

errorWithSource:
@"myNullRef
    should not be null but was

Additional Info:
    Some additional context",

errorWithoutSource:
@"null
    should not be null but was

Additional Info:
    Some additional context");
        }

        [Fact]
        public void ShouldPass()
        {
            string myRef = "Hello World";
            
            myRef.ShouldNotBeNull()
                .ShouldBeSameAs(myRef);
        }
    }
}