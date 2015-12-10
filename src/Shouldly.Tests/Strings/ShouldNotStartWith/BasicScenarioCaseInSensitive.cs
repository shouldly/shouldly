using Xunit;

namespace Shouldly.Tests.Strings.ShouldNotStartWith
{
    public class BasicScenarioCaseInSensitive
    {
        [Fact]
        public void BasicScenarioCaseInSensitiveShouldFail()
        {
            Verify.ShouldFail(() =>
    "Cheese".ShouldNotStartWith("cH", Case.Insensitive),

errorWithSource:
@"""Cheese""
    should not start with
""cH""
    but was
""Cheese""",

errorWithoutSource:
@"""Cheese""
    should not start with
""cH""
    but was
""Cheese""");
        }

        [Fact]
        public void ShouldPass()
        {
            "Cheese".ShouldNotStartWith("Ce", Case.Insensitive);
        }
    }
}