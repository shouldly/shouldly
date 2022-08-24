using Shouldly.Tests.Strings;

namespace Shouldly.Tests.ShouldContain
{
    public class PredicateClosureScenario
    {
        [Fact]
        public void PredicateClosureScenarioShouldFail()
        {
            var capturedOuterVar = 4;
            var arr = new[] { 1, 2, 3 };
            Verify.ShouldFail(() =>
arr.ShouldContain(i => i > capturedOuterVar, "Some additional context"),

errorWithSource:
@"arr
    should contain an element satisfying the condition
(i > capturedOuterVar)
    but does not

Additional Info:
    Some additional context",

errorWithoutSource:
@"[1, 2, 3]
    should contain an element satisfying the condition
(i > capturedOuterVar)
    but does not

Additional Info:
    Some additional context");
        }
    }
}