namespace Shouldly.Tests.ShouldBe.EnumerableType;

public class IntegerArrayScenario
{
    [Fact]
    public void IntegerArrayScenarioShouldFail()
    {
        Verify.ShouldFail(() =>
            new[] { 99, 2, 3, 5 }.ShouldBe([1, 2, 3, 4], "Some additional context"));
    }

    [Fact]
    public void ShouldPass()
    {
        new[] { 1, 2, 3, 4 }.ShouldBe([1, 2, 3, 4]);
    }
}