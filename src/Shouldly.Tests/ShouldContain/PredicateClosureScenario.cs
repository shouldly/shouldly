#if net40
using Shouldly.Tests.Strings;
using Xunit;

namespace Shouldly.Tests.ShouldContain
{
    public class PredicateClosureScenario
    {
        [Fact]
        public void PredicateClosureScenarioShouldFail()
        {
            var capturedOuterVar = 4;
            Verify.ShouldFail(() =>
new[] { 1, 2, 3 }.ShouldContain(i => i > capturedOuterVar, "Some additional context"),

errorWithSource:
@"new[] { 1, 2, 3 }
                should contain an element satisfying the condition
            (i > capturedOuterVar)
                but does not
            Additional Info:
            Some additional context",

errorWithoutSource:
@"new[] { 1, 2, 3 }
                should contain an element satisfying the condition
            (i > capturedOuterVar)
                but does not
            Additional Info:
            Some additional context");
        }
    }
}
#endif