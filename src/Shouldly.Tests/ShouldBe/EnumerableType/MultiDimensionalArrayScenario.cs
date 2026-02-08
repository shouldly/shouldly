namespace Shouldly.Tests.ShouldBe.EnumerableType;

public class MultiDimensionalArrayScenario
{
    [Fact]
    public void MultiDimensionalArrayScenarioShouldFail()
    {
        // TODO Multidimensional arrays are not outputted correctly?
        Verify.ShouldFail(() =>
            new[,] { { "1", "2" }, { "3", "5" } }.ShouldBe(new[,] { { "1", "2" }, { "3", "4" } }, "Some additional context"));
    }

    [Fact]
    public void ShouldPass()
    {
        new[,] { { "1", "2" }, { "3", "4" } }.ShouldBe(new[,] { { "1", "2" }, { "3", "4" } });
    }
}