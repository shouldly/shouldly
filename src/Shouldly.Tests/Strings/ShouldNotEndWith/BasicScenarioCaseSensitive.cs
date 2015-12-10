using Xunit;

namespace Shouldly.Tests.Strings.ShouldNotEndWith
{
    public class BasicScenarioCaseSensitive
    {
        [Fact]
        public void BasicScenarioCaseSensitiveShouldFail()
        {
            Verify.ShouldFail(() =>
"Cheese".ShouldNotEndWith("se", Case.Sensitive),

errorWithSource:
@"""Cheese""
    should not end with
""se""
    but was
""Cheese""",

errorWithoutSource:
@"""Cheese""
    should not end with
""se""
    but was
""Cheese""");
        }

        [Fact]
        public void ShouldPass()
        {
            "Cheese".ShouldNotEndWith("SE", Case.Sensitive);
        }
    }
}