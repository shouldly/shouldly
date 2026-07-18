namespace Shouldly.Tests.ShouldNotBe.WithTolerance;

public class TimeSpanScenario
{
    [Fact]
    public void TimeSpanScenarioShouldFail()
    {
        var timeSpan = TimeSpan.FromHours(1);
        Verify.ShouldFail(() =>
            timeSpan.ShouldNotBe(timeSpan.Add(TimeSpan.FromHours(1.1d)), TimeSpan.FromHours(1.5d), "Some additional context"));
    }

    [Fact]
    public void ShouldPass()
    {
        var timeSpan = TimeSpan.FromHours(1);
        timeSpan.ShouldNotBe(timeSpan.Add(TimeSpan.FromHours(1.1d)), TimeSpan.FromHours(1));
    }
}