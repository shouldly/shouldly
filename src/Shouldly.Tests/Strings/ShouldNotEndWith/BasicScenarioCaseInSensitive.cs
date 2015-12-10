using Xunit;

namespace Shouldly.Tests.Strings.ShouldNotEndWith
{
    public class BasicScenarioCaseInSensitive
    {
        [Fact]
        public void BasicScenarioCaseInSensitiveShouldFail()
        {
            Verify.ShouldFail(() =>
"Cheese".ShouldNotEndWith("SE", Case.Insensitive),

errorWithSource:
@"""Cheese""
    should not end with
""SE""
    but was
""Cheese""",

errorWithoutSource:
@"""Cheese""
    should not end with
""SE""
    but was
""Cheese""");
        }

        [Fact]
        public void ShouldPass()
        {
            "Cheese".ShouldNotEndWith("ze", Case.Insensitive);
        }
    }
}