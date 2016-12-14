using Shouldly.Tests.Strings;
using Xunit;

namespace Shouldly.Tests.ShouldBeginEndWith
{
    public class BeginEndWithIntegerWithNegativeValuesScenario
    {
        readonly int[] _target = { 2, 3, 4, 5, 4, 123665, 11234, -13562377 };

        [Fact]
        public void BeginShouldFail()
        {
            Verify.ShouldFail(() =>
_target.ShouldBeginWith(-2, "Some additional context"),

errorWithSource:
@"_target
    should begin with
-2
    but was
[2, 3, 4, 5, 4, 123665, 11234, -13562377]

Additional Info:
    Some additional context",

errorWithoutSource:
@"[2, 3, 4, 5, 4, 123665, 11234, -13562377]
    should begin with
-2
    but did not

Additional Info:
    Some additional context");
        }

        [Fact]
        public void BeginShouldPass()
        {
            _target.ShouldBeginWith(2);
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