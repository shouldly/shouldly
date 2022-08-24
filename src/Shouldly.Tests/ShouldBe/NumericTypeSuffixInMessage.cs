using Shouldly.Tests.Strings;

namespace Shouldly.Tests.ShouldBe
{
    public class NumericTypeSuffixInMessage
    {
        [Fact]
        public void ShouldPass()
        {
            2.0f.ShouldBe(2UL);
        }

        [Fact]
        public void NumericTypeSuffixInMessageInErrorMessage()
        {
            const ulong uLong = 2UL;
            Verify.ShouldFail(() =>
uLong.ShouldBe(3UL),

errorWithSource:
@"uLong
    should be
3uL
    but was
2uL",

errorWithoutSource:
@"2uL
    should be
3uL
    but was not");
        }
    }
}
