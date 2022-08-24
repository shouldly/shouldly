using Shouldly.Tests.Strings;

namespace Shouldly.Tests.ShouldNotContain
{
    public class PredicateObjectScenario
    {
        [Fact]
        public void PredicateObjectScenarioShouldFail()
        {
            var a = new object();
            var b = new object();
            var c = new object();
            Verify.ShouldFail(() =>
new[] { a, b, c }.ShouldNotContain(o => o.GetType().FullName!.Equals("System.Object"),
                "Some additional context"),

errorWithSource:
@"new[] { a, b, c }
    should not contain an element satisfying the condition
o.GetType().FullName.Equals(""System.Object"")
    but does

Additional Info:
    Some additional context",

errorWithoutSource:
@"[System.Object (000000), System.Object (000000), System.Object (000000)]
    should not contain an element satisfying the condition
o.GetType().FullName.Equals(""System.Object"")
    but does

Additional Info:
    Some additional context");
        }

        [Fact]
        public void ShouldPass()
        {
            var a = new object();
            var b = new object();
            var c = new object();
            new[] { a, b, c }.ShouldNotContain(o => o.GetType().FullName!.Equals(""));
        }
    }
}