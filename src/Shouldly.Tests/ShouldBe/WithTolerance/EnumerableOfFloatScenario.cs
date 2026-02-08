namespace Shouldly.Tests.ShouldBe.WithTolerance;

public class EnumerableOfFloatScenario
{
    [Fact]
    [UseCulture("en-US")]
    public void EnumerableOfFloatScenarioShouldFail()
    {
        Verify.ShouldFail(() =>
            new[] { (float)MathEx.PI, (float)MathEx.PI }.ShouldBe([3.24f, 3.24f], 0.01, "Some additional context"));
    }

    [Fact]
    public void ShouldPass()
    {
        new[] { (float)MathEx.PI, (float)MathEx.PI }.ShouldBe([3.14f, 3.14f], 0.01);
    }
}