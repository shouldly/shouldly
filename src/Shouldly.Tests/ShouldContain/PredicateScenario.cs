using Shouldly.Tests.Strings;

namespace Shouldly.Tests.ShouldContain
{
    public class PredicateScenario
    {
        [Fact]
        public void PredicateScenarioShouldFail()
        {
            Verify.ShouldFail(() =>
new[] { 1, 2, 3 }.ShouldContain(i => i > 4, "Some additional context"),

errorWithSource:
@"new[] { 1, 2, 3 }
    should contain an element satisfying the condition
(i > 4)
    but does not

Additional Info:
    Some additional context",

errorWithoutSource:
@"[1, 2, 3]
    should contain an element satisfying the condition
(i > 4)
    but does not

Additional Info:
    Some additional context");
        }

        [Fact]
        public void ShouldPass()
        {
            new[] { 1, 2, 3 }.ShouldContain(i => i < 3);
        }
    }
}