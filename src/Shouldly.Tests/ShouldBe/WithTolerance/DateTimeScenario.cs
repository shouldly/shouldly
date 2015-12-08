using System;
using System.Globalization;
using System.Threading;
using Shouldly.Tests.Strings;
using Xunit;

namespace Shouldly.Tests.ShouldBe.WithTolerance
{
    public class DateTimeScenario
    {
        [Fact]
        public void DateTimeScenarioShouldFail()
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-GB");
            var date = new DateTime(2000, 6, 1);
            var expected = new DateTime(2000, 6, 1, 1, 0, 1);
            Verify.ShouldFail(() =>
date.ShouldBe(expected, TimeSpan.FromHours(1), "Some additional context"),

errorWithSource:
@"date
    should be within
01:00:00
    of
01/06/2000 01:00:01
    but was
01/06/2000 00:00:00

Additional Info:
    Some additional context",

errorWithoutSource:
@"01/06/2000 00:00:00
    should be within
01:00:00
    of
01/06/2000 01:00:01
    but was not

Additional Info:
    Some additional context");
        }

        [Fact]
        public void ShouldPass()
        {
            var date = new DateTime(2000, 6, 1);
            date.ShouldBe(new DateTime(2000, 6, 1, 1, 0, 1), TimeSpan.FromHours(1.5d));
        }
    }
}
