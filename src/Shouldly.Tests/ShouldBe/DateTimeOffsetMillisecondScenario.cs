namespace Shouldly.Tests.ShouldBe;

public class DateTimeOffsetMillisecondScenario
{
    [Fact]
    public void DateTimeOffsetsDifferingByMillisecondsShouldFailWithDistinguishableMessage()
    {
        var actual = new DateTimeOffset(2000, 1, 1, 0, 0, 0, 123, TimeSpan.Zero);
        var expected = new DateTimeOffset(2000, 1, 1, 0, 0, 0, 456, TimeSpan.Zero);

        Verify.ShouldFail(() =>
            actual.ShouldBe(expected));
    }
}
