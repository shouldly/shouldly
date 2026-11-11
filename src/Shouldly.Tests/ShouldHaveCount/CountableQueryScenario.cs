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

    [Fact]
    public void AFailingCountOfACountableQueryReadsOnlyWhatTheMessageShows()
    {
        var selected = 0;
        var query = Enumerable.Range(1, 10_000).ToArray().Select(x => { selected++; return x; });

        using (MaxEnumerableItemsScope.Of(3))
        {
            Should.Throw<ShouldAssertException>(() => query.ShouldHaveCount(2)).Message
                .ShouldBe(
                    """
                    query
                        should have 2 items but had
                    10000
                        items and was
                    [1, 2, 3, ...]
                    """, StringCompareShould.IgnoreLineEndings);
        }

        // One item more than the message shows is read, which is how it knows to end with "...".
        selected.ShouldBe(4);
    }
}
#endif
