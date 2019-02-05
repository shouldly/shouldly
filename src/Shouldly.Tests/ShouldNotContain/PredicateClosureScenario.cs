using Shouldly.Tests.Strings;
using Xunit;

namespace Shouldly.Tests.ShouldNotContain
{
    public class PredicateClosureScenario
    {
        [Fact]
        public void PredicateClosureScenarioShouldFail()
        {
            var capturedOuterVar = 3;
            var arr = new[] { 1, 2, 3 };
            Verify.ShouldFail(() =>
arr.ShouldNotContain(i => i < capturedOuterVar, "Some additional context"),

errorWithSource:
@"arr
    should not contain an element satisfying the condition
(i < capturedOuterVar)
    but does
        [[0 => 1], [1 => 2]]

Additional Info:
    Some additional context",

errorWithoutSource:
@"[1, 2, 3]
    should not contain an element satisfying the condition
(i < capturedOuterVar)
    but does
        [[0 => 1], [1 => 2]]

Additional Info:
    Some additional context");
        }
    }
}