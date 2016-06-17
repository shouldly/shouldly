using Xunit;

namespace Shouldly.Tests.Strings
{
    public class ShouldNotBeNullOrEmpty
    {
        [Fact]
        public void SingleLetterShouldFail()
        {
            Verify.ShouldFail(() =>
"".ShouldNotBeNullOrEmpty(() => "Some additional context"),
errorWithSource: @"""""
    should not be null or empty

Additional Info:
    Some additional context",
errorWithoutSource: @"""""
    should not be null or empty

Additional Info:
    Some additional context");
        }

        [Fact]
        public void ShouldPass()
        {
            " ".ShouldNotBeNullOrEmpty();
        }
    }
}