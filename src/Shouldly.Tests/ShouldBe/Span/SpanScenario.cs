namespace Shouldly.Tests.ShouldBe.Span;

public class SpanScenario
{
    [Fact]
    public void SpanScenarioShouldFail()
    {
        Verify.ShouldFail(() =>
            new[] { 99, 2, 3, 5 }.AsSpan().ShouldBe([1, 2, 3, 4], "Some additional context"));
    }

    [Fact]
    public void ShouldPass()
    {
        new[] { 1, 2, 3, 4 }.AsSpan().ShouldBe([1, 2, 3, 4]);
    }
}
