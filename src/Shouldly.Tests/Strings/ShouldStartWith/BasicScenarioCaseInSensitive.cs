using Xunit;

namespace Shouldly.Tests.Strings.ShouldStartWith
{
    public class BasicScenarioCaseInSensitive
    {
        [Fact]
        public void BasicScenarioCaseInSensitiveShouldFail()
        {
            Verify.ShouldFail(() =>
    "Cheese".ShouldStartWith("Ce", Case.Insensitive, "Some additional context"),

errorWithSource:
@"""Cheese""
    should start with
""Ce""
    but was not

Additional Info:
    Some additional context",

    errorWithoutSource:
@"""Cheese""
    should start with
""Ce""
    but was not

Additional Info:
    Some additional context");
        }

        [Fact]
        public void ShouldPass()
        {
            "Cheese".ShouldStartWith("CH", Case.Insensitive);
        }
    }
}