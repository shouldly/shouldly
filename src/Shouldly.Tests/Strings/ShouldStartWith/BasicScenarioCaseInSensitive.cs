using Xunit;

namespace Shouldly.Tests.Strings.ShouldStartWith
{
    public class BasicScenarioCaseInSensitive
    {
        [Fact]
        public void BasicScenarioCaseInSensitiveShouldFail()
        {
            Verify.ShouldFail(() =>
    "Cheese".ShouldStartWith("Ce", Case.Insensitive),

errorWithSource:
@"""Cheese""
    should start with
""Ce""
    but was
""Cheese""",

    errorWithoutSource:
@"""Cheese""
    should start with
""Ce""
    but was
""Cheese""");
        }

        [Fact]
        public void ShouldPass()
        {
            "Cheese".ShouldStartWith("CH", Case.Insensitive);
        }
    }
}