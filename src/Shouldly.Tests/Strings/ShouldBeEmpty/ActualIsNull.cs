using Xunit;

namespace Shouldly.Tests.Strings.ShouldBeEmpty
{
    public class ActualIsNull
    {
        [Fact]
        public void ActualIsNullShouldFail()
        {
            Verify.ShouldFail(() =>
((string)null).ShouldBeEmpty("Some additional context"),

errorWithSource:
@"(string)null should be empty but was null
Additional Info:
    Some additional context",

errorWithoutSource:
@"(string)null should be empty but was null
Additional Info:
    Some additional context");
        }
    }
}