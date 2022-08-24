namespace Shouldly.Tests.ShouldNotBe.WithTolerance
{
    public class DateTimeScenario
    {
        [Fact]
        public void DateTimeScenarioShouldFail()
        {
            var date = new DateTime(2000, 6, 1);
            var dateString = date.ToString("o");
            var expected = new DateTime(2000, 6, 1, 1, 0, 1);
            var expectedString = expected.ToString("o");

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
            var date = new DateTime(2000, 6, 1);
            date.ShouldNotBe(new DateTime(2000, 6, 1, 1, 0, 1), TimeSpan.FromHours(1));
        }
    }
}
