using Xunit;

namespace Shouldly.Tests.Strings.ShouldBeEmpty
{
    public class ActualIsNull
    {
        [Fact]
        public void ActualIsNullShouldFail()
        {
            var str = (string?)null;

            Verify.ShouldFail(() =>
str.ShouldBeEmpty("Some additional context"),

errorWithSource:
@"str
    should be empty but was
null

Additional Info:
    Some additional context",

errorWithoutSource:
@"null
    should be empty but was not empty

Additional Info:
    Some additional context");
        }
    }
}
