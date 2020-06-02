using Shouldly.Tests.Strings;
using Xunit;

namespace Shouldly.Tests.ShouldStartEndWith
{
    public class StartEndWithIntegerWithNegativeValuesScenario
    {
        readonly int[] _target = { 2, 3, 4, 5, 4, 123665, 11234, -13562377 };

        [Fact]
        public void StartShouldFail()
        {
            Verify.ShouldFail(() =>
_target.ShouldStartWith(-2, "Some additional context"),

errorWithSource:
@"_target
    should start with
-2
    but was
[2, 3, 4, 5, 4, 123665, 11234, -13562377]

Additional Info:
    Some additional context",

errorWithoutSource:
@"[2, 3, 4, 5, 4, 123665, 11234, -13562377]
    should start with
-2
    but was not

Additional Info:
    Some additional context");
        }

        [Fact]
        public void StartShouldPass()
        {
            _target.ShouldStartWith(2);
        }

        [Fact]
        public void EndShouldFail()
        {
            Verify.ShouldFail(() =>
_target.ShouldEndWith(13562377, "Some additional context"),

errorWithSource:
@"_target
    should end with
13562377
    but was
[2, 3, 4, 5, 4, 123665, 11234, -13562377]

Additional Info:
    Some additional context",

errorWithoutSource:
@"[2, 3, 4, 5, 4, 123665, 11234, -13562377]
    should end with
13562377
    but did not

Additional Info:
    Some additional context");
        }

        [Fact]
        public void EndShouldPass()
        {
            _target.ShouldEndWith(-13562377);
        }
    }
}