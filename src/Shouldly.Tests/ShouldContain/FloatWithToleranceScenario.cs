namespace Shouldly.Tests.ShouldContain;

public class FloatWithToleranceScenario
{
    [Fact]
    [UseCulture("en-US")]
    public void FloatWithToleranceScenarioShouldFail()
    {
        Verify.ShouldFail(() =>
            new[] { 1f, 2f, 3f }.ShouldContain(1.8f, 0.1d, "Some additional context"));
    }

    [Fact]
    public void ShouldPass()
    {
        new[] { 1f, 2f, 3f }.ShouldContain(1.91f, 0.1d);
    }

    [Fact]
    public void ShouldPassWithZeroTolerance()
    {
        new[] { 1f, 2f, 3f }.ShouldContain(2f, 0d);
    }

    [Fact]
    public void ShouldPassWhenDifferenceEqualsTolerance()
    {
        new[] { 1f, 2f, 3f }.ShouldContain(1.5f, 0.5d);
    }
}