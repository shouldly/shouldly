using Xunit;

namespace Shouldly.Tests.Strings.ShouldNotEndWith
{
    public class BasicScenarioCaseSensitive
    {
        [Fact]
        public void BasicScenarioCaseSensitiveShouldFail()
        {
            Verify.ShouldFail(() =>
"Cheese".ShouldNotEndWith("se", "Some additional context", Case.Sensitive),

errorWithSource:
@"""Cheese""
    should not end with
""se""
    but was

Additional Info:
    Some additional context",

errorWithoutSource:
@"""Cheese""
    should not end with
""se""
    but was

Additional Info:
    Some additional context");
        }

        [Fact]
        public void ShouldPass()
        {
            "Cheese".ShouldNotEndWith("SE", Case.Sensitive);
        }
    }
}