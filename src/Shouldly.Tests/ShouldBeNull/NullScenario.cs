using Shouldly.Tests.Strings;
using Xunit;

namespace Shouldly.Tests.ShouldBeNull
{
    public class NullScenario
    {
        [Fact]
        public void NullScenarioShouldFail()
        {
            string myNullRef = "Hello World";
            Verify.ShouldFail(() =>
myNullRef.ShouldBeNull("Some additional context"),

errorWithSource:
@"myNullRef
    should be null but was
""Hello World""

Additional Info:
    Some additional context",

errorWithoutSource:
@"""Hello World""
    should be null but was

Additional Info:
    Some additional context");
        }

        [Fact]
        public void ShouldPass()
        {
            ((string)null).ShouldBeNull();
        }
    }
}