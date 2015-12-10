using Xunit;

namespace Shouldly.Tests.Strings.ShouldNotStartWith
{
    public class BasicScenarioCaseSensitive
    {
        [Fact]
        public void BasicScenarioCaseSensitiveShouldFail()
        {
            Verify.ShouldFail(() =>
    "Cheese".ShouldNotStartWith("Ch", Case.Sensitive),

errorWithSource:
@"""Cheese""
    should not start with
""Ch""
   
    but was
""Cheese""",

errorWithoutSource:
@"""Cheese""
    should not start with
""Ch""
   
    but was
""Cheese""");
        }

        [Fact]
        public void ShouldPass()
        {
            "Cheese".ShouldNotStartWith("CH", Case.Sensitive);
        }
    }
}