namespace Shouldly.Tests.ShouldBe.WithTolerance;

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
            date.ShouldBe(expected, TimeSpan.FromHours(1), "Some additional context"));
    }

    [Fact]
    public void ShouldPass()
    {
        var date = new DateTimeOffset(new DateTime(2000, 6, 1), TimeSpan.Zero);
        date.ShouldBe(new(new DateTime(2000, 6, 1, 1, 0, 1), TimeSpan.Zero), TimeSpan.FromHours(1.5));
    }

    [Fact]
    public void ShouldPassWithZeroTolerance()
    {
        var date = new DateTimeOffset(new DateTime(2000, 6, 1), TimeSpan.Zero);
        date.ShouldBe(date, TimeSpan.Zero);
    }
}