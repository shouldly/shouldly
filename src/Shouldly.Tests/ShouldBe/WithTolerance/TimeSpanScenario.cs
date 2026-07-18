namespace Shouldly.Tests.ShouldBe.WithTolerance;

public class TimeSpanScenario
{
    [Fact]
    public void TimeSpanScenarioShouldFail()
    {
        var timeSpan = TimeSpan.FromHours(1);
        Verify.ShouldFail(() =>
            timeSpan.ShouldBe(timeSpan.Add(TimeSpan.FromHours(1.1d)), TimeSpan.FromHours(1), "Some additional context"));
    }

    [Fact]
    public void ShouldPass()
    {
        var timeSpan = TimeSpan.FromHours(1);
        timeSpan.ShouldBe(timeSpan.Add(TimeSpan.FromHours(1.1d)), TimeSpan.FromHours(1.5d));
    }

    [Fact]
    public void ShouldPassWithZeroTolerance()
    {
        var timeSpan = TimeSpan.FromHours(1);
        timeSpan.ShouldBe(timeSpan, TimeSpan.Zero);
    }

    [Fact]
    public void TimeSpanScenarioShouldFailWithZeroTolerance()
    {
        var timeSpan = TimeSpan.FromHours(1);
        Verify.ShouldFail(() =>
            timeSpan.ShouldNotBe(timeSpan, TimeSpan.Zero, "Some additional context"));
    }
}