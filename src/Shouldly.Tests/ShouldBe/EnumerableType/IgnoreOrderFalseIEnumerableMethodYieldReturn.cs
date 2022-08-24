using Shouldly.Tests.Strings;
using Xunit;

namespace Shouldly.Tests.ShouldBe.EnumerableType
{
    public class IgnoreOrderFalseIEnumerableMethodYieldReturn
    {
        [Fact]
        public void IgnoreOrderFalseIEnumerableMethodYieldReturnShouldFail()
        {
            Verify.ShouldFail(() =>
GetEnumerable().ShouldBe(new[] { 1, 2 }, false, "Some additional context"),

errorWithSource:
@"GetEnumerable()
    should be
[1, 2]
    but was
[1]
    difference
[1, *]

Additional Info:
    Some additional context",

errorWithoutSource:
@"[1]
    should be
[1, 2]
    but was not
    difference
[1, *]

Additional Info:
    Some additional context");
        }

        [Fact]
        public void ShouldPass()
        {
            GetEnumerable().ShouldBe(new[] { 1 });
        }

        private static IEnumerable<int> GetEnumerable()
        {
            yield return 1;
        }
    }
}
