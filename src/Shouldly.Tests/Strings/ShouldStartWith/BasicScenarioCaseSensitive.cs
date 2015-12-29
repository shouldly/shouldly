using Xunit;

namespace Shouldly.Tests.Strings.ShouldStartWith
{
    public class BasicScenarioCaseSensitive
    {
        [Fact]
        public void BasicScenarioCaseSensitiveShouldFail()
        {
            Verify.ShouldFail(() =>
    "Cheese".ShouldStartWith("cH", Case.Sensitive),

errorWithSource:
@"""Cheese""
    should start with
""cH""
    but was not",

errorWithoutSource:
@"""Cheese""
    should start with
""cH""
    but was not");
        }

        [Fact]
        public void ShouldPass()
        {
            "Cheese".ShouldStartWith("Ch", Case.Sensitive);
        }
    }
}