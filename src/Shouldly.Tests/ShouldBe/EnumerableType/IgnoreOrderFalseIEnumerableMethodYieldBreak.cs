using Shouldly.Tests.Strings;

namespace Shouldly.Tests.ShouldBe.EnumerableType
{
    public class IgnoreOrderFalseIEnumerableMethodYieldBreak
    {
        [Fact]
        public void IgnoreOrderFalseIEnumerableMethodYieldBreakShouldFail()
        {
            Verify.ShouldFail(() =>
GetEmptyEnumerable().ShouldBe(new[] { 2, 4 }, false, "Some additional context"),

errorWithSource:
@"GetEmptyEnumerable()
    should be
[2, 4]
    but was
[]
    difference
[*, *]

Additional Info:
    Some additional context",

errorWithoutSource:
@"[]
    should be
[2, 4]
    but was not
    difference
[*, *]

Additional Info:
    Some additional context");
        }

        [Fact]
        public void ShouldPass()
        {
            GetEmptyEnumerable().ShouldBe(new int[0]);
        }

        private static IEnumerable<int> GetEmptyEnumerable()
        {
            yield break;
        }
    }
}
