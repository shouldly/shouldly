namespace Shouldly.Tests.ShouldNotContain
{
    public class PredicateScenario
    {
        [Fact]
        public void PredicateScenarioShouldFail()
        {
            Verify.ShouldFail(() =>
new[] { 1, 2, 3 }.ShouldNotContain(i => i < 4, "Some additional context"),

errorWithSource:
@"new[] { 1, 2, 3 }
    should not contain an element satisfying the condition
(i < 4)
    but does

Additional Info:
    Some additional context",

errorWithoutSource:
@"[1, 2, 3]
    should not contain an element satisfying the condition
(i < 4)
    but does

Additional Info:
    Some additional context");
        }

        [Fact]
        public void ShouldPass()
        {
            new[] { 1, 2, 3 }.ShouldNotContain(i => i > 3);
        }
    }
}