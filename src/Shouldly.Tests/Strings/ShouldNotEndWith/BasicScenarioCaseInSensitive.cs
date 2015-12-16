using Xunit;

namespace Shouldly.Tests.Strings.ShouldNotEndWith
{
    public class BasicScenarioCaseInSensitive
    {
        [Fact]
        public void BasicScenarioCaseInSensitiveShouldFail()
        {
            Verify.ShouldFail(() =>
"Cheese".ShouldNotEndWith("SE", "Some additional context", Case.Insensitive),

errorWithSource:
@"""Cheese""
    should not end with
""SE""
    but was

Additional Info:
    Some additional context",

errorWithoutSource:
@"""Cheese""
    should not end with
""SE""
    but was

Additional Info:
    Some additional context");
        }

        [Fact]
        public void ShouldPass()
        {
            "Cheese".ShouldNotEndWith("ze", Case.Insensitive);
        }
    }
}