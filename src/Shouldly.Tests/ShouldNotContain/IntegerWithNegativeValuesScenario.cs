using Shouldly.Tests.Strings;
using Xunit;

namespace Shouldly.Tests.ShouldNotContain
{
    public class IntegerWithNegativeValuesScenario
    {
        readonly int[] _target = { 2, 3, 4, 5, 4, 123665, 11234, -13562377 };

        [Fact]
        public void IntegerWithNegativeValuesScenarioShouldFail()
        {
            Verify.ShouldFail(() =>
    _target.ShouldNotContain(3, "Some additional context"),

errorWithSource:
@"_target
    should not contain
3
    but was actually
[2, 3, 4, 5, 4, 123665, 11234, -13562377]

Additional Info:
    Some additional context",

    errorWithoutSource:
@"[2, 3, 4, 5, 4, 123665, 11234, -13562377]
    should not contain
3
    but did

Additional Info:
    Some additional context");
        }

        [Fact]
        public void ShouldPass()
        {
            _target.ShouldNotContain(-300);
        }
    }
}