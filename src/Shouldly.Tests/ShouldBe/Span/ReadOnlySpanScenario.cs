namespace Shouldly.Tests.ShouldBe.Span;

public class ReadOnlySpanScenario
{
    [Fact]
    public void ReadOnlySpanScenarioShouldFail()
    {
        Verify.ShouldFail(() =>
            "hello".AsSpan().ShouldBe("world".AsSpan(), "Some additional context"));
    }

    [Fact]
    public void ShouldPass()
    {
        "hello".AsSpan().ShouldBe("hello".AsSpan());
    }
}
