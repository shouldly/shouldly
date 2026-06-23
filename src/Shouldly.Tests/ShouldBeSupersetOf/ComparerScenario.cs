namespace Shouldly.Tests.ShouldBeSupersetOf;

public class ComparerScenario
{
    [Fact]
    public void ShouldPassWithComparer()
    {
        new[] { "A", "b", "C" }.ShouldBeSupersetOf(["a", "B"], StringComparer.OrdinalIgnoreCase);
    }
}
