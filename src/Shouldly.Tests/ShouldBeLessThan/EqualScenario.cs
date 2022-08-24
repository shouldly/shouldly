using Shouldly.Tests.Strings;

namespace Shouldly.Tests.ShouldBeLessThan
{
    public class EqualScenario
    {
        [Fact]
        public void EqualScenarioShouldFail()
        {
            var one = 1;
            Verify.ShouldFail(() =>
one.ShouldBeLessThan(1, "Some additional context"),

errorWithSource:
@"one
    should be less than
1
    but was
1

Additional Info:
    Some additional context",

errorWithoutSource:
@"1
    should be less than
1
    but was not

Additional Info:
    Some additional context");
        }
    }
}