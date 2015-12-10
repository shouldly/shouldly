using Xunit;

namespace Shouldly.Tests.Strings
{
    public class ShouldContainWithoutWhitespace
    {

        [Fact]
        public void ShouldContainWithoutWhitespaceShouldFail()
        {
            Verify.ShouldFail(() =>
"Fun   with space   and \"quotes\"".ShouldContainWithoutWhitespace("Fun with space and missing quotes", "Some additional context"),

errorWithSource:
@"""Fun   with space   and \\""quotes\\""
    should contain without whitespace
""Fun with space and missing quotes""
   
    but was
actually
""Fun   with space   and ""quotes""""

Additional Info:
Some additional context",

errorWithoutSource:
@"""Fun   with space   and \\""quotes\\""
    should contain without whitespace
""Fun with space and missing quotes""
   
    but was
actually
""Fun   with space   and ""quotes""""

Additional Info:
Some additional context");
        }

        [Fact]
        public void ShouldPass()
        {
            "Fun   with space   and \"quotes\"".ShouldContainWithoutWhitespace("Fun with space and 'quotes'");
        }
    }
}