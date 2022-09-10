namespace Shouldly.Tests.ShouldBe.WithTolerance;

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
                date.ShouldBe(expected, TimeSpan.FromHours(1), "Some additional context"),

            errorWithSource:
            $@"date
    should be within
01:00:00
    of
{expectedString}
    but was
{dateString}

Additional Info:
    Some additional context",

            errorWithoutSource:
            $@"{dateString}
    should be within
01:00:00
    of
{expectedString}
    but was not

Additional Info:
    Some additional context");
    }

    [Fact]
    public void DateTimeFromTicksScenarioShouldFailAndShowDetailedDateDifference()
    {
        var date = new DateTime(635961688375100000);
        var dateString = date.ToString("o");
        var expected = new DateTime(635961688375106000);
        var expectedString = expected.ToString("o");

        Verify.ShouldFail(() =>
                date.ShouldBe(expected, "Some additional context"),

            errorWithSource:
            $@"date
    should be
{expectedString}
    but was
{dateString}

Additional Info:
    Some additional context",

            errorWithoutSource:
            $@"{dateString}
    should be
{expectedString}
    but was not

Additional Info:
    Some additional context");
    }

    [Fact]
    public void ShouldPass()
    {
        var date = new DateTime(2000, 6, 1);
        date.ShouldBe(new(2000, 6, 1, 1, 0, 1), TimeSpan.FromHours(1.5d));
    }
}