namespace Shouldly.Tests.ShouldBe.WithTolerance;

public class TimeSpanScenario
{
    [Fact]
    public void TimeSpanScenarioShouldFail()
    {
        var timeSpan = TimeSpan.FromHours(1);
        Verify.ShouldFail(() =>
                timeSpan.ShouldBe(timeSpan.Add(TimeSpan.FromHours(1.1d)), TimeSpan.FromHours(1), "Some additional context"),

            errorWithSource:
            """
            timeSpan
                should be within
            01:00:00
                of
            02:06:00
                but was
            01:00:00

            Additional Info:
                Some additional context
            """,

            errorWithoutSource:
            """
            01:00:00
                should be within
            01:00:00
                of
            02:06:00
                but was not

            Additional Info:
                Some additional context
            """);
    }

    [Fact]
    public void ShouldPass()
    {
        var timeSpan = TimeSpan.FromHours(1);
        timeSpan.ShouldBe(timeSpan.Add(TimeSpan.FromHours(1.1d)), TimeSpan.FromHours(1.5d));
    }
}