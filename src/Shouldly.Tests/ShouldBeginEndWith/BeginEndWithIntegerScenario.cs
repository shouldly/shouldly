using Shouldly.Tests.Strings;
using Xunit;

namespace Shouldly.Tests.ShouldBeginEndWith
{
    public class BeginEndWithIntegerScenario
    {
        readonly int[] _target = { 1, 2, 3, 4, 5 };

        [Fact]
        public void BeginShouldFail()
        {
            Verify.ShouldFail(() =>
_target.ShouldBeginWith(2, "Some additional context"),

errorWithSource:
@"_target
    should begin with
2
    but was
[1, 2, 3, 4, 5]

Additional Info:
    Some additional context",

errorWithoutSource:
@"[1, 2, 3, 4, 5]
    should begin with
2
    but did not

Additional Info:
    Some additional context");
        }

        [Fact]
        public void BeginShouldPass()
        {
            _target.ShouldBeginWith(1);
        }


        [Fact]
        public void EndShouldFail()
        {
            Verify.ShouldFail(() =>
_target.ShouldEndWith(6, "Some additional context"),

errorWithSource:
@"_target
    should end with
6
    but was
[1, 2, 3, 4, 5]

Additional Info:
    Some additional context",

errorWithoutSource:
@"[1, 2, 3, 4, 5]
    should end with
6
    but did not

Additional Info:
    Some additional context");
        }

        [Fact]
        public void EndShouldPass()
        {
            _target.ShouldEndWith(5);
        }
    }
}
