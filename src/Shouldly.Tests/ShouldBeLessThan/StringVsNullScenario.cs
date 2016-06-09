using Shouldly.Tests.Strings;
using Xunit;

namespace Shouldly.Tests.ShouldBeLessThan
{
    public class StringVsNullScenario
    {
        [Fact]
        public void StringVsNullScenarioShouldFail()
        {
            var bee = "b";
            Verify.ShouldFail(() =>
bee.ShouldBeLessThan(null, "Some additional context"),

errorWithSource:
@"bee
    should be less than
null
    but was
""b""

Additional Info:
    Some additional context",

errorWithoutSource:
@"""b""
    should be less than
null
    but was not

Additional Info:
    Some additional context");
        }

        [Fact]
        public void ShouldPass()
        {
            ((string)null).ShouldBeLessThan("b");
        }
    }
}