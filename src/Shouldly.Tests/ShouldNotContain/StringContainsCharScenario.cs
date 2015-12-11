using Shouldly.Tests.Strings;
using Xunit;

namespace Shouldly.Tests.ShouldNotContain
{
    public class StringContainsCharScenario
    {
        const string Target = "Foo";

        [Fact]
        public void StringContainsCharScenarioShouldFail()
        {
            Verify.ShouldFail(() =>
Target.ShouldNotContain('F', "Some additional context"),

errorWithSource:
@"Target
    should not contain
F
    but was actually
""Foo""

Additional Info:
    Some additional context",

errorWithoutSource:
@"""Foo""
    should not contain
F
    but did

Additional Info:
    Some additional context");
        }

        [Fact]
        public void ShouldPass()
        {
            Target.ShouldNotContain('B');
        }
    }
}