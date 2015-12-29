using System;
using Shouldly.Tests.Strings;
using Xunit;

namespace Shouldly.Tests.ShouldBe.WithTolerance
{
    public class DecimalScenario
    {
        [Fact]
        public void DecimalScenarioShouldFail()
        {
            const decimal pi = (decimal)Math.PI;
            Verify.ShouldFail(() =>
pi.ShouldBe(3.24m, 0.01m, "Some additional context"),

errorWithSource:
@"pi
    should be within
0.01
    of
3.24
    but was
3.14159265358979

Additional Info:
    Some additional context",

errorWithoutSource:
@"3.14159265358979
    should be within
0.01
    of
3.24
    but was not

Additional Info:
    Some additional context");
        }

        [Fact]
        public void ShouldPass()
        {
            const decimal pi = (decimal)Math.PI;
            pi.ShouldBe(3.14m, 0.01m);
        }
    }
}