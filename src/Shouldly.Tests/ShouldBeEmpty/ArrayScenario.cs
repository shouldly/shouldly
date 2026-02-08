namespace Shouldly.Tests.ShouldBeEmpty;

public class ArrayScenario
{
    [Fact]
    public void ArrayScenarioShouldFail()
    {
        Verify.ShouldFail(() =>
            new[] { 1 }.ShouldBeEmpty("Some additional context"));
    }

    [Fact]
    public void ShouldPass()
    {
        new int[0].ShouldBeEmpty();
    }
}