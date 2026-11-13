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

    // The difference is beyond what a TimeSpan holds, so beyond any tolerance: the assertion fails rather than
    // overflowing.
    [Fact]
    public void ValuesTooFarApartToSubtractShouldFail()
    {
        Should.Throw<ShouldAssertException>(() => TimeSpan.MaxValue.ShouldBe(TimeSpan.MinValue, TimeSpan.FromTicks(1)));
        Should.Throw<ShouldAssertException>(() => TimeSpan.MinValue.ShouldBe(TimeSpan.MaxValue, TimeSpan.MaxValue));
        TimeSpan.MaxValue.ShouldNotBe(TimeSpan.MinValue, TimeSpan.MaxValue);
    }

    // A difference of exactly TimeSpan.MinValue is a tick longer than TimeSpan.MaxValue, the longest tolerance.
    [Fact]
    public void DifferenceOfMinValueShouldFail()
    {
        Should.Throw<ShouldAssertException>(() => TimeSpan.MinValue.ShouldBe(TimeSpan.Zero, TimeSpan.MaxValue));
        TimeSpan.MinValue.ShouldNotBe(TimeSpan.Zero, TimeSpan.MaxValue);
    }
}