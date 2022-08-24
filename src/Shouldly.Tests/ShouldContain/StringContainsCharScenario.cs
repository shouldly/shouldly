using Shouldly.Tests.Strings;

namespace Shouldly.Tests.ShouldContain
{
    public class StringContainsCharScenario
    {
        private const string Target = "Foo";

        [Fact]
        public void StringContainsCharScenarioShouldFail()
        {
            Verify.ShouldFail(() =>
Target.ShouldContain('B', "Some additional context"),

errorWithSource:
@"Target
    should contain
B
    but was actually
""Foo""

Additional Info:
    Some additional context",

errorWithoutSource:
@"""Foo""
    should contain
B
    but did not

Additional Info:
    Some additional context");
        }

        [Fact]
        public void ShouldPass()
        {
            Target.ShouldContain('F');
        }
    }
}