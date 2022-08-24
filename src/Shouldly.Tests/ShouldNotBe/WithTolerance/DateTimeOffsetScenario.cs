namespace Shouldly.Tests.ShouldNotBe.WithTolerance
{
    public class DateTimeOffsetScenario
    {
        [Fact]
        public void DateTimeOffsetScenarioShouldFail()
        {
            var date = new DateTimeOffset(new DateTime(2000, 6, 1), TimeSpan.Zero);
            var dateString = date.ToString();
            var expected = new DateTimeOffset(new DateTime(2000, 6, 1, 1, 0, 1), TimeSpan.Zero);
            var expectedString = expected.ToString();

            Verify.ShouldFail(() =>
date.ShouldNotBe(expected, TimeSpan.FromHours(1.5), "Some additional context"),

errorWithSource:
$@"date
    should not be within
01:30:00
    of
{expectedString}
    but was
{dateString}

Additional Info:
    Some additional context",

errorWithoutSource:
$@"{dateString}
    should not be within
01:30:00
    of
{expectedString}
    but was not

Additional Info:
    Some additional context");
        }

        [Fact]
        public void ShouldPass()
        {
            var date = new DateTimeOffset(new DateTime(2000, 6, 1), TimeSpan.Zero);
            date.ShouldNotBe(new DateTimeOffset(new DateTime(2000, 6, 1, 1, 0, 1), TimeSpan.Zero), TimeSpan.FromHours(1));
        }
    }
}