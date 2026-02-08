namespace Shouldly.Tests;

public class ShouldNotBeInRangeTests
{
    [Fact]
    [UseCulture("en-US")]
    public void ShouldNotBeInRangeTestsShouldFail()
    {
        var @decimal = 1.5m;
        Verify.ShouldFail(() =>
            @decimal.ShouldNotBeInRange(1.4m, 1.6m, "Some additional context"));
    }

    [Fact]
    public void ShouldPass()
    {
        1.5m.ShouldNotBeInRange(1.6m, 1.7m);
    }
}