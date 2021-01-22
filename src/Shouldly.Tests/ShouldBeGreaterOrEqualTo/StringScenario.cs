using Shouldly.Tests.Strings;
using Xunit;

namespace Shouldly.Tests.ShouldBeGreaterOrEqualTo
{
    public class StringScenario
    {
        [Fact]
        public void StringScenarioShouldFail()
        {
            var aVar = "a";
            Verify.ShouldFail(() =>
aVar.ShouldBeGreaterThanOrEqualTo("b", "Some additional context"),

errorWithSource:
@"aVar
    should be greater than or equal to
""b""
    but was
""a""

Additional Info:
    Some additional context",

errorWithoutSource:
@"""a""
    should be greater than or equal to
""b""
    but was not

Additional Info:
    Some additional context");
        }

        [Fact]
        public void ShouldPass()
        {
            "a".ShouldBeGreaterThanOrEqualTo("a");
        }

        [Fact]
        public void ShouldFailDate()
        {
            var oldDate = Convert.ToDateTime("22/1/2020 4:44:01.210 PM");
            var newDate = Convert.ToDateTime("22/1/2020 4:44:01.209 PM");
            old.ShouldBeGreaterThanOrEqualTo(newDate);
        }
    }
}