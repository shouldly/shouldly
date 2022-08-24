using Shouldly.Tests.Strings;

namespace Shouldly.Tests.ShouldBe.WithTolerance
{
    public class DateTimeOffsetScenario
    {
        [Fact]
        public void DateTimeOffsetScenarioShouldFail()
        {
            var date = new DateTimeOffset(new DateTime(2000, 6, 1), TimeSpan.Zero);
            var dateString = date.ToString();
            var expected = new DateTimeOffset(new DateTime(2000, 6, 1, 1, 0, 1), TimeSpan.Zero);
            var expectedDate = expected.ToString();
            Verify.ShouldFail(() =>
date.ShouldBe(expected, TimeSpan.FromHours(1), "Some additional context"),

errorWithSource:
$@"date
    should be within
01:00:00
    of
{expectedDate}
    but was
{dateString}

Additional Info:
    Some additional context",

errorWithoutSource:
$@"{dateString}
    should be within
01:00:00
    of
{expectedDate}
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