namespace Shouldly.Tests.ShouldBeSubsetOf;

public class FloatArrayScenario
{
    [Fact]
    public void FloatArrayScenarioShouldFail()
    {
        Verify.ShouldFail(() =>
            new[] { 1f, 2f, 5f }.ShouldBeSubsetOf([2f, 3f, 4f], "Some additional context"));
    }

    [Fact]
    public void ShouldPass()
    {
        new[] { 1f }.ShouldBeSubsetOf([1f, 2f, 3f]);
    }
}