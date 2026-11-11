#if NET
namespace Shouldly.Tests.ShouldHaveCount;

public class CountableQueryScenario
{
    [Fact]
    public void APassingCountOfACountableQueryDoesNotRunIt()
    {
        var selected = 0;
        var query = new[] { 1, 2 }.Select(x => { selected++; return x; });

        query.ShouldHaveCount(2);

        selected.ShouldBe(0);
    }
}
#endif
