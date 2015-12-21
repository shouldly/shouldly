using Xunit;

namespace Shouldly.Tests.Strings
{
    public class ShouldNotMatch
    {
        [Fact]
        public void ShouldNotMatchShouldFail()
        {
            Verify.ShouldFail(() =>
"Cheese".ShouldNotMatch(@"\w+", "Some additional context"),

errorWithSource:
@"""Cheese"" should not match ""\w+"" but did

Additional Info:
    Some additional context",

errorWithoutSource:
@"""Cheese"" should not match ""\w+"" but did

Additional Info:
    Some additional context");
        }

        [Fact]
        public void ShouldPass()
        {
            "Cheese".ShouldNotMatch(@"Cat");
        }
    }
}