namespace Shouldly.Tests.ShouldNotBe.WithTolerance;

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
            messageScrubber: msg => msg.Replace(dateString, "<date>").Replace(expectedString, "<expected>"));
    }

    [Fact]
    public void ShouldPass()
    {
        var date = new DateTimeOffset(new DateTime(2000, 6, 1), TimeSpan.Zero);
        date.ShouldNotBe(new(new DateTime(2000, 6, 1, 1, 0, 1), TimeSpan.Zero), TimeSpan.FromHours(1));
    }
}