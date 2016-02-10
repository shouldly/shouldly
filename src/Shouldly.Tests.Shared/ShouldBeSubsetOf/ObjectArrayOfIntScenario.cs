using Shouldly.Tests.Strings;
using Xunit;

namespace Shouldly.Tests.ShouldBeSubsetOf
{
    public class ObjectArrayOfIntScenario
    {

        [Fact]
        public void ObjectArrayOfIntScenarioShouldFail()
        {
            var arr = new object[] { 1, 2, 3 };
            var arr2 = new object[] { 1, 2 };

            Verify.ShouldFail(() =>
arr.ShouldBeSubsetOf(arr2, "Some additional context"),

errorWithSource:
@"arr
    should be subset of
[1, 2]
    but
[3]
    is outside subset

Additional Info:
    Some additional context",

errorWithoutSource:
@"[1, 2, 3]
    should be subset of
[1, 2]
    but
[3]
    is outside subset

Additional Info:
    Some additional context");
        }

        [Fact]
        public void ShouldPass()
        {
            var arr = new object[] { 1, 2, 3 };
            var arr2 = new object[] { 1, 2, 3, 4 };

            arr.ShouldBeSubsetOf(arr2);
        }
    }
}