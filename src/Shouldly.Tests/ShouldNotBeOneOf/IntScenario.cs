using Shouldly.Tests.Strings;
using Xunit;

namespace Shouldly.Tests.ShouldNotBeOneOf
{
    public class IntScenario
    {
        [Fact]
        public void IntScenarioShouldFail()
        {
            var one = 1;
            Verify.ShouldFail(() =>
one.ShouldNotBeOneOf(1, 2, 3),

errorWithSource:
@"one
    should not be one of
[1, 2, 3]
    but was
1",

errorWithoutSource:
@"1
    should not be one of
[1, 2, 3]
    but was");
        }

        [Fact]
        public void ShouldPass()
        {
            1.ShouldNotBeOneOf(4, 5, 6);
        }
    }
}