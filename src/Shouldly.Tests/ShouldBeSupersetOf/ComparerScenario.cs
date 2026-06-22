namespace Shouldly.Tests.ShouldBeSupersetOf;

public class ComparerScenario
{
    [Fact]
    public void ShouldPassWithComparer()
    {
        // The comparer drives the pass/fail decision: without it the differing
        // casing would make "a"/"B" appear missing from { "A", "b", "C" }.
        new[] { "A", "b", "C" }.ShouldBeSupersetOf(["a", "B"], StringComparer.OrdinalIgnoreCase);
    }
}
