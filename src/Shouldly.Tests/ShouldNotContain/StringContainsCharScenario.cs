using Shouldly.Tests.Strings;
using Xunit;

namespace Shouldly.Tests.ShouldNotContain
{
    public class StringContainsCharScenario
    {
        protected string target = "Foo";

        [Fact]
        public void StringContainsCharScenarioShouldFail()
        {
            Verify.ShouldFail(() =>
    target.ShouldNotContain('F', "Some additional context"),

    errorWithSource:
    @"target should not contain F
    but was
actually ""Foo""
Additional Info:
    Some additional context",

    errorWithoutSource:
    @"target should not contain F
    but was
actually ""Foo""
Additional Info:
    Some additional context");
        }

        [Fact]
        public void ShouldPass()
        {
            target.ShouldNotContain('B');
        }
    }
}