using Xunit;

#if net40
namespace Shouldly.Tests.Strings
{
    public class ShouldBeNullOrWhiteSpace
    {
        [Fact]
        public void StringWithOneLetterShouldFail()
        {
            Verify.ShouldFail(
            () =>
"a".ShouldBeNullOrWhiteSpace("Some additional context"),

@"""a""
    should be null or white space

Additional Info:
    Some additional context",

@"""a""
    should be null or white space

Additional Info:
    Some additional context");
        }

        [Fact]
        public void StringWithOneLetterAsVariableShouldFail()
        {
            var oneLetter = "a";
            Verify.ShouldFail(
            () =>
oneLetter.ShouldBeNullOrWhiteSpace("Some additional context"),

@"oneLetter (""a"")
    should be null or white space

Additional Info:
    Some additional context",

@"""a""
    should be null or white space

Additional Info:
    Some additional context");
        }

        [Fact]
        public void NullShouldPass()
        {
            ((string)null).ShouldBeNullOrWhiteSpace();
        }

        [Fact]
        public void EmptyStringShouldPass()
        {
            string.Empty.ShouldBeNullOrWhiteSpace();
        }

        [Fact]
        public void SpacesShouldPass()
        {
            "   ".ShouldBeNullOrWhiteSpace();
        }
    }
}
#endif