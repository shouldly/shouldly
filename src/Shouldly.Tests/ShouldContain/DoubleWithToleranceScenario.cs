namespace Shouldly.Tests.ShouldContain;

public class DoubleWithToleranceScenario
{
    [Fact]
    [UseCulture("en-US")]
    public void DoubleWithToleranceScenarioShouldFail()
    {
        Verify.ShouldFail(() =>
            new[] { 1d, 2d, 3d }.ShouldContain(1.8, 0.1d, "Some additional context"));
    }

    [Fact]
    public void ShouldPass()
    {
        new[] { 1d, 2d, 3d }.ShouldContain(1.91d, 0.1d);
    }
}