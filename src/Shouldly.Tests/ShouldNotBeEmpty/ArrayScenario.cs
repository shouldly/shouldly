namespace Shouldly.Tests.ShouldNotBeEmpty;

public class ArrayScenario
{
    [Fact]
    public void ArrayScenarioShouldFail()
    {
        Verify.ShouldFail(() =>
            new int[0].ShouldNotBeEmpty("Some additional context"));
    }

    [Fact]
    public void ShouldPass()
    {
        new[] { 1 }.ShouldNotBeEmpty();
    }
}