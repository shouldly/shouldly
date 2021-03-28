using Shouldly.Tests.Strings;
using Xunit;

namespace Shouldly.Tests.ShouldContain
{
    public class IntegerScenario
    {
        private readonly int[] _target = { 1, 2, 3, 4, 5 };

        [Fact]
        public void IntegerScenarioShouldFail()
        {
            Verify.ShouldFail(() =>
_target.ShouldContain(6, "Some additional context"),

errorWithSource:
@"_target
    should contain
6
    but was actually
[1, 2, 3, 4, 5]

Additional Info:
    Some additional context",

errorWithoutSource:
@"[1, 2, 3, 4, 5]
    should contain
6
    but did not

Additional Info:
    Some additional context");
        }

        [Fact]
        public void ShouldPass()
        {
            _target.ShouldContain(3);
        }
    }
}