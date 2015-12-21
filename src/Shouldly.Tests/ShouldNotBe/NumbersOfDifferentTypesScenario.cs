using Shouldly.Tests.Strings;
using Xunit;

namespace Shouldly.Tests.ShouldNotBe
{
    public class NumbersOfDifferentTypesScenario
    {
        [Fact]
        public void NumbersOfDifferentTypesScenarioShouldFail()
        {
            const long aLong = 1L;
            Verify.ShouldFail(() =>
aLong.ShouldNotBe(1),

errorWithSource:
@"aLong
    should not be
1
    but was",

errorWithoutSource:
@"1
    should not be
1
    but was");
        }

        [Fact]
        public void ShouldPass()
        {
            1L.ShouldNotBe(2);
        }
    }
}