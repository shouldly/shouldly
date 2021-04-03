using Shouldly.Tests.Strings;
using Xunit;

namespace Shouldly.Tests.ShouldNotBe
{
    public class StringScenario
    {
        private const string ThisOtherString = "this other string";
        private const string ThisString = "this string";

        [Fact]
        public void StringScenarioShouldFail()
        {
            Verify.ShouldFail(() =>
ThisString.ShouldNotBe(ThisString),

errorWithSource:
@"ThisString
    should not be
""this string""
    but was",

errorWithoutSource:
@"""this string""
    should not be
""this string""
    but was");
        }

        [Fact]
        public void ShouldPass()
        {
            ThisString.ShouldNotBe(ThisOtherString);
        }
    }
}