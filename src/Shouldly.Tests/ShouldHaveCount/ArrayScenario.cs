namespace Shouldly.Tests.ShouldHaveCount;

public class ArrayScenario
{
    [Fact]
    public void ArrayScenarioShouldFail()
    {
        var testList = new[] { 1, 2 };
        Verify.ShouldFail(() =>
            testList.ShouldHaveCount(3, "Some additional context"));
    }

    [Fact]
    public void ShouldFailWithSingleExpected()
    {
        Verify.ShouldFail(() =>
            new[] { 1, 2 }.ShouldHaveCount(1, "Some additional context"));
    }

    [Fact]
    public void ShouldFailWithSingleActual()
    {
        Verify.ShouldFail(() =>
            new[] { 1 }.ShouldHaveCount(3, "Some additional context"));
    }

    [Fact]
    public void ShouldPass()
    {
        new[] { 1, 2 }.ShouldHaveCount(2);
    }
}
