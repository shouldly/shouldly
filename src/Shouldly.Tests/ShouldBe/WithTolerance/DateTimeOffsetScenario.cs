using System;
using System.Globalization;
using System.Threading;
using Shouldly.Tests.Strings;
using Xunit;

namespace Shouldly.Tests.ShouldBe.WithTolerance
{
    public class DateTimeOffsetScenario
    {
        [Fact]
        public void DateTimeOffsetScenarioShouldFail()
        {
#if DNX451
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-GB");
#elif DNXCORE50
            CultureInfo.CurrentCulture = new CultureInfo("en-GB");
#endif
            var date = new DateTimeOffset(new DateTime(2000, 6, 1), TimeSpan.Zero);
            var exptected = new DateTimeOffset(new DateTime(2000, 6, 1, 1, 0, 1), TimeSpan.Zero);
            Verify.ShouldFail(() =>
date.ShouldBe(exptected, TimeSpan.FromHours(1), "Some additional context"),

errorWithSource:
@"date
    should be within
01:00:00
    of
01/06/2000 01:00:01 +00:00
    but was
01/06/2000 00:00:00 +00:00

Additional Info:
    Some additional context",

errorWithoutSource:
@"01/06/2000 00:00:00 +00:00
    should be within
01:00:00
    of
01/06/2000 01:00:01 +00:00
    but was not

Additional Info:
    Some additional context");
        }

        [Fact]
        public void ShouldPass()
        {
            var date = new DateTimeOffset(new DateTime(2000, 6, 1), TimeSpan.Zero);
            date.ShouldBe(new DateTimeOffset(new DateTime(2000, 6, 1, 1, 0, 1), TimeSpan.Zero), TimeSpan.FromHours(1.5));
        }
    }
}