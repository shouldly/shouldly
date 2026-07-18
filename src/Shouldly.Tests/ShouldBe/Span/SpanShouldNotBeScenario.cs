namespace Shouldly.Tests.ShouldBe.Span;

public class SpanShouldNotBeScenario
{
    [Fact]
    public void SpanShouldNotBeScenarioShouldFail()
    {
        Verify.ShouldFail(() =>
            new[] { 1, 2, 3 }.AsSpan().ShouldNotBe([1, 2, 3], "Some additional context"));
    }

    [Fact]
    public void ShouldPass()
    {
        new[] { 1, 2, 3 }.AsSpan().ShouldNotBe([1, 2, 4]);
    }
}
