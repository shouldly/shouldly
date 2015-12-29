using System;
using Shouldly.Tests.Strings;
using Xunit;

namespace Shouldly.Tests.ShouldBe.WithTolerance
{
    public class DoubleScenario
    {
        [Fact]
        public void DoubleScenarioShouldFail()
        {
            const double pi = Math.PI;
            Verify.ShouldFail(() =>
pi.ShouldBe(3.24d, 0.01d, "Some additional context"),

errorWithSource:
@"pi
    should be within
0.01d
    of
3.24d
    but was
3.14159265358979d

Additional Info:
    Some additional context",

    errorWithoutSource:
@"3.14159265358979d
    should be within
0.01d
    of
3.24d
    but was not

Additional Info:
    Some additional context");
        }

        [Fact]
        public void ShouldPass()
        {
            const double pi = Math.PI;
            pi.ShouldBe(3.14d, 0.01d);
        }
    }
}