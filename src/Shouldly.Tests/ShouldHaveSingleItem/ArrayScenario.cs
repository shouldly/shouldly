namespace Shouldly.Tests.ShouldHaveSingleItem;

public class ArrayScenario
{
    [Fact]
    public void ArrayScenarioShouldFail()
    {
        Verify.ShouldFail(() =>
            new[] { 1, 2 }.ShouldHaveSingleItem("Some additional context"));
    }

    [Fact]
    public void ShouldPass()
    {
        var result = new[] { 1 }.ShouldHaveSingleItem();
        result.ShouldBe(1);
    }
}