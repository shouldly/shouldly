namespace Shouldly.Tests.ShouldBeUnique;

public class LargeDatasetScenario
{
    [Fact]
    public void ShouldPass()
    {
        Enumerable.Range(1, 500000)
            .ToArray()
            .ShouldBeUnique();
    }
}