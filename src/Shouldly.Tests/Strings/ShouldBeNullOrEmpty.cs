using Xunit;

namespace Shouldly.Tests.Strings
{
    public class ShouldBeNullOrEmpty
    {
        [Fact]
        public void SingleLetterShouldFail()
        {
            Verify.ShouldFail(() => 
"a".ShouldBeNullOrEmpty("Some additional context"),
@"""a""
    should be null or empty

Additional Info:
    Some additional context",
@"""a""
    should be null or empty

Additional Info:
    Some additional context");
        }

        [Fact]
        public void SingleLetterAsVariableShouldFail()
        {
            var singleLetter = "a";

            Verify.ShouldFail(() =>
singleLetter.ShouldBeNullOrEmpty("Some additional context"),
@"singleLetter (""a"")
    should be null or empty

Additional Info:
    Some additional context",
@"""a""
    should be null or empty

Additional Info:
    Some additional context");
        }

        [Fact]
        public void ShouldPass()
        {
            ((string)null).ShouldBeNullOrEmpty();
            string.Empty.ShouldBeNullOrEmpty();
        }
    }
}