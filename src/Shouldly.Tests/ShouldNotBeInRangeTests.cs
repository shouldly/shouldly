using Shouldly.Tests.Strings;
using Xunit;

namespace Shouldly.Tests
{
    public class ShouldNotBeInRangeTests
    {
        [Fact]
        public void ShouldNotBeInRangeTestsShouldFail()
        {
            var @decimal = 1.5m;
            Verify.ShouldFail(() =>
@decimal.ShouldNotBeInRange(1.4m, 1.6m, "Some additional context"),

errorWithSource:
@"@decimal
    should not be in range
{ from = 1.4, to = 1.6 }
    but was
1.5

Additional Info:
    Some additional context",

errorWithoutSource:
@"1.5
    should not be in range
{ from = 1.4, to = 1.6 }
    but was

Additional Info:
    Some additional context");
        }

        [Fact]
        public void ShouldPass()
        {
            1.5m.ShouldNotBeInRange(1.6m, 1.7m);
        }
    }
}
